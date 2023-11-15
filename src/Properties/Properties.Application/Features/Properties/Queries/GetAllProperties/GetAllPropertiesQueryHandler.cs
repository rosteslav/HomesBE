using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQueryHandler(IPropertiesRepository propertiesRepository)
        : IRequestHandler<GetAllPropertiesQuery, IEnumerable<Property>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<IEnumerable<Property>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
            => await _propertiesRepository.Get();
    }
}
