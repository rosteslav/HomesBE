using BuildingMarket.Common.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPreferencesStore
    {
        Task<IDictionary<string, BuyerPreferencesRedisModel>> GetAllBuyersPreferences(CancellationToken cancellationToken);

        Task<BuyerPreferencesRedisModel> GetPreferences(string buyerId, CancellationToken cancellationToken);
    }
}
