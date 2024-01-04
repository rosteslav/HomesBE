using BuildingMarket.Common.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface INeighbourhoodsRepository
    {
        Task<NeighbourhoodsRatingModel> GetRating(CancellationToken cancellationToken = default);
    }
}
