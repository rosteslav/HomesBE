using MediatR;

namespace BuildingMarket.Admins.Application.Features.Admins.Queries.GetNeighbourhoodsRegions
{
    public class GetNeighbourhoodsRegionsQuery : IRequest<IDictionary<string, IEnumerable<string>>>
    {
    }
}
