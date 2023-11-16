using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddMultipleProperties.Commands
{
    public class AddMultiplePropertiesCommand : IRequest<Response>
    {
        public IEnumerable<PropertyModel> Properties { get; set; }
    }
}
