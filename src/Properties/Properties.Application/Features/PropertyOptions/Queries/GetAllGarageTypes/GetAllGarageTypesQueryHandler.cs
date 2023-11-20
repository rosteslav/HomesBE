using BuildingMarket.Properties.Application.Contracts;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllGarageTypes
{

    public class GetAllGarageTypesQueryHandler(IPropertyOptionsRepository propertyOptionsRepository) : IRequestHandler<GetAllGarageTypesQuery, IEnumerable<string>>
    {
        private readonly IPropertyOptionsRepository _propertyOptionsRepository = propertyOptionsRepository;

        public async Task<IEnumerable<string>> Handle(GetAllGarageTypesQuery request, CancellationToken cancellationToken)
            => await _propertyOptionsRepository.GetGarageTypes();
    }
}
