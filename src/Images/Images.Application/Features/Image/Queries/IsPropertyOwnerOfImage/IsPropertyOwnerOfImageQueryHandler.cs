using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Queries.IsPropertyOwnerOfImage
{
    public class IsPropertyOwnerOfImageQueryHandler(
        IImagesRepository imagesRepository,
        IPropertiesRepository propertiesRepository)
        : IRequestHandler<IsPropertyOwnerOfImageQuery, bool>
    {
        private readonly IImagesRepository _imagesRepository = imagesRepository;
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;


        public async Task<bool> Handle(IsPropertyOwnerOfImageQuery request, CancellationToken cancellationToken)
        {
            var propertyId = await _imagesRepository
                .GetPropertyIdOfImageById(request.ImageId);

            return await _propertiesRepository.PropertyExists(propertyId);
        }
    }
}
