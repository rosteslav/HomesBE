using AutoMapper;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddMultipleProperties.Commands
{
    public class AddMultiplePropertiesCommandHandler(
        IPropertiesRepository propertiesRepository,
        IMapper mapper,
        ILogger<AddMultiplePropertiesCommandHandler> logger)
        : IRequestHandler<AddMultiplePropertiesCommand, Response>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AddMultiplePropertiesCommandHandler> _logger = logger;

        public async Task<Response> Handle(AddMultiplePropertiesCommand request, CancellationToken cancellationToken)
        {
            var properties = _mapper.Map<IEnumerable<Property>>(request.Properties);
            int result = await _propertiesRepository.AddMultiple(properties);

            return new Response { Status = "Success", Message = $"{result} properties have been successfully added" };
        }
    }
}
