using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllNeighbourhoods
{
    public class GetAllNeighbourhoodsQueryHandler(IPropertyOptionsRepository propertyOptionsRepository) : IRequestHandler<GetAllNeighbourhoodsQuery, IEnumerable<string>>
    {
        private readonly IPropertyOptionsRepository _propertyOptionsRepository = propertyOptionsRepository;

        public async Task<IEnumerable<string>> Handle(GetAllNeighbourhoodsQuery request, CancellationToken cancellationToken)
                => await _propertyOptionsRepository.GetNeighbourhoods();
    }
}
