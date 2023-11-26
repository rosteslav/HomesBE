using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetBySeller
{
    public class GetBySellerQueryHandler(IPropertiesRepository propertiesRepository)
        : IRequestHandler<GetBySellerQuery, IEnumerable<PropertyModel>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<IEnumerable<PropertyModel>> Handle(GetBySellerQuery request, CancellationToken cancellationToken)
            => await _propertiesRepository.GetBySeller(request.SellerId);
    }
}