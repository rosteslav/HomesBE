using BuildingMarket.Properties.Application.Models.Security;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPreferencesStore
    {
        Task<PreferencesModel> GetPreferences(string buyerId, CancellationToken cancellationToken);
    }
}
