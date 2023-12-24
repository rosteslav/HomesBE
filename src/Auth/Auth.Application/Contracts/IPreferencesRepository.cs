using BuildingMarket.Auth.Application.Models.Security;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IPreferencesRepository
    {
        Task<IEnumerable<PreferencesRedisModel>> GetAllBuyersPreferences(CancellationToken cancellationToken);
    }
}
