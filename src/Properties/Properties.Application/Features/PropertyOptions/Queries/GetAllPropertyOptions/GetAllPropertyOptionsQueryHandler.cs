using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllBuilidngTypes
{
    public class GetAllPropertyOptionsQueryHandler(IPropertyOptionsRepository propertyOptionsRepository) : IRequestHandler<GetAllPropertyOptionsQuery, PropertyOptionsModel>
    {
        private readonly IPropertyOptionsRepository _propertyOptionsRepository = propertyOptionsRepository;

        public async Task<PropertyOptionsModel> Handle(GetAllPropertyOptionsQuery request, CancellationToken cancellationToken)
            => await _propertyOptionsRepository.GetAllPropertyOptions();
    }
}
