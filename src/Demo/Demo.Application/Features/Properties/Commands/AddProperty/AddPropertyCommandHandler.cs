using AutoMapper;
using Demo.Application.Contracts;
using Demo.Domain.Enities;
using MediatR;

namespace Demo.Application.Features.Properties.Commands.AddProperty
{
    public class AddPropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper) : IRequestHandler<AddPropertyCommand>
    {
        private readonly IPropertyRepository _propertyRepository = propertyRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(AddPropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(request);
            await _propertyRepository.Add(property);
        }
    }
}
