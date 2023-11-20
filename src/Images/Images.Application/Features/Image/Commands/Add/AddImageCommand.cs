using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Features.Image.Commands.Add
{
    public class AddImageCommand : IRequest<string>
    {
        public IFormFile FormFile { get; set; }
        public int PropertyId { get; set; }
    }
}
