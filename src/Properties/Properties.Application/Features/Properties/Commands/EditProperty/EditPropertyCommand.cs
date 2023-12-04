using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.EditProperty
{
    public class EditPropertyCommand : IRequest<DeletePropertyResult>
    {
        public int PropertyId { get; set; }
        public AddPropertyInputModel EditedProperty { get; set; }
        public string UserId { get; set; }
    }
}
