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
                var recommended = (await _lazyProperties.Value)
                    .Select(async p => new { Id = p.Key, Grade = await GradeProperty(p.Value, preferences) })
                    .OrderByDescending(p => p.Result.Grade)
                    .Take(RecommendedCount)
                    .Select(p => p.Id)
                    .ToArray();

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

            grade += await GradeBy(property.Region, preferences?.Region, _nextToRegions);
            grade += await GradeBy(property.BuildingType, preferences?.BuildingType, _nextToBuildingTypes);
            grade += await GradeBy(property.NumberOfRooms, preferences?.NumberOfRooms, _nextToNumberOfRooms);
            grade += await GradeByPurpose(property.Neighbourhood, preferences?.Purpose);

            return grade;
        }

        private async Task<int> GradeByPurpose(string neighbourhood, string purpose)
        {
            var neighbourhoodsRating = await _lazyNeighbourhoodsRating.Value;
            if (string.IsNullOrWhiteSpace(purpose) || neighbourhoodsRating is null)
                return 10;

            if ((purpose.Contains(Purposes.ForLiving) && neighbourhoodsRating.ForLiving.First().Contains(neighbourhood))
                    || (purpose.Contains(Purposes.ForInvestment) && neighbourhoodsRating.ForInvestment.First().Contains(neighbourhood))
                    || (purpose.Contains(Purposes.Budget) && neighbourhoodsRating.Budget.First().Contains(neighbourhood))
                    || (purpose.Contains(Purposes.Luxury) && neighbourhoodsRating.Luxury.First().Contains(neighbourhood)))
                return 10;

            if ((purpose.Contains(Purposes.ForLiving) && neighbourhoodsRating.ForLiving.Last().Contains(neighbourhood))
                    || (purpose.Contains(Purposes.ForInvestment) && neighbourhoodsRating.ForInvestment.Last().Contains(neighbourhood))
                    || (purpose.Contains(Purposes.Budget) && neighbourhoodsRating.Budget.Last().Contains(neighbourhood))
                    || (purpose.Contains(Purposes.Luxury) && neighbourhoodsRating.Luxury.Last().Contains(neighbourhood)))
                return 5;

            return 0;
        }

        private async Task<int> GradeBy(string sourceValue, string preferredValues, IDictionary<string, HashSet<string>> nextToValues)
        {
            await Task.Yield();

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
