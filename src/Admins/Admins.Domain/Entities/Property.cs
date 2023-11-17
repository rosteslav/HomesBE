namespace BuildingMarket.Admins.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }

        public required string Type { get; set; }

        public required int NumberOfRooms { get; set; }

        public required string District { get; set; }

        public required decimal Space { get; set; }

        public required int Floor { get; set; }

        public required int TotalFloorsInBuilding { get; set; }

        public required string SellerId { get; set; }

        public string BrokerId { get; set; }
    }
}
