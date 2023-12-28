using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetRecommended
{
    public class GetRecommendedQueryHandler(
        IPropertiesRepository propertiesRepository,
        IRecommendationRepository recommendationRepository,
        IPropertyImagesStore propertyImagesStore,
        IPreferencesStore preferencesStore)
        : IRequestHandler<GetRecommendedQuery, IEnumerable<GetAllPropertiesOutputModel>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IRecommendationRepository _recommendationRepository = recommendationRepository;
        private readonly IPropertyImagesStore _propertyImagesStore = propertyImagesStore;
        private readonly IPreferencesStore _preferencesStore = preferencesStore;

        public async Task<IEnumerable<GetAllPropertiesOutputModel>> Handle(GetRecommendedQuery request, CancellationToken cancellationToken)
        {
            var preferences = await _preferencesStore.GetPreferences(request.BuyerId, cancellationToken);
            var recommendedIds = await _recommendationRepository.GetRecommended(preferences, cancellationToken);
            
            var properties = await _propertiesRepository.GetByIds(recommendedIds, cancellationToken);
            if (properties.Any())
            {
                var propertyIds = properties.Select(p => p.Id.ToString()).ToArray();
                var propertiesImages = await _propertyImagesStore.GetPropertiesImages(propertyIds);
                if (propertiesImages.Any())
                {
                    for (int i = 0; i < propertiesImages.Count(); i++)
                        properties.ElementAt(i).Images = propertiesImages.ElementAt(i).Images;
                }
            }

            return properties;
        }
    }
}
