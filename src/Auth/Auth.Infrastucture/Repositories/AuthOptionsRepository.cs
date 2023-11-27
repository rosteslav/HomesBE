using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.AuthOptions;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Infrastructure.Persistence;
using BuildingMarket.Common.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class AuthOptionsRepository(
        ApplicationDbContext context,
        ILogger<AuthOptionsRepository> logger)
        : IAuthOptionsRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<AuthOptionsRepository> _logger = logger;

        public async Task<IEnumerable<BrokerModel>> GetAllBrokers()
        {
            _logger.LogInformation("DB get all brokers");

            try
            {
                var query = from user in _context.Users
                            join userRole in _context.UserRoles on user.Id equals userRole.UserId
                            join role in _context.Roles.Where(r => r.Name == UserRoles.Broker) on userRole.RoleId equals role.Id
                            join userData in _context.AdditionalUserData on user.Id equals userData.UserId
                            select new BrokerModel
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirstName = userData.FirstName,
                                LastName = userData.LastName,
                                PhoneNumber = userData.PhoneNumber
                            };

                return await query.ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting brokers");
            }

            return Enumerable.Empty<BrokerModel>();
        }

        public async Task<UserRolesModel> GetUserRoles()
        {
            await Task.Yield();

            return new UserRolesModel
            {
                Roles = [UserRoles.Buyer, UserRoles.Seller, UserRoles.Broker]
            };
        }
    }
}
