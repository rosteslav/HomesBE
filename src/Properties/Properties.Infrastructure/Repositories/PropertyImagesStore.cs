using BuildingMarket.Common.Providers.Interfaces;
using BuildingMarket.Properties.Application.Configurations;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Collections.Frozen;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class PropertyImagesStore(
        IOptions<RedisStoreSettings> storeSettings,
        IRedisProvider redisProvider,
        ILogger<PropertyImagesStore> logger)
        : IPropertyImagesStore
    {
        private readonly RedisStoreSettings _storeSettings = storeSettings.Value;
        private readonly IRedisProvider _redisProvider = redisProvider;
        private readonly IDatabase _redisDb = redisProvider.GetDatabase();
        private readonly ILogger<PropertyImagesStore> _logger = logger;
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<IEnumerable<PropertyImagesModel>> GetPropertiesImages(params string[] propertyIds)
        {
            await Task.Yield();
            await _semaphore.WaitAsync();

            _logger.LogInformation("Attempt to retrieve all properties images");

            try
            {
                var key = new RedisKey(_storeSettings.ImagesHashKey);
                var fields = propertyIds.Select(id => new RedisValue(id)).ToArray();
                var entries = await _redisDb.HashGetAsync(key, fields);

                var propertiesImages = entries
                    .Select(entry => new PropertyImagesModel
                    {
                        Images = entry.HasValue
                            ? MessagePackSerializer.Deserialize<IEnumerable<string>>(entry)
                            : Enumerable.Empty<string>()
                    })
                    .ToArray();

                return propertiesImages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving images from Redis in {nameof(PropertyImagesStore)}");
            }
            finally
            {
                _semaphore.Release();
            }

            return Enumerable.Empty<PropertyImagesModel>();
        }

        public async Task<IDictionary<int, int>> GetPropertyIdsWithImagesCount(CancellationToken cancellationToken = default)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            _logger.LogInformation("Attempt to retrieve all property ids with images count...");

            try
            {
                var key = new RedisKey(_storeSettings.ImagesHashKey);
                var entries = await _redisDb.HashGetAllAsync(key);

                return entries.ToFrozenDictionary(
                    e => int.Parse(e.Name),
                    e => MessagePackSerializer.Deserialize<IEnumerable<string>>(e.Value).Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving property ids with images count from Redis in {nameof(PropertyImagesStore)}");
            }
            finally
            {
                _semaphore.Release();
            }

            return new Dictionary<int, int>();
        }
    }
}
