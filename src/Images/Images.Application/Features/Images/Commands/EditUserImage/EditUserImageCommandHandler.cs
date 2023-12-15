using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.EditUserImage
{
    public class EditUserImageCommandHandler(
        IUserImagesRepository repository,
        IImgbbService imgbbService) : IRequestHandler<EditUserImageCommand, ImageData>
    {
        private readonly IUserImagesRepository _repository = repository;
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task<ImageData> Handle(
            EditUserImageCommand request,
            CancellationToken cancellationToken)
        {
            string ext = Path.GetExtension(request.FormFile.FileName);
            var imageName = $"{request.UserId}-{Guid.NewGuid()}{ext}";

            ImageData imageData = await _imgbbService
                .UploadImage(request.FormFile, imageName);

            if (imageData is null)
            {
                return new ImageData();
            }

            await _repository.Add(imageData.DisplayUrl, request.UserId);

            return imageData;
        }
    }
}
