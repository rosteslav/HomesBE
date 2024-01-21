using BuildingMarket.Properties.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.UploadRecommendations
{
    public class UploadRecommendationsCommandHandler(
        IPreferencesStore preferencesStore,
        IRecommendationRepository recommendationRepository,
        IRecommendationStore recommendationStore,
        ILogger<UploadRecommendationsCommandHandler> logger)
        : IRequestHandler<UploadRecommendationsCommand>
    {
        private readonly IPreferencesStore _preferencesStore = preferencesStore;
        private readonly IRecommendationRepository _recommendationRepository = recommendationRepository;
        private readonly IRecommendationStore _recommendationStore = recommendationStore;
        private readonly ILogger<UploadRecommendationsCommandHandler> _logger = logger;

        public async Task Handle(UploadRecommendationsCommand request, CancellationToken cancellationToken)
        {
            var buyersPreferences = await _preferencesStore.GetAllBuyersPreferences(cancellationToken);
            if (buyersPreferences is not null && buyersPreferences.Any())
            {
                var buyersRecommendations = buyersPreferences.ToFrozenDictionary(
                    b => b.Key,
                    b => _recommendationRepository.GetRecommended(b.Value, cancellationToken).Result);

                if (buyersRecommendations.Count > 0)
                    await _recommendationStore.UploadRecommendations(buyersRecommendations, cancellationToken);
                else
                    _logger.LogInformation("There are no recommendations for uploading.");
            }
            else
            {
                _logger.LogInformation("There are no buyer's preferences on which to upload recommendations.");
            }
        }
    }
}
