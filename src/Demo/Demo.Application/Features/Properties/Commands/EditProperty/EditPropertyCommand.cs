using MediatR;

namespace Demo.Application.Features.Properties.Commands.EditProperty
{
    public class EditPropertyCommand : IRequest
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public short NumberOfRooms { get; set; }

        public string District { get; set; }

        public float Space { get; set; }

        public short Floor { get; set; }

        public short TotalFloorsInBuilding { get; set; }

        public int SellerId { get; set; }

        public int? BrokerId { get; set; }
    }
}
