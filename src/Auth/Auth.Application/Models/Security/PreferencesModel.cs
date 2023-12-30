using System.ComponentModel.DataAnnotations;

namespace BuildingMarket.Auth.Application.Models.Security
{
    public class PreferencesModel
    {
        public string UserId { get; set; }

        public string Purpose { get; set; }

        public string Region { get; set; }

        public string BuildingType { get; set; }

        [Range(0, 100_000_000_000)]
        public decimal PriceHigherEnd { get; set; }
        public string NumberOfRooms { get; set; }
    }
}
