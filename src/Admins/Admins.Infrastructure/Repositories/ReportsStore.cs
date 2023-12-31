using BuildingMarket.Admins.Application.Configurations;
using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Common.Providers.Interfaces;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BuildingMarket.Admins.Infrastructure.Repositories
{
    public class ReportsStore(
        IOptions<RedisStoreSettings> storeSettings,
        IRedisProvider redisProvider,
        ILogger<ReportsStore> logger) : IReportsStore
    {
        private readonly RedisStoreSettings _storeSettings = storeSettings.Value;
        private readonly IRedisProvider _redisProvider = redisProvider;
        private readonly IDatabase _redisDb = redisProvider.GetDatabase();
        private readonly ILogger<ReportsStore> _logger = logger;
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task GetAllReports(CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            _logger.LogInformation("Attempt to retrieve all buyers preferences");

            try
            {
                var key = new RedisKey(_storeSettings.ReportsHashKey);
                var entries = await _redisDb.HashGetAllAsync(key);
                // deserialize
                //return entries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all the reports from Redis in {store}.", nameof(ReportsStore));
            }
            finally
            {
                _semaphore.Release();
            }

            //return default;
        }
    }
}
