namespace BuildingMarket.Properties.Application.Models
{
    public class BuyerRecommendedPriceRanges
    {
        public decimal BestPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public IEnumerable<decimal> GapsBelow { get; set; } = new List<decimal>();
        public IEnumerable<decimal> GapsAbove { get; set; } = new List<decimal>();
    }
}
