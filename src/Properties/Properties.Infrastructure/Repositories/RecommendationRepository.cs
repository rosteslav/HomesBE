using BuildingMarket.Common.Models;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class RecommendationRepository(
        IConfiguration configuration,
        IPropertiesStore propertiesStore,
        INeighbourhoodsRepository neighbourhoodsRepository,
        ILogger<RecommendationRepository> logger)
        : IRecommendationRepository
    {
        private readonly int RecommendedCount = configuration.GetValue<int>("PropertiesRecommendedCount");
        private readonly ILogger<RecommendationRepository> _logger = logger;

        private readonly Lazy<Task<IDictionary<int, PropertyRedisModel>>> _lazyProperties = new(async () => await propertiesStore.GetProperties());
        private readonly Lazy<Task<NeighbourhoodsRatingModel>> _lazyNeighbourhoodsRating = new(async () => await neighbourhoodsRepository.GetRating());

        public async Task<IEnumerable<int>> GetRecommended(BuyerPreferencesRedisModel preferences, CancellationToken cancellationToken)
        {
            // NOTE: If there are no preferences for the given buyer, the "preferences" argument will be null.

            _logger.LogInformation("DB get recommended properties");

            try
            {
                var properties = await _lazyProperties.Value;
                var neighbourhoodsRating = await _lazyNeighbourhoodsRating.Value;

                var recommended = properties
                    .Select(p => new
                    {
                        Id = p.Key,
                        Grade = GradeProperty(p.Value, preferences)
                    })
                    .OrderByDescending(p => p.Grade)
                    .Take(RecommendedCount)
                    .Select(p => p.Id);

                return recommended;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to retrieve recommended properties.");
            }

            return Enumerable.Empty<int>();
        }

        private static int GradeProperty(
            PropertyRedisModel property,
            BuyerPreferencesRedisModel preferences)
        {
            if (string.IsNullOrEmpty(preferences.BuildingType)) return 10;

            var buildingTypes = preferences.BuildingType.Split('/');
            var grade = 0;

            if (buildingTypes.Contains(property.BuildingType))
            {
                grade = 10;
            }
            else if ((property.BuildingType == "ЕПК" || property.BuildingType == "Тухла") &&
                (buildingTypes.Contains("Тухла") || buildingTypes.Contains("ЕПК")))
            {
                grade = 5;
            }
            else if (property.BuildingType == "Панел" &&
                (buildingTypes.Contains("Тухла") || buildingTypes.Contains("ЕПК")))
            {
                grade = 5;
            }

            return grade;
        }
    }
}
