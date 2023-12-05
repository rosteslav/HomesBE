namespace BuildingMarket.Properties.Application.Models
{
    public class PropertyOptionsModel
    {
        public IEnumerable<string> BuildingType { get; set; }
        public IEnumerable<string> Exposure { get; set; }
        public IEnumerable<string> Finish { get; set; }
        public IEnumerable<string> Furnishment { get; set; }
        public IEnumerable<string> Garage { get; set; }
        public IEnumerable<string> Heating { get; set; }
        public IEnumerable<string> Neighbourhood { get; set; }
        public IEnumerable<string> NumberOfRooms { get; set; }
    }
}
