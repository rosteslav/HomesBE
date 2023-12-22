namespace BuildingMarket.Properties.Application.Models
{
    public class GetAllPropertiesOutputModel
    {
        public int Id { get; set; }
        public string Neighbourhood { get; set; }
        public string NumberOfRooms { get; set; }
        public decimal Space { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public DateTime CreatedOnLocalTime { get; set; }
        public IEnumerable<string> Images { get; set; } = new List<string>();
    }
}
