using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Commands.Add
{
    public class AddImageCommandHandler(
        IImagesRepository repository,
        IImgbbService imgbbService)
        : IRequestHandler<AddImageCommand, string>
    {
        private readonly IImagesRepository _repository = repository;
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task<string> Handle(
            AddImageCommand request,
            CancellationToken cancellationToken)
        {
            string ext = Path.GetExtension(request.FormFile.FileName);
            var imageName = $"{request.PropertyId}-{Guid.NewGuid()}{ext}";

            ImageData imageData = await _imgbbService
                .UploadImage(request.FormFile, imageName);

            if (imageData is null)
            {
                return string.Empty;
            }

            await _repository.Add(new()
            {
                PropertyId = request.PropertyId,
                ImageName = imageName,
                ImageURL = imageData.DisplayUrl,
                DeleteURL = imageData.DeleteUrl
            });

            return imageData.DisplayUrl;
        }
    }
}
