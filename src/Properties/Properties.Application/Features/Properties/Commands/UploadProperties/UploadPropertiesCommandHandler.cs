using BuildingMarket.Properties.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.UploadProperties
{
    public class UploadPropertiesCommandHandler(
        IPropertiesRepository repository,
        IPropertiesStore propertiesStore,
        IPropertyImagesStore imagesStore,
        ILogger<UploadPropertiesCommandHandler> logger)
        : IRequestHandler<UploadPropertiesCommand>
    {
        private readonly IPropertiesRepository _repository = repository;
        private readonly IPropertiesStore _propertiesStore = propertiesStore;
        private readonly IPropertyImagesStore _imagesStore = imagesStore;
        private readonly ILogger<UploadPropertiesCommandHandler> _logger = logger;

        public async Task Handle(UploadPropertiesCommand request, CancellationToken cancellationToken)
        {
            var properties = await _repository.GetForRecommendations(cancellationToken);
            var imagesCount = await _imagesStore.GetPropertyIdsWithImagesCount(cancellationToken);
            foreach (var property in properties)
            {
                if (imagesCount.TryGetValue(property.Id, out int count))
                    property.NumberOfImages = count;
            }

            if (properties is not null && properties.Any())
                await _propertiesStore.UploadProperties(properties, cancellationToken);
            else
                _logger.LogInformation("There are no properties for uploading.");
        }
    }
}
