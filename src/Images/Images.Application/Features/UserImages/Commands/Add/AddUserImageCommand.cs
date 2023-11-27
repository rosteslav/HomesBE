using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Features.UserImages.Commands.Add
{
    public class AddAdditionalUserDataCommand : IRequest<string>
    {
        public IFormFile FormFile { get; set; }
        public string UserId { get; set; }
    }
}
