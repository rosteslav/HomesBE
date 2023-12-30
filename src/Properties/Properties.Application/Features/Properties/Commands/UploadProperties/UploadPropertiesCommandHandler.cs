using BuildingMarket.Properties.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.UploadProperties
{
    public class UploadPropertiesCommandHandler(
        IPropertiesRepository repository,
        IPropertiesStore store,
        ILogger<UploadPropertiesCommandHandler> logger)
        : IRequestHandler<UploadPropertiesCommand>
    {
        private readonly IPropertiesRepository _repository = repository;
        private readonly IPropertiesStore _store = store;
        private readonly ILogger<UploadPropertiesCommandHandler> _logger = logger;

        public async Task Handle(UploadPropertiesCommand request, CancellationToken cancellationToken)
        {
            var properties = await _repository.GetForRecommendations(cancellationToken);

            if (properties is not null && properties.Any())
                await _store.UploadProperties(properties, cancellationToken);
            else
                _logger.LogInformation("There are no properties for uploading.");
        }
    }
}
