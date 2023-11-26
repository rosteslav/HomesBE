using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker
{
    public class GetByBrokerQuery : IRequest<IEnumerable<PropertyModel>>
    {
        public string BrokerId { get; set; }
    }
}
