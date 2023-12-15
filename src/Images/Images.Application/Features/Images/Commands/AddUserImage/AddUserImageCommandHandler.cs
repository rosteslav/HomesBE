using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.AddUserImage
{
    public class AddUserImageCommandHandler(IImgbbService imgbbService) : IRequestHandler<AddUserImageCommand, ImageData>
    {
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task<ImageData> Handle(AddUserImageCommand request, CancellationToken cancellationToken)
        {
            string ext = Path.GetExtension(request.FormFile.FileName);
            var imageName = $"UserImg-{Guid.NewGuid()}{ext}";

            ImageData imageData = await _imgbbService.UploadImage(request.FormFile, imageName);

            if (imageData is null)
            {
                return new ImageData();
            }

            return imageData;
        }
    }
}
