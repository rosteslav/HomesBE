using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Application.Models.Security.Enums;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class SecurityService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IAdditionalUserDataRepository additionalUserDataRepository,
        IPreferencesRepository preferencesRepository)
        : ISecurityService
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IAdditionalUserDataRepository _additionalUserDataRepository = additionalUserDataRepository;
        private readonly IPreferencesRepository _preferencesRepository = preferencesRepository;

        public async Task<IEnumerable<Claim>> GetLoginClaims(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }

        public async Task<RegistrationResult> Registration(
            RegisterModel model,
            IEnumerable<string> roles,
            PreferencesModel preferences)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return RegistrationResult.AlreadyExists;

            var user = new IdentityUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return RegistrationResult.Failure;

            if (model.FirstName != null ||
                model.LastName != null ||
                model.PhoneNumber != null)
            {
                await _additionalUserDataRepository.AddAsync(new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserId = user.Id
                });
            }

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));

                await _userManager.AddToRoleAsync(user, role);
            }

            if (preferences != null)
                await _preferencesRepository.Add(user.Id, preferences);

            return RegistrationResult.Success;
        }
    }
}
