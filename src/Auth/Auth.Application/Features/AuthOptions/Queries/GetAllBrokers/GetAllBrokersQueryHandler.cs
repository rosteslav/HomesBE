using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.AuthOptions;
using MediatR;

namespace BuildingMarket.Auth.Application.Features.AuthOptions.Queries.GetAllBrokers
{
    public class GetAllBrokersQueryHandler(IAuthOptionsRepository authOptionsRepository)
        : IRequestHandler<GetAllBrokersQuery, IEnumerable<BrokerModel>>
    {
        private readonly IAuthOptionsRepository _authOptionsRepository = authOptionsRepository;

        public Task<IEnumerable<BrokerModel>> Handle(GetAllBrokersQuery request, CancellationToken cancellationToken)
            => _authOptionsRepository.GetAllBrokers();
    }
}
