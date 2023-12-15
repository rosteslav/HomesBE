using BuildingMarket.Images.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Features.Images.Commands.AddUserImage
{
    public class AddUserImageCommand : IRequest<ImageData>
    {
        public IFormFile FormFile { get; set; }
    }
}
