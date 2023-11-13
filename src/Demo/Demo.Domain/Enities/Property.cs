namespace Demo.Domain.Enities
{
    using System.ComponentModel.DataAnnotations;

    public class Property(string type,
        short numberOfRooms,
        string district,
        float space,
        short floor,
        short totalFloorsInBuilding,
        int sellerId,
        int? brokerId)
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; } = type;
        [Required]
        public short NumberOfRooms { get; set; } = numberOfRooms;
        [Required]
        public string District { get; set; } = district;
        [Required]
        public float Space { get; set; } = space;
        [Required]
        public short Floor { get; set; } = floor;
        [Required]
        public short TotalFloorsInBuilding { get; set; } = totalFloorsInBuilding;
        [Required]
        public int SellerId { get; set; } = sellerId;
        public int? BrokerId { get; set; } = brokerId;
    }


}
