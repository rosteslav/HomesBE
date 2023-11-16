using BuildingMarket.Admins.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Admins.Application.Features.Admins.Commands.AddMultipleProperties
{
    public class AddMultiplePropertiesCommand : IRequest<Response>
    {
        public string JWT { get; set; }
        public IFormFile File { get; set; }
    }
}
