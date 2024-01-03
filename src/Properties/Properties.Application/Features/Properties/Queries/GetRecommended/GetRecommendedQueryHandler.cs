using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetRecommended
{
    public class GetRecommendedQueryHandler(
        IPropertiesRepository propertiesRepository,
        IRecommendationRepository recommendationRepository,
        IPropertyImagesStore propertyImagesStore,
        IPreferencesStore preferencesStore,
        IRecommendationStore recommendedStore)
        : IRequestHandler<GetRecommendedQuery, IEnumerable<GetAllPropertiesOutputModel>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IRecommendationRepository _recommendationRepository = recommendationRepository;
        private readonly IPropertyImagesStore _propertyImagesStore = propertyImagesStore;
        private readonly IPreferencesStore _preferencesStore = preferencesStore;
        private readonly IRecommendationStore _recommendedStore = recommendedStore;

        public async Task<IEnumerable<GetAllPropertiesOutputModel>> Handle(GetRecommendedQuery request, CancellationToken cancellationToken)
        {
            var recommendedIds = await _recommendedStore.GetRecommendedByUserId(request.BuyerId, cancellationToken);

            IEnumerable<GetAllPropertiesOutputModel> properties;

            if (recommendedIds != null)
            {
                properties = await _propertiesRepository.GetByIds(recommendedIds, cancellationToken);
            }
            else
            {
                var preferences = await _preferencesStore.GetPreferences(request.BuyerId, cancellationToken);
                properties = await _propertiesRepository.GetByIds(
                    (await _recommendationRepository.GetRecommended(preferences, cancellationToken)).Take(1).ToList(), cancellationToken);
            }

            if (properties.Any())
            {
                var propertiesImages = await _propertyImagesStore.GetPropertiesImages(properties.Select(p => p.Id.ToString()).ToArray());

                properties = properties.Zip(propertiesImages, (prop, img) =>
                {
                    prop.Images = img.Images;
                    return prop;
                });

                return properties;
            }

            return Enumerable.Empty<GetAllPropertiesOutputModel>();
        }
    }
}
