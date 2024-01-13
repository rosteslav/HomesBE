using BuildingMarket.Properties.Application.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IRecommendationService
    {
        BuyerRecommendedPriceRanges GetBuyerRecommendedPriceRanges(decimal priceHigherEnd, IEnumerable<decimal> prices);
    }
}
