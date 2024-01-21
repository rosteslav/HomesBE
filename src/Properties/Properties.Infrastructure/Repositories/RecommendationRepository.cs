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
        IRecommendationService recommendationService,
        NextTo nextTo,
        ILogger<RecommendationRepository> logger)
        : IRecommendationRepository
    {
        private readonly PropertiesConfiguration _propertiesConfiguration = propertiesConfiguration.Value;
        private readonly IRecommendationService _recommendationService = recommendationService;
        private readonly NextTo _nextTo = nextTo;
        private readonly ILogger<RecommendationRepository> _logger = logger;

        private readonly Lazy<Task<IEnumerable<PropertyRedisModel>>> _lazyProperties = new(async () => await propertiesStore.GetProperties());
        private readonly Lazy<Task<NeighbourhoodsRatingModel>> _lazyNeighbourhoodsRating = new(async () => await neighbourhoodsRepository.GetRating());

        public async Task<IEnumerable<int>> GetRecommended(BuyerPreferencesRedisModel preferences, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DB get recommended properties");

            try
            {
                var properties = await _lazyProperties.Value;

                var recommendedPriceRanges = _recommendationService
                    .GetBuyerRecommendedPriceRanges(preferences?.PriceHigherEnd ?? 0M, properties.Select(p => p.Price));

                var recommended = properties
                    .Select(p => (p.Id, Grade: GradeProperty(p, preferences, recommendedPriceRanges).Result))
                    .OrderByDescending(p => p.Grade)
                    .Take(_propertiesConfiguration.RecommendedCount)
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

        private async Task<int> GradeProperty(PropertyRedisModel property, BuyerPreferencesRedisModel preferences, BuyerRecommendedPriceRanges recommendedPriceRanges)
        {
            int grade = 0;

            grade += await GradeBy(property.Region, preferences?.Region, _nextTo.Region);
            grade += await GradeBy(property.BuildingType, preferences?.BuildingType, _nextTo.BuildingType);
            grade += await GradeBy(property.NumberOfRooms, preferences?.NumberOfRooms, _nextTo.NumberOfRooms);
            grade += await GradeByPurpose(property.Neighbourhood, preferences?.Purpose);
            grade += await GradeByPrice(property.Price, preferences?.PriceHigherEnd ?? 0M, recommendedPriceRanges);

            if (property.NumberOfImages == 0)
                return grade < _propertiesConfiguration.ReduceGradeValue ? 0 : grade - _propertiesConfiguration.ReduceGradeValue;

            return grade;
        }

        private async Task<int> GradeByPrice(decimal propertyPrice, decimal priceHigherEnd, BuyerRecommendedPriceRanges recommendedPriceRanges)
        {
            await Task.Yield();

            if (priceHigherEnd <= 0M)
                return 10;

            if (propertyPrice == recommendedPriceRanges.BestPrice)
                return 10;

            if (propertyPrice == recommendedPriceRanges.LowestPrice)
                return 5;

            if (propertyPrice == recommendedPriceRanges.HighestPrice)
                return 0;

            int grade = 6;
            foreach (var gapPrice in recommendedPriceRanges.GapsBelow)
            {
                if (propertyPrice <= gapPrice)
                    return grade;

                grade++;
            }

            grade = 4;
            foreach (var gapPrice in recommendedPriceRanges.GapsAbove)
            {
                if (propertyPrice <= gapPrice)
                    return grade;

                grade--;
            }

            return 0;
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
