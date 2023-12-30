using MessagePack;

namespace BuildingMarket.Properties.Application.Models
{
    [MessagePackObject]
    public class PropertyRedisModel
    {
        [Key(0)]
        public decimal Price { get; set; }

        [Key(1)]
        public string Neighbourhood { get; set; }

        [Key(2)]
        public string Region { get; set; }

        [Key(3)]
        public string NumberOfRooms { get; set; }

        [Key(4)]
        public string BuildingType { get; set; }
    }
}
