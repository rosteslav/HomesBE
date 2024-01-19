namespace BuildingMarket.Properties.Application.Models
{
    public class GetAllPropertiesInputModel
    {
        public string[] Neighbourhood { get; set; }

        public string[] NumberOfRooms { get; set; }

        public decimal SpaceFrom { get; set; }

        public decimal SpaceTo { get; set; }

        public decimal PriceFrom { get; set; }

        public decimal PriceTo { get; set; }

        public string[] Finish { get; set; }

        public string[] Furnishment { get; set; }

        public string[] Heating { get; set; }

        public string[] BuildingType { get; set; }

        public string[] Exposure { get; set; }

        public string OrderBy { get; set; }

        public bool IsAscending { get; set; }

        public int PublishedOn { get; set; }

        public int Page { get; set; } = 1;
    }
}
