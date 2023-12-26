namespace BuildingMarket.Properties.Application.Models
{
    public class GetAllPropertiesOutputModel
    {
        public int Id { get; set; }
        public required string Neighbourhood { get; set; }
        public required string NumberOfRooms { get; set; }
        public required decimal Space { get; set; }
        public required decimal Price { get; set; }
        public required string Details { get; set; }
        public required DateTime CreatedOnLocalTime { get; set; }
        public required IEnumerable<string> Images { get; set; } = new List<string>();
    }
}
