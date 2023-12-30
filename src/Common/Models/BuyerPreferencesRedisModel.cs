using MessagePack;

namespace BuildingMarket.Common.Models
{
    [MessagePackObject]
    public class BuyerPreferencesRedisModel
    {
        [Key(0)]
        public string Purpose { get; set; }

        [Key(1)]
        public string Region { get; set; }

        [Key(2)]
        public string BuildingType { get; set; }

        [Key(3)]
        public decimal PriceHigherEnd { get; set; }

        [Key(4)]
        public string NumberOfRooms { get; set; }
    }
}
