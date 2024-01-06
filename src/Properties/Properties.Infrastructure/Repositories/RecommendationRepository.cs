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

        private static readonly Dictionary<string, HashSet<string>> _nextToRegions = new()
        {
            { "Юг", new() { "Запад", "Център", "Изток" } },
            { "Запад", new() { "Юг", "Център", "Север" } },
            { "Север", new() { "Запад", "Център", "Изток" } },
            { "Изток", new() { "Юг", "Център", "Север" } },
            { "Център", new() { "Юг", "Запад", "Север", "Изток" } }
        };

        public async Task<IEnumerable<int>> GetRecommended(BuyerPreferencesRedisModel preferences, CancellationToken cancellationToken)
        {
            // NOTE: If there are no preferences for the given buyer, the "preferences" argument will be null.

            _logger.LogInformation("DB get recommended properties");

            try
            {
                var properties = await _lazyProperties.Value;
                var neighbourhoodsRating = await _lazyNeighbourhoodsRating.Value;

                var preferredRegions = new HashSet<string>(preferences.Region is not null
                    ? preferences.Region.Split("/", StringSplitOptions.RemoveEmptyEntries)
                    : Enumerable.Empty<string>());

                var recommended = properties
                    .Select(p => new { Id = p.Key, Grade = GradeProperty(p.Value, preferredRegions) })
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

        private static int GradeProperty(PropertyRedisModel property, IEnumerable<string> preferredRegions)
        {
            int grade = 0;

            if (!preferredRegions.Any() || preferredRegions.Contains(property.Region))
                grade += 10;
            else if (_nextToRegions[property.Region].Overlaps(preferredRegions))
                grade += 5;

            return grade;
        }
    }
}
