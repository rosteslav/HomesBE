﻿using BuildingMarket.Auth.Application.Contracts;
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
        IAuthOptionsRepository authOptionsRepository)
        : ISecurityService
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IAdditionalUserDataRepository _additionalUserDataRepository = additionalUserDataRepository;
        private readonly IAuthOptionsRepository _authOptionsRepository = authOptionsRepository;

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

            if(!string.IsNullOrEmpty(additData?.FirstName)) 
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

            if (model.FirstName != null ||
                model.LastName != null ||
                model.PhoneNumber != null)
            {
                await _additionalUserDataRepository.AddAsync(new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserId = user.Id,
                    ImageURL = model.ImageUrl
                });
            }

            if (model.Purpose != null ||
                model.Region != null ||
                model.BuildingType != null ||
                model.PriceHigherEnd != 0)
            {
                await _authOptionsRepository.AddPreferences(new PreferencesModel
                {
                    UserId = user.Id,
                    Purpose = model.Purpose,
                    Region = model.Region,
                    BuildingType = model.BuildingType,
                    PriceHigherEnd = model.PriceHigherEnd
                });
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
