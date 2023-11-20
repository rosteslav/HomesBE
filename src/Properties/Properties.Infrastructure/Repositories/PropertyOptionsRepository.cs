using BuildingMarket.Properties.Infrastructure.Persistence;
using BuildingMarket.Properties.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class PropertyOptionsRepository(PropertiesDbContext context, ILogger<PropertyOptionsRepository> logger) : IPropertyOptionsRepository
    {
        private readonly ILogger<PropertyOptionsRepository> _logger = logger;
        private readonly PropertiesDbContext _context = context;

        public async Task<IEnumerable<string>> GetBuidingTypes()
        {
            try
            {
                var buildingTypes = await _context.BuildingTypes.Select(bt => bt.Description).ToListAsync();

                _logger.LogInformation("Building types returned from db");

                return buildingTypes;
            }
            catch (Exception e)
            {

                _logger.LogError("Error while retrieving buidling types\n" + e.Message);
            }
            
            return Enumerable.Empty<string>();
            
        }

        public async Task<IEnumerable<string>> GetFinishTypes()
        {
            try
            {
                var finishTypes = await _context.Finishes.Select(bt => bt.Description).ToListAsync();

                _logger.LogInformation("Finish types returned from db");

                return finishTypes;
            }
            catch (Exception e)
            {

                _logger.LogError("Error while retrieving finish types\n" + e.Message);
            }

            return Enumerable.Empty<string>();
        }

        public async Task<IEnumerable<string>> GetFurnishmentTypes()
        {
            try
            {
                var furnishmentTypes = await _context.Furnishments.Select(bt => bt.Description).ToListAsync();

                _logger.LogInformation("Furnishments types returned from db");

                return furnishmentTypes;
            }
            catch (Exception e)
            {

                _logger.LogError("Error while retrieving furnishments types\n" + e.Message);
            }

            return Enumerable.Empty<string>();
        }

        public async Task<IEnumerable<string>> GetGarageTypes()
        {
            try
            {
                var garageTypes = await _context.Garages.Select(bt => bt.Description).ToListAsync();

                _logger.LogInformation("Garage types returned from db");

                return garageTypes;
            }
            catch (Exception e)
            {

                _logger.LogError("Error while retrieving garage types\n" + e.Message);
            }

            return Enumerable.Empty<string>();
        }

        public async Task<IEnumerable<string>> GetHeatingTypes()
        {
            try
            {
                var heatingTypes = await _context.Heatings.Select(bt => bt.Description).ToListAsync();

                _logger.LogInformation("Heating types returned from db");

                return heatingTypes;
            }
            catch (Exception e)
            {

                _logger.LogError("Error while retrieving heating types\n" + e.Message);
            }

            return Enumerable.Empty<string>();
        }

        public async Task<IEnumerable<string>> GetNeighbourhoods()
        {
            try
            {
                var neighbourhoods = await _context.Neighborhoods.Select(bt => bt.Description).ToListAsync();

                _logger.LogInformation("Neighbourhoods returned from db");

                return neighbourhoods;
            }
            catch (Exception e)
            {

                _logger.LogError("Error while retrieving Neighbourhoods\n" + e.Message);
            }

            return Enumerable.Empty<string>();
        }
    }
}
