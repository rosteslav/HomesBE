using BuildingMarket.Images.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Features.Images.Commands.EditUserImage
{
    public class EditUserImageCommand : IRequest<ImageOutputModel>
    {
        public IFormFile FormFile { get; set; }
        public string UserId { get; set; }
    }
}
