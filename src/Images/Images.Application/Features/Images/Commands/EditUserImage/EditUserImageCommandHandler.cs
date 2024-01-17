using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.EditUserImage
{
    public class EditUserImageCommandHandler(
        IUserImagesRepository repository,
        IImgbbService imgbbService) : IRequestHandler<EditUserImageCommand, ImageOutputModel>
    {
        private readonly IUserImagesRepository _repository = repository;
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task<ImageOutputModel> Handle(
            EditUserImageCommand request,
            CancellationToken cancellationToken)
        {
            string ext = Path.GetExtension(request.FormFile.FileName);
            var imageName = $"{request.UserId}-{Guid.NewGuid()}{ext}";

            ImageOutputModel output = await _imgbbService
                .UploadImage(request.FormFile, imageName);

            if (output is null)
            {
                return new();
            }

            await _repository.Add(output.DisplayUrl, request.UserId);

            return output;
        }
    }
}
