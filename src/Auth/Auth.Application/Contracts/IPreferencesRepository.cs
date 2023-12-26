using BuildingMarket.Auth.Application.Models.Security;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IPreferencesRepository
    {
        Task<IEnumerable<BuyerPreferencesRedisModel>> GetAllBuyersPreferences(CancellationToken cancellationToken);
    }
}
