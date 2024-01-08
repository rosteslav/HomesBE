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

                var recommended = (await Task.WhenAll(properties.Select(async p => new { Id = p.Key, Grade = await GradeProperty(p.Value, preferences) })))
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

        private async Task<int> GradeProperty(PropertyRedisModel property, BuyerPreferencesRedisModel preferences)
        {
            int grade = 0;

            grade += GradeByRegion(property.Region, preferences?.Region);
            grade += GradeByBuildingType(property.BuildingType, preferences?.BuildingType);
            grade += await GradeByPurpose(property.Neighbourhood);

            return grade;
        }

        private async Task<int> GradeByPurpose(string propertyNeighbourhood)
        {
            var neighbourhoodsRating = await _lazyNeighbourhoodsRating.Value;

            bool isBest = neighbourhoodsRating.ForLiving.First().Contains(propertyNeighbourhood)
                || neighbourhoodsRating.ForInvestment.First().Contains(propertyNeighbourhood)
                || neighbourhoodsRating.Budget.First().Contains(propertyNeighbourhood)
                || neighbourhoodsRating.Luxury.First().Contains(propertyNeighbourhood);

            if (isBest)
                return 10;

            bool isSecondary = neighbourhoodsRating.ForLiving.Last().Contains(propertyNeighbourhood)
                || neighbourhoodsRating.ForInvestment.Last().Contains(propertyNeighbourhood)
                || neighbourhoodsRating.Budget.Last().Contains(propertyNeighbourhood)
                || neighbourhoodsRating.Luxury.Last().Contains(propertyNeighbourhood);

            if (isSecondary)
                return 5;

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
