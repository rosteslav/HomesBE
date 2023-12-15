using BuildingMarket.Auth.Application.Models.AuthOptions;
using BuildingMarket.Auth.Application.Models.Security;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IAuthOptionsRepository
    {
        Task AddPreferences(PreferencesModel model);

        Task<PreferencesOutputModel> GetPreferences();

        Task<IEnumerable<BrokerModel>> GetAllBrokers();

        Task<UserRolesModel> GetUserRoles();
    }
}
