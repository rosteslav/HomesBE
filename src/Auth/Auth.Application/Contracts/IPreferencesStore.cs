using BuildingMarket.Auth.Application.Models.Security;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IPreferencesStore
    {
        Task SetBuyersPreferences(IDictionary<string, BuyerPreferencesRedisModel> buyersPreferences, CancellationToken cancellationToken);
    }
}
