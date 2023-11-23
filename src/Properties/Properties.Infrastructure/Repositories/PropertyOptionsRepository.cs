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
                var buildingTypes = await _context.BuildingTypes.Select(bt => bt.Description).ToListAsync();
                var finishTypes = await _context.Finishes.Select(bt => bt.Description).ToListAsync();
                var furnishmentTypes = await _context.Furnishments.Select(bt => bt.Description).ToListAsync();
                var garageTypes = await _context.Garages.Select(bt => bt.Description).ToListAsync();
                var heatingTypes = await _context.Heatings.Select(bt => bt.Description).ToListAsync();
                var neighbourhoods = await _context.Neighborhoods.Select(bt => bt.Description).ToListAsync();

                PropertyOptionsModel propertyOptionsModel = new PropertyOptionsModel()
                {
                    BuildingType = buildingTypes,
                    Finish = finishTypes,
                    Furnishment = furnishmentTypes,
                    Garage = garageTypes,
                    Heating = heatingTypes,
                    Neighbourhood = neighbourhoods
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
