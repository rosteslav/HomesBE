using BuildingMarket.Common.Models;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class NeighbourhoodsRepository(PropertiesDbContext context, ILogger<NeighbourhoodsRepository> logger) : INeighbourhoodsRepository
    {
        private readonly PropertiesDbContext _context = context;
        private readonly ILogger<NeighbourhoodsRepository> _logger = logger;

        public async Task<NeighbourhoodsRatingModel> GetRating(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DB get neighbourhoods rating");

            try
            {
                var ratings = await _context.NeighbourhoodsRating.ToArrayAsync(cancellationToken);
                var result = new NeighbourhoodsRatingModel
                {
                    ForLiving = ratings.Select(r => JsonConvert.DeserializeObject<IEnumerable<string>>(r.ForLiving)),
                    ForInvestment = ratings.Select(r => JsonConvert.DeserializeObject<IEnumerable<string>>(r.ForInvestment)),
                    Budget = ratings.Select(r => JsonConvert.DeserializeObject<IEnumerable<string>>(r.Budget)),
                    Luxury = ratings.Select(r => JsonConvert.DeserializeObject<IEnumerable<string>>(r.Luxury))
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
