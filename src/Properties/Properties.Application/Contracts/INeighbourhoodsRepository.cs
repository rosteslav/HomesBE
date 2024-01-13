using BuildingMarket.Common.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface INeighbourhoodsRepository
    {
        Task<string> GetNeighbourhoodRegion(string neighbourhood, CancellationToken cancellationToken = default);

        Task<NeighbourhoodsRatingModel> GetRating(CancellationToken cancellationToken = default);
    }
}
