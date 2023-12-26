using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetRecommended
{
    public class GetRecommendedQueryHandler(IPropertiesRepository repository, IPropertyImagesStore propertyImagesStore) 
        : IRequestHandler<GetRecommendedQuery, IEnumerable<GetAllPropertiesOutputModel>>
    {
        private readonly IPropertiesRepository _repository = repository;
        private readonly IPropertyImagesStore _propertyImagesStore = propertyImagesStore;

        public async Task<IEnumerable<GetAllPropertiesOutputModel>> Handle(GetRecommendedQuery request, CancellationToken cancellationToken)
        {
            var properties = await _repository.GetRecommended(cancellationToken);
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
