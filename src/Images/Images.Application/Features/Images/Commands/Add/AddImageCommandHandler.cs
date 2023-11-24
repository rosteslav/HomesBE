using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using BuildingMarket.Images.Domain.Entities;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.Add
{
    public class AddImageCommandHandler(
        IImagesRepository repository,
        IPropertiesRepository propertiesRepository,
        IImgbbService imgbbService)
        : IRequestHandler<AddImageCommand, Tuple<string, int>>
    {
        private readonly IImagesRepository _repository = repository;
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task<Tuple<string, int>> Handle(
            AddImageCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _propertiesRepository.PropertyExists(request.PropertyId))
            {
                return Tuple.Create(string.Empty, default(int));
            }

            string ext = Path.GetExtension(request.FormFile.FileName);
            var imageName = $"{request.PropertyId}-{Guid.NewGuid()}{ext}";

            ImageData imageData = await _imgbbService
                .UploadImage(request.FormFile, imageName);

            if (imageData is null)
            {
                return Tuple.Create(string.Empty, default(int));
            }

            var image = new Image
            {
                PropertyId = request.PropertyId,
                ImageURL = imageData.DisplayUrl
            };
            
            await _repository.Add(image);

            return Tuple.Create(image.ImageURL, image.Id);
        }
    }
}
