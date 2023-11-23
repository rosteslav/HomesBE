using BuildingMarket.Properties.Infrastructure.Persistence;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class PropertyOptionsRepository(PropertiesDbContext context, ILogger<PropertyOptionsRepository> logger) : IPropertyOptionsRepository
    {
        private readonly ILogger<PropertyOptionsRepository> _logger = logger;
        private readonly PropertiesDbContext _context = context;

        public async Task<PropertyOptionsModel> GetAllPropertyOptions()
        {
            try
            {
                PropertyOptionsModel propertyOptionsModel = new PropertyOptionsModel()
                {
                    BuildingType = await _context.BuildingTypes.Select(bt => bt.Description).ToListAsync(),
                    Finish = await _context.Finishes.Select(bt => bt.Description).ToListAsync(),
                    Furnishment = await _context.Furnishments.Select(bt => bt.Description).ToListAsync(),
                    Garage = await _context.Garages.Select(bt => bt.Description).ToListAsync(),
                    Heating = await _context.Heating.Select(bt => bt.Description).ToListAsync(),
                    Neighbourhood = await _context.Neighborhoods.Select(bt => bt.Description).ToListAsync()
                };

                _logger.LogInformation("Property options returned from db");

                return propertyOptionsModel;
            }
            catch (Exception e)
            {

                _logger.LogError("Error while retrieving buidling types\n" + e.Message);
            }

            return (PropertyOptionsModel)Enumerable.Empty<string>();

        }
    }
}
