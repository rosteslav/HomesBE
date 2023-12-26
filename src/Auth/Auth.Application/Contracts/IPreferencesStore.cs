using BuildingMarket.Auth.Application.Models.Security;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IPreferencesStore
    {
        Task SetBuyersPreferences(IEnumerable<BuyerPreferencesRedisModel> buyersPreferences, CancellationToken cancellationToken);
    }
}
