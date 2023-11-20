using MediatR;

namespace BuildingMarket.Images.Application.Features.Property.Queries.PropertyExists
{
    public class PropertyExistsQuery : IRequest<bool>
    {
        public int PropertyId { get; set; }
    }
}
