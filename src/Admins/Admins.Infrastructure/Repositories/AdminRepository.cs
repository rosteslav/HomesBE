using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Common.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Admins.Infrastructure.Repositories
{
    public class AdminRepository(UserManager<IdentityUser> userManager, ILogger<AdminRepository> logger) : IAdminRepository
    {
        private readonly ILogger<AdminRepository> _logger = logger;
        private readonly UserManager<IdentityUser> _userManager = userManager;

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
