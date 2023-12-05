using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class PropertyOptionsRepository(
        PropertiesDbContext context,
        IMapper mapper,
        ILogger<PropertyOptionsRepository> logger)
        : IPropertyOptionsRepository
    {
        private readonly PropertiesDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<PropertyOptionsRepository> _logger = logger;

        public async Task<PropertyOptionsModel> GetAllPropertyOptions()
        {
            try
            {
                var optionsModel = new PropertyOptionsModel
                {
                    BuildingType = await _context.BuildingTypes.Select(bt => bt.Description).ToArrayAsync(),
                    Finish = await _context.Finishes.Select(f => f.Description).ToArrayAsync(),
                    Exposure = await _context.Exposures.Select(e => e.Description).ToArrayAsync(),
                    Furnishment = await _context.Furnishments.Select(f => f.Description).ToArrayAsync(),
                    Garage = await _context.Garages.Select(g => g.Description).ToArrayAsync(),
                    Heating = await _context.Heating.Select(h => h.Description).ToArrayAsync(),
                    Neighbourhood = await _context.Neighborhoods.Select(n => n.Description).ToArrayAsync(),
                    NumberOfRooms = await _context.NumberOfRooms.Select(nr => nr.Description).ToArrayAsync()
                };

                _logger.LogInformation("Property options returned from db");

                return optionsModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while retrieving property options");
            }

            return (PropertyOptionsModel)Enumerable.Empty<string>();
        }

        public async Task<PropertyOptionsWithFilterModel> GetPropertyOptionsWithFilter()
        {
            try
            {
                var optionsWithFilterModel = new PropertyOptionsWithFilterModel
                {
                    BuildingType = await _context.BuildingTypes.Select(bt => bt.Description).ToArrayAsync(),
                    Finish = await _context.Finishes.Select(f => f.Description).ToArrayAsync(),
                    Exposure = await _context.Exposures.Select(e => e.Description).ToArrayAsync(),
                    Furnishment = await _context.Furnishments.Select(f => f.Description).ToArrayAsync(),
                    Garage = await _context.Garages.Select(g => g.Description).ToArrayAsync(),
                    Heating = await _context.Heating.Select(h => h.Description).ToArrayAsync(),
                    Neighbourhood = await _context.Neighborhoods.Select(n => n.Description).ToArrayAsync(),
                    NumberOfRooms = await _context.NumberOfRooms.Select(nr => nr.Description).ToArrayAsync(),
                    PublishedOn = await _context.PublishedOn.ProjectTo<PublishedOnModel>(_mapper.ConfigurationProvider).ToArrayAsync(),
                    OrderBy = await _context.OrderBy.ProjectTo<OrderByModel>(_mapper.ConfigurationProvider).ToArrayAsync()
                };

                _logger.LogInformation("Property options returned from db");

                return optionsWithFilterModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while retrieving property options");
            }

            return (PropertyOptionsWithFilterModel)Enumerable.Empty<string>();
        }
    }
}
