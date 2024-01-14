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
        IPropertiesStore propertiesStore,
        INeighbourhoodsRepository neighbourhoodsRepository,
        IMapper mapper,
        ILogger<PropertiesRepository> logger)
        : IPropertiesRepository
    {
        private readonly int PageSize = configuration.GetValue<int>("PropertiesPageSize");
        private readonly PropertiesDbContext _context = context;
        private readonly IPropertiesStore _propertiesStore = propertiesStore;
        private readonly INeighbourhoodsRepository _neighbourhoodsRepository = neighbourhoodsRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<PropertiesRepository> _logger = logger;

        public async Task<AddPropertyOutputModel> Add(Property property, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"DB add property with seller ID: {property.SellerId}");
            property.CreatedOnUtcTime = DateTime.UtcNow;
            await _context.Properties.AddAsync(property, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var redisModelToBeAdded = _mapper.Map<PropertyRedisModel>(property);
            redisModelToBeAdded.Region = await _neighbourhoodsRepository.GetNeighbourhoodRegion(property.Neighbourhood, cancellationToken);
            await _propertiesStore.UpdateProperty(redisModelToBeAdded, cancellationToken);

            return new AddPropertyOutputModel { Id = property.Id };
        }

        public async Task<IEnumerable<GetAllPropertiesOutputModel>> Get(GetAllPropertiesQuery query)
        {
            await Task.Yield();
            _logger.LogInformation("DB get all properties");
            var input = query.Input ?? new();

            if (input.Exposure != null)
            {
                for (int i = 0; i < input.Exposure.Length; i++)
                    input.Exposure[i] = "%" + input.Exposure[i] + "%";
            }

            try
            {
                var orderByPropInfo = typeof(GetAllPropertiesOutputModel).GetProperty(input.OrderBy ?? nameof(GetAllPropertiesOutputModel.CreatedOnLocalTime));
                var properties = _context.Properties
                    .Where(property =>
                        (input.Neighbourhood == null || input.Neighbourhood.Contains(property.Neighbourhood)) &&
                        (input.NumberOfRooms == null || input.NumberOfRooms.Contains(property.NumberOfRooms)) &&
                        (input.SpaceFrom == 0 || input.SpaceFrom <= property.Space) &&
                        (input.SpaceTo == 0 || input.SpaceTo >= property.Space) &&
                        (input.PriceFrom == 0 || input.PriceFrom <= property.Price) &&
                        (input.PriceTo == 0 || input.PriceTo >= property.Price) &&
                        (input.Finish == null || input.Finish.Contains(property.Finish)) &&
                        (input.Furnishment == null || input.Furnishment.Contains(property.Furnishment)) &&
                        (input.Heating == null || input.Heating.Contains(property.Heating)) &&
                        (input.BuildingType == null || input.BuildingType.Contains(property.BuildingType)) &&
                        (input.Exposure == null || input.Exposure.Any(e => EF.Functions.Like(property.Exposure, e))) &&
                        (input.PublishedOn == 0 || property.CreatedOnUtcTime.Date > DateTime.UtcNow.AddDays(-input.PublishedOn).Date))
                    .ProjectTo<GetAllPropertiesOutputModel>(_mapper.ConfigurationProvider);

                var orderedProps = input.IsAscending
                    ? properties.OrderBy(orderByPropInfo.GetValue)
                    : properties.OrderByDescending(orderByPropInfo.GetValue);

                return query.IsAdmin
                    ? orderedProps
                    : orderedProps.Skip(PageSize * (input.Page - 1)).Take(PageSize).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all items");
            }

            return Enumerable.Empty<GetAllPropertiesOutputModel>();
        }

        public async Task<IEnumerable<PropertyRedisModel>> GetForRecommendations(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DB get all properties for recommendations");

            try
            {
                return await _context.Properties
                    .Join(_context.Neighborhoods,
                        p => p.Neighbourhood,
                        n => n.Description,
                        (p, n) => new PropertyRedisModel
                        {
                            Id = p.Id,
                            Price = p.Price,
                            BuildingType = p.BuildingType,
                            Neighbourhood = p.Neighbourhood,
                            NumberOfRooms = p.NumberOfRooms,
                            Region = n.Region
                        })
                    .ToArrayAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all properties");
            }

            return default;
        }

        public async Task<PropertyModel> GetById(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DB get property with ID {id}", id);

            var result = await GetByFilterExpression<PropertyModel>(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<GetAllPropertiesOutputModel>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            if (ids is not null && ids.Any())
            {
                _logger.LogInformation("DB get properties");

                try
                {
                    var properties = await _context.Properties
                        .Where(p => ids.Contains(p.Id))
                        .ProjectTo<GetAllPropertiesOutputModel>(_mapper.ConfigurationProvider)
                        .ToArrayAsync(cancellationToken);

                    return properties;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while getting properties");
                }
            }
            else
            {
                _logger.LogInformation("DB there are no property ids");
            }

            return Enumerable.Empty<GetAllPropertiesOutputModel>();
        }

        public async Task<IEnumerable<PropertyModelWithId>> GetByBroker(string brokerId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DB get all properties for broker with id " + brokerId);

            return await GetByFilterExpression<PropertyModelWithId>(x => x.BrokerId == brokerId).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<PropertyModelWithId>> GetBySeller(string sellerId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DB get all properties for seller with id " + sellerId);

            return await GetByFilterExpression<PropertyModelWithId>(x => x.SellerId == sellerId).ToListAsync(cancellationToken);
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
                    (pu, additionalUserData) => new PropertyProjectToModel
                    {
                        Property = pu.property,
                        User = pu.user,
                        UserData = additionalUserData
                    })
                .ProjectTo<T>(_mapper.ConfigurationProvider);

        public async Task DeleteById(int id, CancellationToken cancellationToken = default)
        {
            var redisModelToBeDeleted = await _context.Properties
                .Where(p => p.Id == id)
                .ProjectTo<PropertyRedisModel>(_mapper.ConfigurationProvider)
                .FirstAsync(cancellationToken);

            await _context.Properties.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);

            redisModelToBeDeleted.Region = await _neighbourhoodsRepository.GetNeighbourhoodRegion(redisModelToBeDeleted.Neighbourhood, cancellationToken);
            await _propertiesStore.RemoveProperty(redisModelToBeDeleted, cancellationToken);
        }

        public async Task EditById(int id, AddPropertyInputModel editedProperty, CancellationToken cancellationToken = default)
        {
            var propertyToBeUpdated = await _context.Properties.FirstAsync(e => e.Id == id, cancellationToken);

            var redisModelToBeDeleted = _mapper.Map<PropertyRedisModel>(propertyToBeUpdated);
            redisModelToBeDeleted.Region = await _neighbourhoodsRepository.GetNeighbourhoodRegion(propertyToBeUpdated.Neighbourhood, cancellationToken);
            
            _mapper.Map(editedProperty, propertyToBeUpdated);
            await _context.SaveChangesAsync(cancellationToken);

            await _propertiesStore.RemoveProperty(redisModelToBeDeleted, cancellationToken);

            var redisModelToBeAdded = _mapper.Map<PropertyRedisModel>(propertyToBeUpdated);
            redisModelToBeAdded.Region = redisModelToBeDeleted.Neighbourhood != propertyToBeUpdated.Neighbourhood 
                ? await _neighbourhoodsRepository.GetNeighbourhoodRegion(propertyToBeUpdated.Neighbourhood, cancellationToken)
                : redisModelToBeDeleted.Region;
            await _propertiesStore.UpdateProperty(redisModelToBeAdded, cancellationToken);
        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
            => await _context.Properties.AnyAsync(p => p.Id == id, cancellationToken);

        public async Task<bool> IsOwner(string userId, int propertyId, CancellationToken cancellationToken = default)
            => await _context.Properties.AnyAsync(p => p.Id == propertyId && (p.SellerId == userId || p.BrokerId == userId), cancellationToken);
    }
}