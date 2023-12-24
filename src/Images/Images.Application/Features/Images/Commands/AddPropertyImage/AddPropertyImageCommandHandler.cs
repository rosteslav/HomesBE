using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using BuildingMarket.Images.Domain.Entities;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.AddPropertyImage
{
    public class AddPropertyImageCommandHandler(
        IPropertyImagesRepository imagesRepository,
        IPropertyImagesStore imagesStore,
        IPropertiesRepository propertiesRepository,
        IImgbbService imgbbService)
        : IRequestHandler<AddPropertyImageCommand, (string, int)>
    {
        private readonly IPropertyImagesRepository _imagesRepository = imagesRepository;
        private readonly IPropertyImagesStore _imagesStore = imagesStore;
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task<(string, int)> Handle(
            AddPropertyImageCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _propertiesRepository.PropertyExists(request.PropertyId))
            {
                return (string.Empty, default(int));
            }

            string ext = Path.GetExtension(request.FormFile.FileName);
            var imageName = $"{request.PropertyId}-{Guid.NewGuid()}{ext}";

            ImageData imageData = await _imgbbService
                .UploadImage(request.FormFile, imageName);

            if (imageData is null)
            {
                return (string.Empty, default(int));
            }

            var image = new Image
            {
                PropertyId = request.PropertyId,
                ImageURL = imageData.DisplayUrl
            };

            await _imagesRepository.Add(image);

            var propertyImages = await _imagesRepository.GetAllForProperty(request.PropertyId);
            var imagesURLs = propertyImages.Select(img => img.ImageURL);
            await _imagesStore.UpdatePropertyImages(request.PropertyId, imagesURLs, cancellationToken);

            return (image.ImageURL, image.Id);
        }
    }
}
