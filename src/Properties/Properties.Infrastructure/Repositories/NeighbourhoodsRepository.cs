using BuildingMarket.Common.Models;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class NeighbourhoodsRepository(PropertiesDbContext context, ILogger<NeighbourhoodsRepository> logger) : INeighbourhoodsRepository
    {
        private readonly PropertiesDbContext _context = context;
        private readonly ILogger<NeighbourhoodsRepository> _logger = logger;

        public async Task<string> GetNeighbourhoodRegion(string neighbourhood, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DB getting neighbourhood's region...");

            try
            {
                return await _context.Neighborhoods
                    .Where(n => n.Description == neighbourhood)
                    .Select(n => n.Region)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting neighbourhood's region.");
            }

            return default;
        }

        public async Task<NeighbourhoodsRatingModel> GetRating(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DB get neighbourhoods rating");

            try
            {
                var ratings = await _context.NeighbourhoodsRating.ToArrayAsync(cancellationToken);
                var result = new NeighbourhoodsRatingModel
                {
                    ForLiving = ratings.Select(r => JsonSerializer.Deserialize<IEnumerable<string>>(r.ForLiving)),
                    ForInvestment = ratings.Select(r => JsonSerializer.Deserialize<IEnumerable<string>>(r.ForInvestment)),
                    Budget = ratings.Select(r => JsonSerializer.Deserialize<IEnumerable<string>>(r.Budget)),
                    Luxury = ratings.Select(r => JsonSerializer.Deserialize<IEnumerable<string>>(r.Luxury))
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting neighbourhoods rating");
            }

            return new NeighbourhoodsRatingModel();
        }
    }
}
