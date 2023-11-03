using Demo.Application.Models.Security;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace Demo.Application.Features.Security.Queries.Login
{
    public class LoginQuery : IRequest<JwtSecurityToken>
    {
        public LoginModel Model { get; set; }
    }
}
