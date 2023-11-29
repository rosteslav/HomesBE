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
                                Details = string.Join(',', property.BuildingType, property.Finish, property.Furnishment, property.Heating, property.Exposure, property.PublishedOn),
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
            var query = _context.Properties.Where(filterExpression)
                .Join(_context.Users,
                    property => property.BrokerId ?? property.SellerId,
                    user => user.Id,
                    (property, user) => new { property, user })
                .Join(_context.AdditionalUserData,
                    pu => pu.user.Id,
                    additionalUserData => additionalUserData.UserId,
                    (pu, additionalUserData) => new { pu.user, pu.property, additionalUserData })
                .GroupJoin(_context.Images,
                    pua => pua.property.Id,
                    img => img.PropertyId,
                    (pua, image) => new { pua.user, pua.property, pua.additionalUserData, image })
                .Select(data => new PropertyModel
                {
                    BrokerId = data.property.BrokerId,
                    PublishedOn = data.property.PublishedOn,
                    BuildingType = data.property.BuildingType,
                    CreatedOnLocalTime = data.property.CreatedOnUtcTime.ToLocalTime(),
                    Finish = data.property.Finish,
                    Exposure = data.property.Exposure,
                    Floor = data.property.Floor,
                    Furnishment = data.property.Furnishment,
                    Garage = data.property.Garage,
                    Heating = data.property.Heating,
                    NumberOfRooms = data.property.NumberOfRooms,
                    Price = data.property.Price,
                    Neighbourhood = data.property.Neighbourhood,
                    Space = data.property.Space,
                    TotalFloorsInBuilding = data.property.TotalFloorsInBuilding,
                    Description = data.property.Description,
                    ContactInfo = new ContactInfo
                    {
                        Email = data.user.Email,
                        FirstName = data.additionalUserData.FirstName,
                        LastName = data.additionalUserData.LastName,
                        PhoneNumber = data.additionalUserData.PhoneNumber,
                    },
                    Images = data.image.Select(x => x.ImageURL)
                });

            return await query.ToArrayAsync();
        }
    }
}