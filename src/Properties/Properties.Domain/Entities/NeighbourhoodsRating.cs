namespace BuildingMarket.Properties.Domain.Entities
{
    public class NeighbourhoodsRating
    {
        public int Id { get; set; }

        public required string ForLiving { get; set; }

        public required string ForInvestment { get; set; }

        public required string Budget { get; set; }

        public required string Luxury { get; set; }
    }
}
