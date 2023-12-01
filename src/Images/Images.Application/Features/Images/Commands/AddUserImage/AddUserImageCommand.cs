using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Features.Images.Commands.AddUserImage
{
    public class AddUserImageCommand : IRequest<string>
    {
        public IFormFile FormFile { get; set; }
        public string UserId { get; set; }
    }
}
