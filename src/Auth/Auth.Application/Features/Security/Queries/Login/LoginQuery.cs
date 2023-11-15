using BuildingMarket.Auth.Application.Models.Security;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace BuildingMarket.Auth.Application.Features.Security.Queries.Login
{
    public class LoginQuery : IRequest<JwtSecurityToken>
    {
        public LoginModel Model { get; set; }
    }
}
