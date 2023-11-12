using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker
{
    public class GetByBrokerQuery : IRequest<IEnumerable<Property>>
    {
        public string BrokerId { get; set; }
    }
}
