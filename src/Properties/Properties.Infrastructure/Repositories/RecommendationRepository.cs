using BuildingMarket.Common.Models;
using BuildingMarket.Properties.Application.Configurations;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class RecommendationRepository(
        IOptions<PropertiesConfiguration> propertiesConfiguration,
        IPropertiesStore propertiesStore,
        INeighbourhoodsRepository neighbourhoodsRepository,
        NextTo nextTo,
        ILogger<RecommendationRepository> logger)
        : IRecommendationRepository
    {
        private readonly PropertiesConfiguration _propertiesConfiguration = propertiesConfiguration.Value;
        private readonly NextTo _nextTo = nextTo;
        private readonly ILogger<RecommendationRepository> _logger = logger;

        private readonly Lazy<Task<IDictionary<int, PropertyRedisModel>>> _lazyProperties = new(async () => await propertiesStore.GetProperties());
        private readonly Lazy<Task<NeighbourhoodsRatingModel>> _lazyNeighbourhoodsRating = new(async () => await neighbourhoodsRepository.GetRating());

        public async Task<IEnumerable<int>> GetRecommended(BuyerPreferencesRedisModel preferences, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DB get recommended properties");

            try
            {
                var properties = await _lazyProperties.Value;
                var neighbourhoodsRating = await _lazyNeighbourhoodsRating.Value;

                var recommended = properties
                    .Select(p => new { Id = p.Key, Grade = GradeProperty(p.Value, preferences) })
                    .OrderByDescending(p => p.Grade)
                    .Take(_propertiesConfiguration.RecommendedCount)
                    .Select(p => p.Id);

                return recommended;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to retrieve recommended properties.");
            }

            return Enumerable.Empty<int>();
        }

        private int GradeProperty(PropertyRedisModel property, BuyerPreferencesRedisModel preferences)
        {
            int grade = 0;

            grade += GradeBy(property.Region, preferences?.Region, _nextTo.Region);
            grade += GradeBy(property.BuildingType, preferences?.BuildingType, _nextTo.BuildingType);
            grade += GradeBy(property.NumberOfRooms, preferences?.NumberOfRooms, _nextTo.NumberOfRooms);

            if (property.NumberOfImages == 0)
                return grade < _propertiesConfiguration.ReduceGradeValue ? 0 : grade - _propertiesConfiguration.ReduceGradeValue;

            return grade;
        }

        private int GradeBy(string sourceValue, string preferredValues, IDictionary<string, HashSet<string>> nextToValues)
        {
            var preferredCollection = preferredValues is not null
                ? preferredValues.Split("/", StringSplitOptions.RemoveEmptyEntries)
                : Enumerable.Empty<string>();

            if (!preferredCollection.Any() || preferredCollection.Contains(sourceValue))
                return 10;
            else if (nextToValues[sourceValue].Overlaps(preferredCollection))
                return 5;

            return 0;
        }
    }
}
