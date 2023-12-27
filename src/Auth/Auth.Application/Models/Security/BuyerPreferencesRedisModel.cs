using MessagePack;

namespace BuildingMarket.Auth.Application.Models.Security
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
    }
}
