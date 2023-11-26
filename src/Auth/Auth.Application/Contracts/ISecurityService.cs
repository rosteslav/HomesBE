using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Application.Models.Security.Enums;
using System.Security.Claims;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface ISecurityService
    {
        Task<IEnumerable<Claim>> GetLoginClaims(string username, string password);

        Task<UserRolesModel> GetUserRoles();

        Task<RegistrationResult> Registration(RegisterModel model, IEnumerable<string> roles);
    }
}
