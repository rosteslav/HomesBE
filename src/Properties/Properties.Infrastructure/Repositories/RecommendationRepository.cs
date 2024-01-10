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

        private readonly Lazy<Task<IEnumerable<PropertyRedisModel>>> _lazyProperties = new(async () => await propertiesStore.GetProperties());
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

        private IEnumerable<(int Id, decimal Price)> _orderedProperties;

        public async Task<IEnumerable<int>> GetRecommended(BuyerPreferencesRedisModel preferences, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DB get recommended properties");

            try
            {
                var properties = await _lazyProperties.Value;
                var neighbourhoodsRating = await _lazyNeighbourhoodsRating.Value;

                var gradesByPrice = CalculateGradesByPrice(preferences.PriceHigherEnd, properties);

                var recommended = properties
                    .Select(p => new { p.Id, Grade = GradeProperty(p, preferences) + gradesByPrice[p.Id] })
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

        private int GradeProperty(PropertyRedisModel property, BuyerPreferencesRedisModel preferences)
        {
            int grade = 0;

            grade += GradeByRegion(property.Region, preferences?.Region);
            grade += GradeByBuildingType(property.BuildingType, preferences?.BuildingType);

            return grade;
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

            if (!buildingTypes.Any())
                return 10;

            return buildingTypes.Contains(propertyBType) ? 10
                : _nextToBuildingTypes[propertyBType].Overlaps(buildingTypes) ? 5
                : 0;
        }

        private IDictionary<int, int> CalculateGradesByPrice(decimal priceHigherEnd, IEnumerable<PropertyRedisModel> orderedProperties)
        {
            var grades = orderedProperties.Select(p => (p.Id, default(int))).ToDictionary();

            // Grading cheapest
            var cheapest = orderedProperties.FirstOrDefault();
            if (cheapest.Equals(default))
                return grades;
            grades[cheapest.Id] = 5;

            // Grading closest cheaper price
            int left = 0;
            int right = orderedProperties.Count() - 1;
            int closestCheaperIndex = FindClosestCheaperPropertyIndex(left, right, priceHigherEnd, orderedProperties);
            var closestCheaper = closestCheaperIndex != -1 ? orderedProperties.ElementAt(closestCheaperIndex) : default;
            if (!closestCheaper.Equals(default))
                grades[closestCheaper.Id] = 10;

            // Grading lower price
            int grade = 0;
            decimal gap = closestCheaper.Price - cheapest.Price;
            decimal partPriceRange = gap / 4;
            if (gap > 0 && partPriceRange > 0)
            {
                grade = 6;
                left = 0;
                right = closestCheaperIndex - 1;
                for (decimal maxPrice = cheapest.Price + partPriceRange; maxPrice <= closestCheaper.Price; maxPrice += partPriceRange)
                {
                    int index = FindClosestCheaperPropertyIndex(left, right, maxPrice, orderedProperties);
                    if (index != -1)
                    {
                        var id = orderedProperties.ElementAt(index).Id;
                        grades[id] = grade;
                        left = index;
                    }

                    grade++;
                }
            }

            var mostExpensive = orderedProperties.LastOrDefault();
            if (mostExpensive.Equals(closestCheaper))
                return grades;

            // Grading higher price
            gap = mostExpensive.Price - priceHigherEnd;
            partPriceRange = gap / 5;
            grade = 0;
            left = closestCheaperIndex + 1;
            right = orderedProperties.Count() - 1;
            for (decimal maxPrice = priceHigherEnd + partPriceRange; maxPrice <= mostExpensive.Price; maxPrice += partPriceRange)
            {
                var index = FindClosestCheaperPropertyIndex(left, right, maxPrice, orderedProperties);
                if (index == -1)
                    index = right;

                var id = orderedProperties.ElementAt(index).Id;
                grades[id] = grade++;
                left = index;
            }

            return grades;
        }

        private int FindClosestCheaperPropertyIndex(int left, int right, decimal priceTarget, IEnumerable<PropertyRedisModel> properties)
        {
            while (left + 1 < right)
            {
                int mid = (right + left) / 2;
                var midPrice = properties.ElementAt(mid).Price;
                var nextToMidPrice = properties.ElementAt(mid + 1).Price;

                if (midPrice <= priceTarget)
                {
                    if (nextToMidPrice > priceTarget)
                        return mid;
                    else
                        left = mid;
                }
                else
                {
                    right = mid;
                }
            }

            return properties.ElementAt(right).Price <= priceTarget ? right
                : properties.ElementAt(left).Price <= priceTarget ? left
                : -1;
        }
    }
}
