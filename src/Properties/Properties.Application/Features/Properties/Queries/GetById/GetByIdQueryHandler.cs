using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetById
{
    public class GetByIdQueryHandler(IPropertiesRepository propertiesRepository, IPropertyImagesStore propertyImagesStore)
        : IRequestHandler<GetByIdQuery, PropertyModel>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IPropertyImagesStore _propertyImagesStore = propertyImagesStore;

        public async Task<PropertyModel> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _propertiesRepository.GetById(request.Id, cancellationToken);
            if (property is not null)
            {
                var propertiesImages = await _propertyImagesStore.GetPropertiesImages(request.Id.ToString());
                if (propertiesImages.Any())
                    property.Images = propertiesImages.Single().Images;
            }

            return property;
        }
    }
}
