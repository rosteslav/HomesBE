namespace BuildingMarket.Properties.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public required int PropertyId { get; set; }

        public required string ImageURL { get; set; } = null!;
    }
}
