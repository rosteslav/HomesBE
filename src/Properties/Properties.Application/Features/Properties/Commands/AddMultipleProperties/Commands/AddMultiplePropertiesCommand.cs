using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddMultipleProperties.Commands
{
    public class AddMultiplePropertiesCommand : IRequest<int>
    {
        public string SellerId { get; set; }
        public IFormFile File { get; set; }
    }
}
