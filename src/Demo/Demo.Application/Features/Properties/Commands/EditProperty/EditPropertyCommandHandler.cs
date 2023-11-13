using AutoMapper;
using Demo.Application.Contracts;
using Demo.Domain.Enities;
using MediatR;

namespace Demo.Application.Features.Properties.Commands.EditProperty
{
    public class EditPropertyCommandHandler(IPropertyRepository repository, IMapper mapper) : IRequestHandler<EditPropertyCommand>
    {
        private readonly IPropertyRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(EditPropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(request);

            await _repository.Update(property.Id, property);
        }
    }
}
