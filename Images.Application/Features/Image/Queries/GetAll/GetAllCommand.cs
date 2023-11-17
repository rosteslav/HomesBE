using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Queries.GetAll
{
    public class GetAllCommand
        : IRequest<IEnumerable<Domain.Entities.Image>>
    {
        public int PropertyId { get; set; }
    }
}
