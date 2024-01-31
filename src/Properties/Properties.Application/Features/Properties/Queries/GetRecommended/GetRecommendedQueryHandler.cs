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

            if (!recommendedIds.Any())
            {
                var preferences = _preferencesStore.GetPreferences(request.BuyerId, cancellationToken);
                recommendedIds = await _recommendationRepository.GetRecommended(await preferences, cancellationToken);
            }

            var properties = await _propertiesRepository.GetByIds(recommendedIds, cancellationToken);

            if (properties.Any())
            {
                var propertiesImages = _propertyImagesStore.GetPropertiesImages(properties.Select(p => p.Id.ToString()).ToArray());

                properties = properties.Zip(await propertiesImages, (prop, img) =>
                {
                    prop.Images = img.Images.Distinct();
                    return prop;
                });

                return properties;
            }

            return Enumerable.Empty<GetAllPropertiesOutputModel>();
        }
    }
}
