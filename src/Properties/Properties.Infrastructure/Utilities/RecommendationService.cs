using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;

namespace BuildingMarket.Properties.Infrastructure.Utilities
{
    public class RecommendationService : IRecommendationService
    {
        public BuyerRecommendedPriceRanges GetBuyerRecommendedPriceRanges(decimal priceHigherEnd, IEnumerable<decimal> prices)
        {
            if (!prices.Any())
                return new();

            decimal lowestPrice = prices.First();
            decimal highestPrice = prices.Last();
            decimal bestPrice = FindBestPrice(priceHigherEnd, prices);

            return new BuyerRecommendedPriceRanges
            {
                BestPrice = bestPrice,
                LowestPrice = lowestPrice,
                HighestPrice = highestPrice,
                GapsBelow = FindGapsBelow(bestPrice, lowestPrice),
                GapsAbove = FindGapsAbove(priceHigherEnd, highestPrice)
            };
        }

        private decimal FindBestPrice(decimal priceHigherEnd, IEnumerable<decimal> prices)
        {
            int leftBoundary = 0;
            int rightBoundary = prices.Count() - 1;

            while (leftBoundary + 1 < rightBoundary)
            {
                int mid = (rightBoundary + leftBoundary) / 2;
                var midPrice = prices.ElementAt(mid);
                var nextToMidPrice = prices.ElementAt(mid + 1);

                if (midPrice <= priceHigherEnd)
                {
                    if (nextToMidPrice > priceHigherEnd)
                        return prices.ElementAt(mid);
                    else
                        leftBoundary = mid;
                }
                else
                {
                    rightBoundary = mid;
                }
            }

            if (prices.ElementAt(rightBoundary) <= priceHigherEnd)
                return prices.ElementAt(rightBoundary);
            else if (prices.ElementAt(leftBoundary) <= priceHigherEnd)
                return prices.ElementAt(leftBoundary);

            return 0;
        }

        private IEnumerable<decimal> FindGapsBelow(decimal bestPrice, decimal lowestPrice)
        {
            if (bestPrice <= lowestPrice)
                return Enumerable.Empty<decimal>();

            decimal gapRange = (bestPrice - lowestPrice) / 4;

            return Enumerable.Range(1, 4).Select(gap => lowestPrice + (gapRange * gap)).ToArray();
        }

        private IEnumerable<decimal> FindGapsAbove(decimal priceHigherEnd, decimal highestPrice)
        {
            if (priceHigherEnd >= highestPrice)
                return Enumerable.Empty<decimal>();

            decimal gapRange = (highestPrice - priceHigherEnd) / 5;

            return Enumerable.Range(1, 5).Select(gap => priceHigherEnd + (gapRange * gap)).ToArray();
        }
    }
}
