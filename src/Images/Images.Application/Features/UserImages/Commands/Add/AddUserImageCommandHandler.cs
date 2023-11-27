using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using MediatR;

namespace BuildingMarket.Images.Application.Features.UserImages.Commands.Add
{
    public class AddUserImageCommandHandler(IAdditionalUserDataRepository userDataRepository, IImgbbService imgbbService) : IRequestHandler<AddAdditionalUserDataCommand, string>
    {
        private readonly IAdditionalUserDataRepository _userDataRepository = userDataRepository;
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task<string> Handle(
            AddAdditionalUserDataCommand request,
            CancellationToken cancellationToken)
        {
            string ext = Path.GetExtension(request.FormFile.FileName);
            var imageName = $"{request.UserId}-{Guid.NewGuid()}{ext}";

            ImageData imageData = await _imgbbService
                .UploadImage(request.FormFile, imageName);

            if (imageData is null)
            {
                return (string.Empty);
            }

            var userData = new AdditionalUserData
            {
                UserId = request.UserId,
                ImageUrl = imageData.DisplayUrl
            };

            await _userDataRepository.AddUserImage(userData);

            return userData.UserId;
        }
    }
}
