using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQueryHandler(IPropertiesRepository propertiesRepository)
        : IRequestHandler<GetAllPropertiesQuery, IEnumerable<GetAllPropertiesOutputModel>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<IEnumerable<GetAllPropertiesOutputModel>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
            => await _propertiesRepository.Get();
    }
}
