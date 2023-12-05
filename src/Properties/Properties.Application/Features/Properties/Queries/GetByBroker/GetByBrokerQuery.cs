using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker
{
    public class GetByBrokerQuery : IRequest<IEnumerable<PropertyModelWithId>>
    {
        public string BrokerId { get; set; }
    }
}
