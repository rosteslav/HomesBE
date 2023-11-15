using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetBySeller
{
    public class GetBySellerQueryHandler(IPropertiesRepository propertiesRepository)
        : IRequestHandler<GetBySellerQuery, IEnumerable<Property>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<IEnumerable<Property>> Handle(GetBySellerQuery request, CancellationToken cancellationToken)
            => await _propertiesRepository.GetBySeller(request.SellerId);
    }
}