namespace BuildingMarket.Properties.Application.Models
{
    public class PropertyOptionsModel
    {
        public required IEnumerable<string> BuildingType { get; set; }
        public required IEnumerable<string> Exposure { get; set; }
        public required IEnumerable<string> Finish { get; set; }
        public required IEnumerable<string> Furnishment { get; set; }
        public required IEnumerable<string> Garage { get; set; }
        public required IEnumerable<string> Heating { get; set; }
        public required IEnumerable<string> Neighbourhood { get; set; }
        public required IEnumerable<string> NumberOfRooms { get; set; }
    }
}
