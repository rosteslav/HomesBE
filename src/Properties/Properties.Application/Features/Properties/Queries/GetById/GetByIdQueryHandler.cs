using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetById
{
    public class GetByIdQueryHandler(IPropertiesRepository propertiesRepository) : IRequestHandler<GetByIdQuery, PropertyModel>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<PropertyModel> Handle(GetByIdQuery request, CancellationToken cancellationToken)
            => await _propertiesRepository.GetById(request.Id);
    }
}
