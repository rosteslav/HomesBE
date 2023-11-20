using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllNeighbourhoods
{
    public class GetAllNeighbourhoodsQuery : IRequest<IEnumerable<string>>
    {
        public string Descripition { get; set; }
    }
}
