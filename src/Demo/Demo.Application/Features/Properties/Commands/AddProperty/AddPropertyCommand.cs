using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Demo.Application.Features.Properties.Commands.AddProperty
{
    public class AddPropertyCommand : IRequest
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

        [Required]
        public int SellerId { get; set; }

        public int? BrokerId { get; set; }

        public override String ToString()
        {
            return $"Type:{Type},\n" +
                $"Number Of Rooms:{NumberOfRooms},\n" +
                $"District:{District},\n" +
                $"Space:{Space},\n" +
                $"Floor{Floor},\n" +
                $"Total Floors In Building:{TotalFloorsInBuilding},\n" +
                $"Seller Id:{SellerId},\n" +
                $"Broker Id: {(BrokerId == null ? BrokerId : "No broker added yet")}";
        }
    }
}
