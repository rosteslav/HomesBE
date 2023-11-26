﻿using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Application.Models.Security.Enums;
using BuildingMarket.Common.Models.Security;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class SecurityService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IAdditionalUserDataRepository repository)
        : ISecurityService
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IAdditionalUserDataRepository _repository = repository;

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

        public async Task<UserRolesModel> GetUserRoles()
        {
            await Task.Yield();

            return new UserRolesModel
            {
                Roles = [UserRoles.Buyer, UserRoles.Seller, UserRoles.Broker]
            };
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
                await _repository.AddAsync(new()
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

            return RegistrationResult.Success;
        }
    }
}
