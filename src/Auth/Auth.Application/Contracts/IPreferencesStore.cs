using BuildingMarket.Auth.Application.Models.Security;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IPreferencesStore
    {
        Task SetBuyersPreferences(IEnumerable<PreferencesRedisModel> buyersPreferences, CancellationToken cancellationToken);
    }
}
