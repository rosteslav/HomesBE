using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Features.Images.Commands.AddPropertyImage
{
    public class AddPropertyImageCommand : IRequest<(string, int)>
    {
        public IFormFile FormFile { get; set; }
        public int PropertyId { get; set; }
        public string UserId { get; set; }
    }
}
