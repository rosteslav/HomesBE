using Demo.Application.Contracts;
using Demo.Domain.Enities;
using MediatR;

namespace Demo.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository) : IRequestHandler<GetAllPropertiesQuery, IEnumerable<Property>>
    {
        private readonly IPropertyRepository _propertyRepository = propertyRepository;

        public async Task<IEnumerable<Property>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
            => await _propertyRepository.GetAll();
    }
}
