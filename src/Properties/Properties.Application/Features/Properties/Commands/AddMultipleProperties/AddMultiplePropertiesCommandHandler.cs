using AutoMapper;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddMultipleProperties
{
    public class AddMultiplePropertiesCommandHandler(
        IPropertiesRepository propertiesRepository,
        IMapper mapper)
        : IRequestHandler<AddMultiplePropertiesCommand>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(AddMultiplePropertiesCommand request, CancellationToken cancellationToken)
        {
            var properties = _mapper.Map<IEnumerable<Property>>(request.Properties);
            await _propertiesRepository.AddMultiple(properties);
        }
    }
}
