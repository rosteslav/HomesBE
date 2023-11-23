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
                var buildingTypes = _context.BuildingTypes.Select(bt => bt.Description).ToListAsync();
                var finishTypes = _context.Finishes.Select(bt => bt.Description).ToListAsync();
                var furnishmentTypes = _context.Furnishments.Select(bt => bt.Description).ToListAsync();
                var garageTypes = _context.Garages.Select(bt => bt.Description).ToListAsync();
                var heatingTypes = _context.Heating.Select(bt => bt.Description).ToListAsync();
                var neighbourhoods = _context.Neighborhoods.Select(bt => bt.Description).ToListAsync();

                PropertyOptionsModel propertyOptionsModel = new PropertyOptionsModel()
                {
                    BuildingType = await buildingTypes,
                    Finish = await finishTypes,
                    Furnishment = await furnishmentTypes,
                    Garage = await garageTypes,
                    Heating = await heatingTypes,
                    Neighbourhood = await neighbourhoods
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
