using BuildingMarket.Common.Models;
using BuildingMarket.Properties.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

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
                var buyersRecommendations = new Dictionary<string, IEnumerable<int>>();
                foreach ((string buyerId, BuyerPreferencesRedisModel preferences) in buyersPreferences)
                {
                    var recommendations = await _recommendationRepository.GetRecommended(preferences, cancellationToken);
                    if (recommendations.Any())
                        buyersRecommendations.Add(buyerId, recommendations);
                }

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
