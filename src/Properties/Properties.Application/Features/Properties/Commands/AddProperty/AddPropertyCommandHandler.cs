using AutoMapper;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty
{
    public class AddPropertyCommandHandler(
        IPropertiesRepository propertiesRepository,
        IMapper mapper)
        : IRequestHandler<AddPropertyCommand, AddPropertyOutputModel>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<AddPropertyOutputModel> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(request);
            return await _propertiesRepository.Add(property);
        }
    }
}