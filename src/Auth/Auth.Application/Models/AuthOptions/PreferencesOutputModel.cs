namespace BuildingMarket.Auth.Application.Models.AuthOptions
{
    public class PreferencesOutputModel
    {
        public IEnumerable<string> Purposes { get; set; }
        public IEnumerable<string> Regions { get; set; }
        public IEnumerable<string> BuildingTypes { get; set; }
        public IEnumerable<string> NumberOfRooms { get; set; }
    }
}
