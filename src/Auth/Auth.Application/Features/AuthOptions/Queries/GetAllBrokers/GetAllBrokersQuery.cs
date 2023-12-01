using BuildingMarket.Auth.Application.Models.AuthOptions;
using MediatR;

namespace BuildingMarket.Auth.Application.Features.AuthOptions.Queries.GetAllBrokers
{
    public class GetAllBrokersQuery : IRequest<IEnumerable<BrokerModel>>
    {
    }
}
