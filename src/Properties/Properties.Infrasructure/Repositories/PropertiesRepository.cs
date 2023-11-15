using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Domain.Entities;
using BuildingMarket.Properties.Infrasructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Properties.Infrasructure.Repositories
{
    public class PropertiesRepository(PropertiesDbContext context, ILogger<PropertiesRepository> logger) : IPropertiesRepository
    {
        private readonly ILogger<PropertiesRepository> _logger = logger;
        private readonly PropertiesDbContext _context = context;

        public async Task Add(Property item)
        {
            _logger.LogInformation($"DB add property: {item.Type}");

            try
            {
                await _context.Properties.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding property: {item.Type}");
            }
        }

        public async Task<IEnumerable<Property>> Get()
        {
            _logger.LogInformation("DB get all properties");

            try
            {
                var items = await _context.Properties.ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all items");
            }

            return Enumerable.Empty<Property>();
        }

        public async Task<IEnumerable<Property>> GetByBroker(string brokerId)
        {
            _logger.LogInformation("DB get all properties for broker with id " + brokerId);

            try
            {
                var items = await _context.Properties.Where(x => x.BrokerId == brokerId).ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting properties");
            }

            return Enumerable.Empty<Property>();
        }

        public async Task<IEnumerable<Property>> GetBySeller(string sellerId)
        {
            _logger.LogInformation("DB get all properties for seller with id " + sellerId);

            try
            {
                var items = await _context.Properties.Where(x => x.SellerId == sellerId).ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting properties");
            }

            return Enumerable.Empty<Property>();
        }
    }
}
