using AutoMapper;
using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.AuthOptions;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Auth.Infrastructure.Persistence;
using BuildingMarket.Common.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class AuthOptionsRepository(
        ApplicationDbContext context,
        IMapper mapper,
        ILogger<AuthOptionsRepository> logger)
        : IAuthOptionsRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthOptionsRepository> _logger = logger;

        public async Task AddPreferences(PreferencesModel model)
        {
            _logger.LogInformation("DB adding user preferences...");
            var preferences = _mapper.Map<Preferences>(model);

            try
            {
                await _context.Preferences.AddAsync(preferences);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding user preferences");
            }
        }

        public async Task<PreferencesOutputModel> GetPreferences()
        {
            _logger.LogInformation("DB retrieving preferences...");

            try
            {
                return new PreferencesOutputModel
                {
                    BuildingTypes = await _context.BuildingTypes.Select(bt => bt.Description).ToArrayAsync(),
                    Purposes = await _context.PreferencesOptions.Select(p => p.Preference).ToArrayAsync(),
                    Regions = await _context.Neighborhoods.Select(n => n.Region).Distinct().ToArrayAsync(),
                    NumberOfRooms = await _context.NumberOfRooms.Select(n => n.Description).Distinct().ToArrayAsync()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving preferences");
            }

            return new PreferencesOutputModel();
        }

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
