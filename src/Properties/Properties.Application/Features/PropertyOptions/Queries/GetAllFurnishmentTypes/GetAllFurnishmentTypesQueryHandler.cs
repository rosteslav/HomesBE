using BuildingMarket.Properties.Application.Contracts;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllFurnishmentTypes
{
    public class GetAllFurnishmentTypesQueryHandler(IPropertyOptionsRepository propertyOptionsRepository) : IRequestHandler<GetAllFurnishmentTypesQuery, IEnumerable<string>>
    {
        private readonly IPropertyOptionsRepository _propertyOptionsRepository = propertyOptionsRepository;

        public async Task<IEnumerable<string>> Handle(GetAllFurnishmentTypesQuery request, CancellationToken cancellationToken)
            => await _propertyOptionsRepository.GetFurnishmentTypes();
    }
}
