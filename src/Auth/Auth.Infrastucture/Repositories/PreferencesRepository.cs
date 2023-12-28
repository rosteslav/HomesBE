using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Infrastructure.Persistence;
using BuildingMarket.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class PreferencesRepository(ApplicationDbContext context, ILogger<PreferencesRepository> logger) : IPreferencesRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<PreferencesRepository> _logger = logger;

        public async Task<IDictionary<string, BuyerPreferencesRedisModel>> GetAllBuyersPreferences(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve preferences for all buyers.");

            try
            {
                var preferences = await _context.Preferences
                    .ToDictionaryAsync(
                        m => m.UserId,
                        m => new BuyerPreferencesRedisModel
                        {
                            Purpose = m.Purpose,
                            Region = m.Region,
                            BuildingType = m.BuildingType,
                            PriceHigherEnd = m.PriceHigherEnd
                        },
                        cancellationToken);

                return preferences;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve preferences for all buyers.");
            }

            return default;
        }
    }
}
