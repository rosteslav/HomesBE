using BuildingMarket.Common.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IRecommendationRepository
    {
        Task<IEnumerable<int>> GetRecommended(BuyerPreferencesRedisModel preferences, CancellationToken cancellationToken);
    }
}
