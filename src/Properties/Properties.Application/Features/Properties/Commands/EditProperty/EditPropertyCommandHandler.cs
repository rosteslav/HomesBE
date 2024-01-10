using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.EditProperty
{
    public class EditPropertyCommandHandler(IPropertiesRepository propertiesRepository) 
        : IRequestHandler<EditPropertyCommand, PropertyResult>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<PropertyResult> Handle(EditPropertyCommand request, CancellationToken cancellationToken)
        {
            if (!await _propertiesRepository.Exists(request.PropertyId, cancellationToken))
                return PropertyResult.NotFound;

            if (!await _propertiesRepository.IsOwner(request.UserId, request.PropertyId, cancellationToken))
                return PropertyResult.Unauthorized;

            await _propertiesRepository.EditById(request.PropertyId, request.EditedProperty, cancellationToken);

            return PropertyResult.Success;
        }
    }
}
