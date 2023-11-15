using BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty
{
    public class AddPropertyCommand : IRequest
    {
        public PropertyModel Model { get; set; }

        public string SellerId { get; set; }
    }
}
