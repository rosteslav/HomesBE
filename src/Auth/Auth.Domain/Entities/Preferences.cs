namespace BuildingMarket.Auth.Domain.Entities
{
    public class Preferences
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public string Purpose { get; set; }
        public string Region { get; set; }
        public string BuildingType { get; set; }
        public decimal PriceHigherEnd { get; set; }
        public string NumberOfRooms { get; set; }
    }
}
