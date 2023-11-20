using BuildingMarket.Properties.Application.Contracts;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllBuilidngTypes
{
    public class GetAllBuilidngTypesQueryHandler(IPropertyOptionsRepository propertyOptionsRepository) : IRequestHandler<GetAllBuildingTypesQuery, IEnumerable<string>>
    {
        private readonly IPropertyOptionsRepository _propertyOptionsRepository = propertyOptionsRepository;

        public async Task<IEnumerable<string>> Handle(GetAllBuildingTypesQuery request, CancellationToken cancellationToken)
            => await _propertyOptionsRepository.GetBuidingTypes();
    }
}
