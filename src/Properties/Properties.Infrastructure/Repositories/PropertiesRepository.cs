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

        public async Task<IEnumerable<GetAllPropertiesOutputModel>> Get()
        {
            _logger.LogInformation("DB get all properties");

            try
            {
                var items = from property in _context.Properties
                            join image in _context.Images on property.Id equals image.PropertyId into images
                            select new GetAllPropertiesOutputModel
                            {
                                Id = property.Id,
                                CreatedOnLocalTime = property.CreatedOnUtcTime.ToLocalTime(),
                                Details = string.Join(',', property.BuildingType, property.Finish, property.Furnishment, property.Heating),
                                Neighbourhood = property.Neighbourhood,
                                Price = property.Price,
                                NumberOfRooms = property.NumberOfRooms,
                                Space = property.Space,
                                Images = images.Select(img => img.ImageURL)
                            };

                return await items.ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all items");
            }

            return Enumerable.Empty<GetAllPropertiesOutputModel>();
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
            var query = from property in _context.Properties.Where(filterExpression)
                        join user in _context.Users on property.BrokerId ?? property.SellerId equals user.Id
                        join userData in _context.AdditionalUserData on user.Id equals userData.UserId
                        join image in _context.Images on property.Id equals image.PropertyId into images
                        select new PropertyModel
                        {
                            BrokerId = property.BrokerId,
                            BuildingType = property.BuildingType,
                            CreatedOnLocalTime = property.CreatedOnUtcTime.ToLocalTime(),
                            Finish = property.Finish,
                            Floor = property.Floor,
                            Furnishment = property.Furnishment,
                            Garage = property.Garage,
                            Heating = property.Heating,
                            NumberOfRooms = property.NumberOfRooms,
                            Price = property.Price,
                            Neighbourhood = property.Neighbourhood,
                            Space = property.Space,
                            TotalFloorsInBuilding = property.TotalFloorsInBuilding,
                            Description = property.Description,
                            ContactInfo = new ContactInfo
                            {
                                Email = user.Email,
                                FirstName = userData.FirstName,
                                LastName = userData.LastName,
                                PhoneNumber = userData.PhoneNumber
                            },
                            Images = images.Select(img => img.ImageURL)
                        };

            return await query.ToArrayAsync();
        }
    }
}
