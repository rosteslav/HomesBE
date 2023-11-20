using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty
{
    public class AddPropertyCommand : IRequest<PropertyOutputModel>
    {
        public PropertyModel Model { get; set; }

        public string SellerId { get; set; }
    }
}
