using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Commands.Add
{
    public class AddCommandHandler(
        IImagesRepository repository,
        IImgbbService imgbbService)
        : IRequestHandler<AddCommand, string>
    {
        private readonly IImagesRepository _repository = repository;
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task<string> Handle(
            AddCommand request,
            CancellationToken cancellationToken)
        {
            ImageData imageData = await _imgbbService
                .UploadImage(request.Image);

            if (imageData is null)
            {
                return string.Empty;
            }

            await _repository.Add(new()
            {
                PropertyId = request.PropertyId,
                ImageName = $"{request.PropertyId}-{Guid.NewGuid()}{request.Image.FileExtension}",
                ImageURL = imageData.DisplayUrl,
                DeleteURL = imageData.DeleteUrl
            });

            return imageData.DisplayUrl;
        }
    }
}
