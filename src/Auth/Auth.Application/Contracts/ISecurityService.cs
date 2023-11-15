using BuildingMarket.Auth.Application.Models.Security.Enums;
using System.Security.Claims;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface ISecurityService
    {
        Task<IEnumerable<Claim>> GetLoginClaims(string username, string password);

        Task<RegistrationResult> Registration(string username, string email, string password, IEnumerable<string> roles);
    }
}
