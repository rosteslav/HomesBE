using BuildingMarket.Common.Models;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class RecommendationRepository(
        IConfiguration configuration,
        PropertiesDbContext context,
        ILogger<RecommendationRepository> logger)
        : IRecommendationRepository
    {
        private readonly int RecommendedCount = configuration.GetValue<int>("PropertiesRecommendedCount");
        private readonly PropertiesDbContext _context = context;
        private readonly ILogger<RecommendationRepository> _logger = logger;

        public async Task<IEnumerable<int>> GetRecommended(BuyerPreferencesRedisModel preferences, CancellationToken cancellationToken)
        {
            // NOTE: If there are no preferences for the given buyer, the "preferences" argument will be null.

            _logger.LogInformation("DB get recommended properties");

            try
            {
                var properties = await _context.Properties
                    .Select(p => p.Id)
                    .OrderBy(id => id)
                    .Take(RecommendedCount)
                    .ToArrayAsync(cancellationToken);

                return properties;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to retrieve recommended properties.");
            }

            return Enumerable.Empty<int>();
        }
    }
}
