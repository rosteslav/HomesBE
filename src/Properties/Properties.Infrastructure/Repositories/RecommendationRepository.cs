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

        private readonly Dictionary<string, HashSet<string>> _nextToNumberOfRooms = new()
        {
            { NumberOfRooms.Studio, new() { NumberOfRooms.OneBedroom, NumberOfRooms.Attic } },
            { NumberOfRooms.OneBedroom, new() { NumberOfRooms.Studio, NumberOfRooms.ThreeBedroom } },
            { NumberOfRooms.ThreeBedroom, new() { NumberOfRooms.OneBedroom, NumberOfRooms.FourRooms, NumberOfRooms.Maisonette } },
            { NumberOfRooms.FourRooms, new() { NumberOfRooms.ThreeBedroom, NumberOfRooms.Multiroom, NumberOfRooms.Maisonette } },
            { NumberOfRooms.Multiroom, new() { NumberOfRooms.ThreeBedroom, NumberOfRooms.FourRooms, NumberOfRooms.Maisonette } },
            { NumberOfRooms.Maisonette, new() { NumberOfRooms.ThreeBedroom, NumberOfRooms.FourRooms, NumberOfRooms.Multiroom } },
            { NumberOfRooms.Garage, new() },
            { NumberOfRooms.Warehouse, new() { NumberOfRooms.Garage, NumberOfRooms.Attic } },
            { NumberOfRooms.Attic, new() { NumberOfRooms.Garage, NumberOfRooms.Warehouse } },
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

            grade += GradeBy(property.Region, preferences?.Region, _nextToRegions);
            grade += GradeBy(property.BuildingType, preferences?.BuildingType, _nextToBuildingTypes);
            grade += GradeBy(property.NumberOfRooms, preferences?.NumberOfRooms, _nextToNumberOfRooms);
            grade += GradeByPurpose(property.Neighbourhood, preferences?.Purpose, neighbourhoodsRating);

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
