using System.ComponentModel.DataAnnotations;

namespace BuildingMarket.Properties.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public short NumberOfRooms { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public float Space { get; set; }

        [Required]
        public short Floor { get; set; }

        [Required]
        public short TotalFloorsInBuilding { get; set; }

        [Required]
        public string SellerId { get; set; }

        public string? BrokerId { get; set; }
    }
}
