using AutoMapper;
using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Infrastructure.Persistence;
using BuildingMarket.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;

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
                var preferences = await _context.Preferences
                    .Select(p => new KeyValuePair<string, BuyerPreferencesRedisModel>(p.UserId, _mapper.Map<BuyerPreferencesRedisModel>(p)))
                    .ToArrayAsync(cancellationToken);

                return preferences.ToFrozenDictionary();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve preferences for all buyers.");
            }

            return default;
        }
    }
}
