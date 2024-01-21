using AutoMapper;
using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Infrastructure.Persistence;
using BuildingMarket.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class PreferencesRepository(ApplicationDbContext context, IMapper mapper, ILogger<PreferencesRepository> logger) : IPreferencesRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<PreferencesRepository> _logger = logger;

        public async Task<IDictionary<string, BuyerPreferencesRedisModel>> GetAllBuyersPreferences(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve preferences for all buyers.");

            try
            {
                var preferences = await _context.Preferences.ToDictionaryAsync(
                    m => m.UserId,
                    _mapper.Map<BuyerPreferencesRedisModel>,
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
