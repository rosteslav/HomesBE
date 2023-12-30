using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommand : IRequest<DeletePropertyResult>
    {
        public int PropertyId { get; set; }

        public string UserId { get; set; }

        public bool IsAdmin { get; set; }
    }
}
