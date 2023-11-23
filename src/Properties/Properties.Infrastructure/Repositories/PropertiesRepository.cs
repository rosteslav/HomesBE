using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;
using BuildingMarket.Properties.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class PropertiesRepository(
        PropertiesDbContext context,
        ILogger<PropertiesRepository> logger)
        : IPropertiesRepository
    {
        private readonly PropertiesDbContext _context = context;
        private readonly ILogger<PropertiesRepository> _logger = logger;

        public async Task<AddPropertyOutputModel> Add(Property item)
        {
            _logger.LogInformation($"DB add property with seller ID: {item.SellerId}");
            item.CreatedOnUtcTime = DateTime.UtcNow;
            await _context.Properties.AddAsync(item);
            await _context.SaveChangesAsync();

            return new AddPropertyOutputModel { Id = item.Id };
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

        public async Task<PropertyModel> GetById(int id)
        {
            _logger.LogInformation($"DB get property with ID {id}");
            var result = await GetByFilterExpression(x => x.Id == id);

            return result.First();
        }

        public async Task<IEnumerable<PropertyModel>> GetByBroker(string brokerId)
        {
            _logger.LogInformation("DB get all properties for broker with id " + brokerId);

            return await GetByFilterExpression(x => x.BrokerId == brokerId);
        }

        public async Task<IEnumerable<PropertyModel>> GetBySeller(string sellerId)
        {
            _logger.LogInformation("DB get all properties for seller with id " + sellerId);

            return await GetByFilterExpression(x => x.SellerId == sellerId);
        }

        private async Task<IEnumerable<PropertyModel>> GetByFilterExpression(Expression<Func<Property, bool>> filterExpression)
        {
            var query = from p in _context.Properties.Where(filterExpression)
                        join u in _context.Users on p.BrokerId ?? p.SellerId equals u.Id
                        join ad in _context.AdditionalUserData on u.Id equals ad.UserId
                        select new PropertyModel
                        {
                            BrokerId = p.BrokerId,
                            BuildingType = p.BuildingType,
                            CreatedOnLocalTime = p.CreatedOnUtcTime.ToLocalTime(),
                            Finish = p.Finish,
                            Floor = p.Floor,
                            Furnishment = p.Furnishment,
                            Garage = p.Garage,
                            Heating = p.Heating,
                            NumberOfRooms = p.NumberOfRooms,
                            Price = p.Price,
                            Neighbourhood = p.Neighbourhood,
                            Space = p.Space,
                            TotalFloorsInBuilding = p.TotalFloorsInBuilding,
                            Description = p.Description,
                            ContactInfo = new ContactInfo
                            {
                                Email = u.Email,
                                FirstName = ad.FirstName,
                                LastName = ad.LastName,
                                PhoneNumber = ad.PhoneNumber
                            }
                        };

            return await query.ToArrayAsync();
        }
    }
}
