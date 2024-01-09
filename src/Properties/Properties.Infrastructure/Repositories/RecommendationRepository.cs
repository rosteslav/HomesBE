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

        private readonly Dictionary<string, HashSet<string>> _nextToRegions = new()
        {
            { Regions.South, new() { Regions.West, Regions.Center, Regions.East } },
            { Regions.West, new() { Regions.South, Regions.Center, Regions.North } },
            { Regions.North, new() { Regions.West, Regions.Center, Regions.East } },
            { Regions.East, new() { Regions.South, Regions.Center, Regions.North } },
            { Regions.Center, new() { Regions.South, Regions.West, Regions.North, Regions.East } }
        };

        private readonly Dictionary<string, HashSet<string>> _nextToBuildingTypes = new()
        {
            { BuildingTypes.Brick, new() { BuildingTypes.EPK } },
            { BuildingTypes.EPK, new() { BuildingTypes.Brick } },
            { BuildingTypes.Panel, new() { BuildingTypes.Brick, BuildingTypes.EPK } },
        };

        public async Task<IEnumerable<int>> GetRecommended(BuyerPreferencesRedisModel preferences, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DB get recommended properties");

            try
            {
                var properties = await _lazyProperties.Value;
                var neighbourhoodsRating = await _lazyNeighbourhoodsRating.Value;

                var recommended = properties
                    .Select(p => new { Id = p.Key, Grade = GradeProperty(p.Value, preferences, neighbourhoodsRating) })
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

        private int GradeProperty(PropertyRedisModel property, BuyerPreferencesRedisModel preferences, NeighbourhoodsRatingModel neighbourhoodsRating)
        {
            int grade = 0;

            grade += GradeByRegion(property.Region, preferences?.Region);
            grade += GradeByBuildingType(property.BuildingType, preferences?.BuildingType);
            grade += GradeByPurpose(property.Neighbourhood, preferences.Purpose, neighbourhoodsRating);

            return grade;
        }

        private int GradeByPurpose(string neighbourhood, string purpose, NeighbourhoodsRatingModel neighbourhoodsRating)
        {
            if (string.IsNullOrWhiteSpace(purpose) || neighbourhoodsRating is null)
                return 10;

            if ((purpose.Contains(Purposes.ForLiving) && neighbourhoodsRating.ForLiving.First().Contains(neighbourhood))
                || (purpose.Contains(Purposes.ForInvestment) && neighbourhoodsRating.ForInvestment.First().Contains(neighbourhood))
                || (purpose.Contains(Purposes.Budget) && neighbourhoodsRating.Budget.First().Contains(neighbourhood))
                || (purpose.Contains(Purposes.Luxury) && neighbourhoodsRating.Luxury.First().Contains(neighbourhood)))
            {
                return 10;
            }

            if ((purpose.Contains(Purposes.ForLiving) && neighbourhoodsRating.ForLiving.Last().Contains(neighbourhood))
                || (purpose.Contains(Purposes.ForInvestment) && neighbourhoodsRating.ForInvestment.Last().Contains(neighbourhood))
                || (purpose.Contains(Purposes.Budget) && neighbourhoodsRating.Budget.Last().Contains(neighbourhood))
                || (purpose.Contains(Purposes.Luxury) && neighbourhoodsRating.Luxury.Last().Contains(neighbourhood)))
            {
                return 5;
            }

            return 0;
        }

        private int GradeByRegion(string propertyRegion, string preferredRegions)
        {
            var regionsCollection = preferredRegions is not null
                ? preferredRegions.Split("/", StringSplitOptions.RemoveEmptyEntries)
                : Enumerable.Empty<string>();

            if (regionsCollection.Contains(propertyRegion))
                return 10;
            else if (_nextToRegions[propertyRegion].Overlaps(regionsCollection))
                return 5;

            return 0;
        }

        private int GradeByBuildingType(string propertyBType, string preferredBuildingTypes)
        {
            var buildingTypes = string.IsNullOrEmpty(preferredBuildingTypes)
                ? Enumerable.Empty<string>()
                : preferredBuildingTypes.Split("/");

            if (!buildingTypes.Any()) return 10;

            return buildingTypes.Contains(propertyBType) ? 10
                : _nextToBuildingTypes[propertyBType].Overlaps(buildingTypes) ? 5
                : 0;
        }
    }
}
