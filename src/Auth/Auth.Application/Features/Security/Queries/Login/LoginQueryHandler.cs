using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Common.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BuildingMarket.Auth.Application.Features.Security.Queries.Login
{
    public class LoginQueryHandler(ISecurityService securityService, JWT jwt) : IRequestHandler<LoginQuery, JwtSecurityToken>
    {
        private readonly ISecurityService _securityService = securityService;
        private readonly JWT _jwt = jwt;

        public async Task<JwtSecurityToken> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var authClaims = await _securityService.GetLoginClaims(request.Model.Username, request.Model.Password);

            if (authClaims == null)
            {
                return null;
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));

            var token = new JwtSecurityToken(
                issuer: _jwt.ValidIssuer,
                audience: _jwt.ValidAudience,
                expires: DateTime.Now.AddHours(_jwt.ValidHours),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
