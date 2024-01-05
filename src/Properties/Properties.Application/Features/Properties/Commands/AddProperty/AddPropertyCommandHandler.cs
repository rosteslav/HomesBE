using AutoMapper;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty
{
    public class AddPropertyCommandHandler(
        IPropertiesRepository propertiesRepository,
        INeighbourhoodsRepository neighbourhoodsRepository,
        IPropertiesStore propertiesStore,
        IMapper mapper)
        : IRequestHandler<AddPropertyCommand, AddPropertyOutputModel>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly INeighbourhoodsRepository _neighbourhoodsRepository = neighbourhoodsRepository;
        private readonly IPropertiesStore _propertiesStore = propertiesStore;
        private readonly IMapper _mapper = mapper;

        public async Task<AddPropertyOutputModel> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(request);
            var output = await _propertiesRepository.Add(property);

            var storeModel = _mapper.Map<PropertyRedisModel>(property);
            storeModel.Region = await _neighbourhoodsRepository.GetNeighbourhoodRegion(property.Neighbourhood, cancellationToken);
            await _propertiesStore.UpdateProperty(output.Id, storeModel, cancellationToken);

            return output;
        }
    }
}