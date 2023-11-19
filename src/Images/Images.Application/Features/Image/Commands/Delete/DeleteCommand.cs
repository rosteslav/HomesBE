using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Commands.Delete
{
    public class DeleteCommand : IRequest
    {
        public string DeleteURL { get; set; }
    }
}
