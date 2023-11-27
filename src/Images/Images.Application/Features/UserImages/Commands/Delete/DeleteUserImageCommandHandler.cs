using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using MediatR;

namespace BuildingMarket.Images.Application.Features.UserImages.Commands.Add
{
    public class DeleteUserImageCommandHandler(IAdditionalUserDataRepository userDataRepository, IImgbbService imgbbService) : IRequestHandler<DeleteAdditionalUserDataCommand, string>
    {
        private readonly IAdditionalUserDataRepository _userDataRepository = userDataRepository;

        public async Task<string> Handle(
            DeleteAdditionalUserDataCommand request,
            CancellationToken cancellationToken)
        {
            await _userDataRepository.DeleteUserImage(request.UserId);
            return request.UserId;
        }
    }
}
