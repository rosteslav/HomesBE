using BuildingMarket.Images.Application.Models;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Commands.Add
{
    public class AddCommand : IRequest<string>
    {
        public ImageDTO Image { get; set; }
        public int PropertyId { get; set; }
    }
}
