using MediatR;

namespace BuildingMarket.Images.Application.Features.Property.Queries.IsPropertyOwner
{
    public class IsPropertyOwnerQuery : IRequest<bool>
    {
        public int PropertyId { get; set; }
        public string UserId { get; set; }
    }
}
