using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class PreferencesRepository(ApplicationDbContext context, ILogger<PreferencesRepository> logger) : IPreferencesRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<PreferencesRepository> _logger = logger;

        public async Task<IEnumerable<PreferencesRedisModel>> GetAllBuyersPreferences(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve preferences for all buyers.");

            try
            {
                var preferences = await _context.Preferences
                    .Select(pref => new PreferencesRedisModel
                    {
                        UserId = pref.UserId,
                        Purpose = pref.Purpose,
                        Region = pref.Region,
                        BuildingType = pref.BuildingType,
                        PriceHigherEnd = pref.PriceHigherEnd
                    })
                    .ToArrayAsync(cancellationToken);

                return preferences;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve preferences for all buyers.");
            }

            return Enumerable.Empty<PreferencesRedisModel>();
        }
    }
}
