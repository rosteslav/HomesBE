using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Features.Images.Commands.AddUserImage
{
    public class AddUserImageCommand : IRequest<string>
    {
        public string UserId { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
