using MediatR;

namespace BuildingMarket.Images.Application.Features.UserImages.Commands.Add
{
    public class DeleteAdditionalUserDataCommand : IRequest<string>
    {
        public string UserId { get; set; }
    }
}
