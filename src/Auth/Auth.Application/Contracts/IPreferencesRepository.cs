using BuildingMarket.Auth.Application.Models.Security;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IPreferencesRepository
    {
        Task<IDictionary<string, BuyerPreferencesRedisModel>> GetAllBuyersPreferences(CancellationToken cancellationToken);
    }
}
