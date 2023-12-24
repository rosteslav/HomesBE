using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models.Enums;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.DeletePropertyImage
{
    public class DeletePropertyImageCommandHandler(
        IPropertyImagesRepository imagesRepository,
        IPropertyImagesStore imagesStore,
        IPropertiesRepository propertiesRepository)
        : IRequestHandler<DeletePropertyImageCommand, DeleteImageResult>
    {
        private readonly IPropertyImagesRepository _imagesRepository = imagesRepository;
        private readonly IPropertyImagesStore _imagesStore = imagesStore;
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<DeleteImageResult> Handle(DeletePropertyImageCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _imagesRepository.Exists(request.ImageId))
            {
                return DeleteImageResult.ImageNotFound;
            }

            var propertyId = await _imagesRepository
                .GetPropertyIdOfImageById(request.ImageId);

            if (!await _propertiesRepository.PropertyExists(propertyId))
            {
                return DeleteImageResult.PropertyNotFound;
            }

            if (!await _propertiesRepository.IsPropertyOwner(propertyId, request.UserId))
            {
                return DeleteImageResult.UserHasNoAccess;
            }

            await _imagesRepository.Delete(request.ImageId);

            var propertyImages = await _imagesRepository.GetAllForProperty(propertyId);
            var imagesURLs = propertyImages.Select(img => img.ImageURL);
            await _imagesStore.UpdatePropertyImages(propertyId, imagesURLs, cancellationToken);

            return DeleteImageResult.Success;
        }
    }
}
