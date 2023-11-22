using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;
using BuildingMarket.Properties.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class PropertiesRepository(
        PropertiesDbContext context,
        UserManager<IdentityUser> userManager,
        IMapper mapper,
        ILogger<PropertiesRepository> logger)
        : IPropertiesRepository
    {
        private readonly PropertiesDbContext _context = context;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
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

            var property = await _context.Properties.FirstAsync(x => x.Id == id);
            var result = await GetResultModel(property);

            return result;
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

        private async Task<PropertyModel> GetResultModel(Property property)
        {
            var contactId = property.BrokerId is not null ? property.BrokerId : property.SellerId;
            var contact = await _context.AdditionalUserData
                .Where(x => x.UserId == contactId)
                .ProjectTo<ContactInfo>(_mapper.ConfigurationProvider)
                .FirstAsync();
            contact.Email = await _userManager.Users
                .Where(x => x.Id == contactId)
                .Select(x => x.Email)
                .FirstAsync();

            var result = _mapper.Map<PropertyModel>(property);
            result.ContactInfo = contact;

            return result;
        }

        private async Task<IEnumerable<PropertyModel>> GetByFilterExpression(Expression<Func<Property, bool>> filterExpression)
        {
            var properties = await _context.Properties.Where(filterExpression).ToListAsync();
            if (!properties.Any())
            {
                return Enumerable.Empty<PropertyModel>();
            }

            var resultCollection = new List<PropertyModel>();
            foreach (var property in properties)
            {
                resultCollection.Add(await GetResultModel(property));
            }

            return resultCollection;
        }
    }
}
