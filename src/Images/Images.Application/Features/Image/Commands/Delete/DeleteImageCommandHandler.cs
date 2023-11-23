using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models.Enums;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Commands.Delete
{
    public class DeleteImageCommandHandler(
        IImagesRepository imagesRepository,
        IPropertiesRepository propertiesRepository)
        : IRequestHandler<DeleteImageCommand, DeleteImageResult>
    {
        private readonly IImagesRepository _imagesRepository = imagesRepository;
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<DeleteImageResult> Handle(DeleteImageCommand request,
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
            return DeleteImageResult.Success;
        }
    }
}
