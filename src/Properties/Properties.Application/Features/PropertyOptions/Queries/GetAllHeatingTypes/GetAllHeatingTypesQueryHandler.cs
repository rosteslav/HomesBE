using BuildingMarket.Properties.Application.Contracts;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllHeatingTypes
{
    public class GetAllHeatingTypesQueryHandler(IPropertyOptionsRepository propertyOptionsRepository) : IRequestHandler<GetAllHeatingTypesQuery, IEnumerable<string>>
    {
        private readonly IPropertyOptionsRepository _propertyOptionsRepository = propertyOptionsRepository;

        public async Task<IEnumerable<string>> Handle(GetAllHeatingTypesQuery request, CancellationToken cancellationToken)
            => await _propertyOptionsRepository.GetHeatingTypes();
    }
}
