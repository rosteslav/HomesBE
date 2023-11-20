using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Commands.Delete
{
    public class DeleteImageCommand : IRequest
    {
        public int Id { get; set; }
    }
}
