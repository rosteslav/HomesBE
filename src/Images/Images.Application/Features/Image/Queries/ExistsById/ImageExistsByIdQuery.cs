using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Queries.ExistsById
{
    public class ImageExistsByIdQuery : IRequest<bool>
    {
        public int ImageId { get; set; }
    }
}
