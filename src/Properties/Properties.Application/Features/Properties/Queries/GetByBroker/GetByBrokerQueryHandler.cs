using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker
{
    public class GetByBrokerQueryHandler(IPropertiesRepository propertiesRepository)
        : IRequestHandler<GetByBrokerQuery, IEnumerable<PropertyModelWithId>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<IEnumerable<PropertyModelWithId>> Handle(GetByBrokerQuery request, CancellationToken cancellationToken)
            => await _propertiesRepository.GetByBroker(request.BrokerId);
    }
}
