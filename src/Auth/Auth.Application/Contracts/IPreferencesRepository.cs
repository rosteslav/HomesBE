using BuildingMarket.Common.Models;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IPreferencesRepository
    {
        Task<IDictionary<string, BuyerPreferencesRedisModel>> GetAllBuyersPreferences(CancellationToken cancellationToken);
    }
}
