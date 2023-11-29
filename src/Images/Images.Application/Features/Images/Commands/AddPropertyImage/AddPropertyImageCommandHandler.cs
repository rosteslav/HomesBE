﻿using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using BuildingMarket.Images.Domain.Entities;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.AddPropertyImage
{
    public class AddPropertyImageCommandHandler(
        IImagesRepository repository,
        IPropertiesRepository propertiesRepository,
        IImgbbService imgbbService)
        : IRequestHandler<AddPropertyImageCommand, (string, int)>
    {
        private readonly IImagesRepository _repository = repository;
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

            await _repository.AddPropertyImage(image);

            return (image.ImageURL, image.Id);
        }
    }
}