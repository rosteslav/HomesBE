using BuildingMarket.Admins.Application.Configurations;
using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Admins.Application.Models;
using BuildingMarket.Common.Models;
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

        public async Task DeletePropertyReports(int propertyId, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var key = new RedisKey(_storeSettings.ReportsHashKey);
                await _redisDb.HashDeleteAsync(key, propertyId);

                _logger.LogInformation($"Reports for property with id: {propertyId} were deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting all the reports for property with id: {propertyId} from Redis in {nameof(ReportsStore)}.");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<List<AllReportsModel>> GetAllReports(CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            _logger.LogInformation("Attempt to retrieve all buyers preferences");

            try
            {
                var key = new RedisKey(_storeSettings.ReportsHashKey);
                var entries = await _redisDb.HashGetAllAsync(key);
                var deserialized = entries
                    .Select(e => new AllReportsModel
                    {
                        PropertyId = int.Parse(e.Name),
                        Reports = MessagePackSerializer.Deserialize<List<ReportRedisModel>>(e.Value, cancellationToken: cancellationToken)
                    })
                    .ToList();

                return deserialized;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving all the reports from Redis in {nameof(ReportsStore)}.");
            }
            finally
            {
                _semaphore.Release();
            }

            return default;
        }
    }
}
