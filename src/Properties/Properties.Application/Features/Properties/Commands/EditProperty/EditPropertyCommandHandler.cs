using AutoMapper;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.EditProperty
{
    public class EditPropertyCommandHandler(
        IPropertiesRepository propertiesRepository,
        IPropertiesStore propertiesStore,
        INeighbourhoodsRepository neighbourhoodsRepository,
        IMapper mapper) 
        : IRequestHandler<EditPropertyCommand, PropertyResult>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IPropertiesStore _propertiesStore = propertiesStore;
        private readonly INeighbourhoodsRepository _neighbourhoodsRepository = neighbourhoodsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PropertyResult> Handle(EditPropertyCommand request, CancellationToken cancellationToken)
        {
            if (!await _propertiesRepository.Exists(request.PropertyId))
                return PropertyResult.NotFound;

            if (!await _propertiesRepository.IsOwner(request.UserId, request.PropertyId))
                return PropertyResult.Unauthorized;

            await _propertiesRepository.EditById(request.PropertyId, request.EditedProperty);

            var storeModel = _mapper.Map<PropertyRedisModel>(request.EditedProperty);
            storeModel.Region = await _neighbourhoodsRepository.GetNeighbourhoodRegion(request.EditedProperty.Neighbourhood, cancellationToken);
            await _propertiesStore.UpdateProperty(request.PropertyId, storeModel, cancellationToken);

            return PropertyResult.Success;
        }
    }
}
