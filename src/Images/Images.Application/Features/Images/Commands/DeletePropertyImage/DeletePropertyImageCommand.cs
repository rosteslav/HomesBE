using BuildingMarket.Images.Application.Models.Enums;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.DeletePropertyImage
{
    public class DeletePropertyImageCommand : IRequest<DeleteImageResult>
    {
        public int ImageId { get; set; }
        public string UserId { get; set; }
    }
}
