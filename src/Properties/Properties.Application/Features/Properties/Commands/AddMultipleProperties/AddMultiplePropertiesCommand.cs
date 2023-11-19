using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddMultipleProperties
{
    public class AddMultiplePropertiesCommand : IRequest
    {
        public IEnumerable<PropertyModel> Properties { get; set; }
    }
}
