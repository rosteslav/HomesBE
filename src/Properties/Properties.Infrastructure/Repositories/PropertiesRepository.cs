using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;
using BuildingMarket.Properties.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class PropertiesRepository(
        IConfiguration configuration,
        PropertiesDbContext context,
        IMapper mapper,
        ILogger<PropertiesRepository> logger)
        : IPropertiesRepository
    {
        private readonly int PageSize = configuration.GetValue<int>("PropertiesPageSize");
        private readonly PropertiesDbContext _context = context;
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

        public async Task<IEnumerable<GetAllPropertiesOutputModel>> Get(GetAllPropertiesQuery query)
        {
            await Task.Yield();
            _logger.LogInformation("DB get all properties");
            query ??= new();

            if (query.Exposure != null)
            {
                for (int i = 0; i < query.Exposure.Length; i++)
                    query.Exposure[i] = "%" + query.Exposure[i] + "%";
            }

            try
            {
                var orderByPropInfo = typeof(GetAllPropertiesOutputModel).GetProperty(query.OrderBy ?? nameof(GetAllPropertiesOutputModel.CreatedOnLocalTime));
                var properties = _context.Properties
                    .Where(property =>
                        (query.Neighbourhood == null || query.Neighbourhood.Contains(property.Neighbourhood)) &&
                        (query.NumberOfRooms == null || query.NumberOfRooms.Contains(property.NumberOfRooms)) &&
                        (query.SpaceFrom == 0 || query.SpaceFrom <= property.Space) &&
                        (query.SpaceTo == 0 || query.SpaceTo >= property.Space) &&
                        (query.PriceFrom == 0 || query.PriceFrom <= property.Price) &&
                        (query.PriceTo == 0 || query.PriceTo >= property.Price) &&
                        (query.Finish == null || query.Finish.Contains(property.Finish)) &&
                        (query.Furnishment == null || query.Furnishment.Contains(property.Furnishment)) &&
                        (query.Heating == null || query.Heating.Contains(property.Heating)) &&
                        (query.BuildingType == null || query.BuildingType.Contains(property.BuildingType)) &&
                        (query.Exposure == null || query.Exposure.Any(e => EF.Functions.Like(property.Exposure, e))) &&
                        (query.PublishedOn == 0 || property.CreatedOnUtcTime.Date > DateTime.UtcNow.AddDays(-query.PublishedOn).Date))
                    .GroupJoin(_context.Images,
                        property => property.Id,
                        image => image.PropertyId,
                        (property, image) => new { Property = property, Images = image })
                    .Select(pi => new GetAllPropertiesOutputModel
                    {
                        Id = pi.Property.Id,
                        CreatedOnLocalTime = pi.Property.CreatedOnUtcTime.ToLocalTime(),
                        Details = string.Join(',', pi.Property.BuildingType, pi.Property.Finish, pi.Property.Furnishment, pi.Property.Heating, pi.Property.Exposure),
                        Neighbourhood = pi.Property.Neighbourhood,
                        Price = pi.Property.Price,
                        NumberOfRooms = pi.Property.NumberOfRooms,
                        Space = pi.Property.Space,
                        Images = pi.Images.OrderBy(img => img.Id).Select(img => img.ImageURL)
                    });

                var orderedProps = query.IsAscending
                    ? properties.OrderBy(orderByPropInfo.GetValue)
                    : properties.OrderByDescending(orderByPropInfo.GetValue);

                return orderedProps.Skip(PageSize * (query.Page - 1)).Take(PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all items");
            }

            return Enumerable.Empty<GetAllPropertiesOutputModel>();
        }

        public async Task<PropertyModel> GetById(int id)
        {
            _logger.LogInformation("DB get property with ID {id}", id);

            var result = await GetByFilterExpression<PropertyModel>(x => x.Id == id).SingleOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<PropertyModelWithId>> GetByBroker(string brokerId)
        {
            _logger.LogInformation("DB get all properties for broker with id " + brokerId);

            return await GetByFilterExpression<PropertyModelWithId>(x => x.BrokerId == brokerId).ToListAsync();
        }

        public async Task<IEnumerable<PropertyModelWithId>> GetBySeller(string sellerId)
        {
            _logger.LogInformation("DB get all properties for seller with id " + sellerId);

            return await GetByFilterExpression<PropertyModelWithId>(x => x.SellerId == sellerId).ToListAsync();
        }

        private IQueryable<T> GetByFilterExpression<T>(Expression<Func<Property, bool>> filterExpression)
            => _context.Properties.Where(filterExpression)
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
                    (pua, image) => new PropertyProjectToModel
                    {
                        Property = pua.property,
                        User = pua.user,
                        UserData = pua.additionalUserData,
                        Images = image.OrderBy(img => img.Id)
                    })
                .ProjectTo<T>(_mapper.ConfigurationProvider);

        public async Task DeleteById(int id)
        {
            await _context.Properties
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();

            await _context.SaveChangesAsync();
        }

        public async Task EditById(int id, AddPropertyInputModel editedProperty)
        {
            var propertyToUpdate = await _context.Properties
                .FirstAsync(e => e.Id == id);

            _mapper.Map(editedProperty, propertyToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
            => await _context.Properties.AnyAsync(p => p.Id == id);

        public async Task<bool> IsOwner(string userId, int propertyId)
            => await _context.Properties
                .AnyAsync(p => p.Id == propertyId && (p.SellerId == userId || p.BrokerId == userId));
    }
}