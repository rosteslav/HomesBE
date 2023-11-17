using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Admins.Domain.Entities;
using BuildingMarket.Admins.Infrastructure.Persistence;
using BuildingMarket.Common.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Admins.Infrastructure.Repositories
{
    public class AdminRepository(
        AdminsDbContext context,
        UserManager<IdentityUser> userManager,
        ILogger<AdminRepository> logger) 
        : IAdminRepository
    {
        private readonly AdminsDbContext _context = context;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly ILogger<AdminRepository> _logger = logger;

        public async Task AddMultipleProperties(IEnumerable<Property> properties)
        {
            _logger.LogInformation($"DB add multiple properties");

            try
            {
                await _context.Properties.AddRangeAsync(properties);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"DB the properties have been successfully added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding multiple properties");
            }
        }

        public async Task<IEnumerable<IdentityUser>> GetAllBrokers()
        {
            _logger.LogInformation("DB get all brokers");

            try
            {
                var brokers = await _userManager.GetUsersInRoleAsync(UserRoles.Broker);
                return brokers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting brokers");
            }

            return Enumerable.Empty<IdentityUser>();
        }
    }
}
