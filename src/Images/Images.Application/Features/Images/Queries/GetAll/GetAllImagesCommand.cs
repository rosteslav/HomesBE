using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Queries.GetAll
{
    public class GetAllImagesCommand
        : IRequest<IEnumerable<Domain.Entities.Image>>
    {
        public int PropertyId { get; set; }
    }
}
