using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetPropertyOptionsWithFilter
{
    public class GetPropertyOptionsWithFilterQueryHandler(IPropertyOptionsRepository propertyOptionsRepository)
        : IRequestHandler<GetPropertyOptionsWithFilterQuery, PropertyOptionsWithFilterModel>
    {
        private readonly IPropertyOptionsRepository _propertyOptionsRepository = propertyOptionsRepository;

        public async Task<PropertyOptionsWithFilterModel> Handle(GetPropertyOptionsWithFilterQuery request, CancellationToken cancellationToken)
            => await _propertyOptionsRepository.GetPropertyOptionsWithFilter();
    }
}
