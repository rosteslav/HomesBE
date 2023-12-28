using BuildingMarket.Common.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPreferencesStore
    {
        Task<BuyerPreferencesRedisModel> GetPreferences(string buyerId, CancellationToken cancellationToken);
    }
}
