using Demo.Application.Models.Security;
using System.Security.Claims;

namespace Demo.Application.Contracts
{
    public interface ISecurityService
    {
        Task<IEnumerable<Claim>> GetLoginClaims(string username, string password);

        Task<RegistrationResult> Registration(string username, string email, string password, IEnumerable<string> roles);
    }
}
