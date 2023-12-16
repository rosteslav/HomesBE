using BuildingMarket.Common.Providers.Interfaces;
using BuildingMarket.Images.Application.Configurations;
using BuildingMarket.Images.Application.Contracts;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class TestStore(
        ILogger<TestStore> logger,
        IOptions<RedisStoreSettings> storeSettings,
        IRedisProvider redisProvider
        ) : ITestStore
    {
        private readonly ILogger<TestStore> _logger = logger;
        private readonly RedisStoreSettings _storeSettings = storeSettings.Value;
        private readonly SemaphoreSlim _semaphoreConnect = new(1, 1);
        private readonly IRedisProvider _redisProvider = redisProvider;
        private readonly IDatabase _redisDb = redisProvider.GetDatabase();

        public async Task UpdateTestRedis()
        {
            await Task.Yield();
            await _semaphoreConnect.WaitAsync();

            try
            {
                var key = new RedisKey(_storeSettings.TestHashKey);
                var redisValue = await _redisDb.HashGetAsync(key, "id");
                var value = redisValue.HasValue ? 
                    MessagePackSerializer.Deserialize<long>(redisValue) :
                    0L;
                var newValue = value == long.MaxValue ? 0L : value + 1;

                await _redisDb.HashSetAsync(key, "id", MessagePackSerializer.Serialize(newValue));
                _logger.LogInformation($"uploaded {newValue} to redis");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error while updating test redis in {0}", nameof(TestStore));
            }
            finally
            {
                _redisProvider.Dispose();
                _semaphoreConnect.Release();
            }
        }
    }
}
