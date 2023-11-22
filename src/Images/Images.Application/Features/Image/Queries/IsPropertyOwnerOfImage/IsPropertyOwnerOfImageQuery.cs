using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Queries.IsPropertyOwnerOfImage
{
    public class IsPropertyOwnerOfImageQuery : IRequest<bool>
    {
        public int ImageId { get; set; }
        public string UserId { get; set; }
    }
}
