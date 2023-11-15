using System.ComponentModel.DataAnnotations;

namespace BuildingMarket.Properties.Application.Models
{
    public class PropertyModel
    {
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

        public String? BrokerId { get; set; }

        public override String ToString()
        {
            return $"Type:{Type},\n" +
                $"Number Of Rooms:{NumberOfRooms},\n" +
                $"District:{District},\n" +
                $"Space:{Space},\n" +
                $"Floor{Floor},\n" +
                $"Total Floors In Building:{TotalFloorsInBuilding},\n" +
                $"Broker Id: {(BrokerId == null ? BrokerId : "No broker added yet")}";
        }
    }
}
