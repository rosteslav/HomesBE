using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Property.Queries.PropertyExists
{
    public class PropertyExistsQueryHandler(IPropertiesRepository propertiesRepository)
        : IRequestHandler<PropertyExistsQuery, bool>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public Task<bool> Handle(
            PropertyExistsQuery request,
            CancellationToken cancellationToken)
            => _propertiesRepository
                .PropertyExists(request.PropertyId);
    }
}
