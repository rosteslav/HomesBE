using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Admins.Application.Features.Admins.Commands.AddMultipleProperties
{
    public class AddMultiplePropertiesCommand : IRequest
    {
        public IFormFile File { get; set; }
    }
}
