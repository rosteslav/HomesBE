using BuildingMarket.Properties.Application.Contracts;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllFinishTypes
{
    public class GetAllFinishTypesQueryHandler(IPropertyOptionsRepository propertyOptionsRepository) : IRequestHandler<GetAllFinishTypesQuery, IEnumerable<string>>
    {
        private readonly IPropertyOptionsRepository _propertyOptionsRepository = propertyOptionsRepository;

        public async Task<IEnumerable<string>> Handle(GetAllFinishTypesQuery request, CancellationToken cancellationToken)
            => await _propertyOptionsRepository.GetFinishTypes();
    }
}
