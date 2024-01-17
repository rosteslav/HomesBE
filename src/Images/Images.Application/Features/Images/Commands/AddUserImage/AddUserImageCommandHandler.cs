using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.AddUserImage
{
    public class AddUserImageCommandHandler(IImgbbService imgbbService) : IRequestHandler<AddUserImageCommand, ImageOutputModel>
    {
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task<ImageOutputModel> Handle(AddUserImageCommand request, CancellationToken cancellationToken)
        {
            string ext = Path.GetExtension(request.FormFile.FileName);
            var imageName = $"UserImg-{Guid.NewGuid()}{ext}";

            ImageOutputModel output = await _imgbbService.UploadImage(request.FormFile, imageName);

            if (output is null)
            {
                return new();
            }

            return output;
        }
    }
}
