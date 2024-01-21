using BuildingMarket.Common.Providers.Interfaces;
using BuildingMarket.Images.Application.Configurations;
using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class PropertyImagesStore(
        ILogger<PropertyImagesStore> logger,
        IOptions<RedisStoreSettings> storeSettings,
        IRedisProvider redisProvider)
        : IPropertyImagesStore
    {
        private readonly ILogger<PropertyImagesStore> _logger = logger;
        private readonly RedisStoreSettings _storeSettings = storeSettings.Value;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly IRedisProvider _redisProvider = redisProvider;
        private readonly IDatabase _redisDb = redisProvider.GetDatabase();

        public async Task UpdatePropertyImages(int propertyId, IEnumerable<string> imageURLs, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var key = new RedisKey(_storeSettings.ImagesHashKey);
                await _redisDb.HashSetAsync(key, propertyId, MessagePackSerializer.Serialize(imageURLs, options: null, cancellationToken));

                _logger.LogInformation($"A {imageURLs.Count()} images were successfully added to Redis for property with ID: {propertyId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding images into Redis for property with ID: {propertyId} in {nameof(PropertyImagesStore)}");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task UploadPropertiesImages(IEnumerable<PropertyImagesModel> properties, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var key = new RedisKey(_storeSettings.ImagesHashKey);
                var entries = properties
                    .Select(p => new HashEntry(p.PropertyId, MessagePackSerializer.Serialize(p.Images)))
                    .ToArray();

                await _redisDb.HashSetAsync(key, entries);
                _logger.LogInformation($"Images of {entries.Length} properties have been uploaded to Redis");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while uploading images into Redis in {nameof(PropertyImagesStore)}");
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
