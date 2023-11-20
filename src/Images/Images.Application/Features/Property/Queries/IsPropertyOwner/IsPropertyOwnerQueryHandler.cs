using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Property.Queries.IsPropertyOwner
{
    public class IsPropertyOwnerQueryHandler(IPropertiesRepository propertiesRepository)
        : IRequestHandler<IsPropertyOwnerQuery, bool>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public Task<bool> Handle(
            IsPropertyOwnerQuery request,
            CancellationToken cancellationToken)
            => _propertiesRepository
                .IsPropertyOwner(request.PropertyId, request.UserId);
    }
}
