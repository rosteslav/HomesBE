using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.DeleteUserImage
{
    public class DeleteUserImageCommand : IRequest
    {
        public string UserId { get; set; }
    }
}
