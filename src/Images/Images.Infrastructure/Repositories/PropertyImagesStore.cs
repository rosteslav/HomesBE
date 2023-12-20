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
        private readonly SemaphoreSlim _semaphoreConnect = new(1, 1);
        private readonly IRedisProvider _redisProvider = redisProvider;
        private readonly IDatabase _redisDb = redisProvider.GetDatabase();

        public async Task UploadPropertiesImages(IEnumerable<PropertyImagesModel> properties, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphoreConnect.WaitAsync(cancellationToken);

            try
            {
                var entries = properties
                    .Select(p => new HashEntry(p.PropertyId, MessagePackSerializer.Serialize(p.Images)))
                    .ToArray();
                var key = new RedisKey(_storeSettings.ImagesHashKey);

                await _redisDb.HashSetAsync(key, entries);
                _logger.LogInformation("Images of {0} properties have been uploaded to Redis", entries.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while uploading images into Redis in {0}", nameof(PropertyImagesStore));
            }
            finally
            {
                _redisProvider.Dispose();
                _semaphoreConnect.Release();
            }
        }
    }
}
