using BuildingMarket.Auth.Application.Models.AuthOptions;
using BuildingMarket.Auth.Application.Models.Security;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IAuthOptionsRepository
    {
        Task<IEnumerable<BrokerModel>> GetAllBrokers();

        Task<UserRolesModel> GetUserRoles();
    }
}
