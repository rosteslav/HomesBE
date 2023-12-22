using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetBySeller
{
    public class GetBySellerQueryHandler(IPropertiesRepository propertiesRepository, IPropertyImagesStore propertyImagesStore)
        : IRequestHandler<GetBySellerQuery, IEnumerable<PropertyModelWithId>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IPropertyImagesStore _propertyImagesStore = propertyImagesStore;

        public async Task<IEnumerable<PropertyModelWithId>> Handle(GetBySellerQuery request, CancellationToken cancellationToken)
        {
            var properties = await _propertiesRepository.GetBySeller(request.SellerId, cancellationToken);
            if (properties.Any())
            {
                var propertiesIds = properties.Select(p => p.Id.ToString()).ToArray();
                var propertiesImages = await _propertyImagesStore.GetPropertiesImages(propertiesIds);
                if (propertiesImages.Any())
                {
                    int index = 0;
                    foreach (var property in propertiesImages)
                        properties[index++].Images = property.Images;
                }
            }

            return properties;
        }
    }
}