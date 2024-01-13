using AutoMapper;
using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Application.Models.Security.Enums;
using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Common.Models;
using BuildingMarket.Common.Models.Security;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class SecurityService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IAdditionalUserDataRepository additionalUserDataRepository,
        IAuthOptionsRepository authOptionsRepository,
        IPreferencesStore preferencesStore,
        IMapper mapper)
        : ISecurityService
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IAdditionalUserDataRepository _additionalUserDataRepository = additionalUserDataRepository;
        private readonly IAuthOptionsRepository _authOptionsRepository = authOptionsRepository;
        private readonly IPreferencesStore _preferencesStore = preferencesStore;
        private readonly IMapper _mapper = mapper;

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
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var additData = await _additionalUserDataRepository.GetById(user.Id);

            if (!string.IsNullOrEmpty(additData?.FirstName))
                authClaims.Add(new Claim(ClaimTypes.Name, additData.FirstName));
            if (!string.IsNullOrEmpty(additData?.LastName))
                authClaims.Add(new Claim(ClaimTypes.Surname, additData.LastName));
            if (!string.IsNullOrEmpty(additData?.PhoneNumber))
                authClaims.Add(new Claim(ClaimTypes.MobilePhone, additData.PhoneNumber));
            if (!string.IsNullOrEmpty(additData?.ImageURL))
                authClaims.Add(new Claim(ClaimTypes.Uri, additData.ImageURL));

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }

        public async Task<RegistrationResult> Registration(RegisterModel model, IEnumerable<string> roles)
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

            if (roles.Contains(UserRoles.Seller) ||
                roles.Contains(UserRoles.Broker))
            {
                var addUserData = _mapper.Map<AdditionalUserData>(model);
                addUserData.UserId = user.Id;
                await _additionalUserDataRepository.Add(addUserData);
            }

            if (roles.Contains(UserRoles.Buyer))
            {
                var preferences = _mapper.Map<PreferencesModel>(model);
                preferences.UserId = user.Id;
                
                await _authOptionsRepository.AddPreferences(preferences);

                var buyerPreferences = _mapper.Map<BuyerPreferencesRedisModel>(preferences);

                await _preferencesStore.SetRegisteredBuyerPreferences(
                    user.Id,
                    buyerPreferences);
            }

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));

                await _userManager.AddToRoleAsync(user, role);
            }

            return RegistrationResult.Success;
        }
    }
}
