using BuildingMarket.Common.Providers.Interfaces;
using BuildingMarket.Properties.Application.Configurations;
using BuildingMarket.Properties.Application.Contracts;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class RecommendationStore(
        IOptions<RedisStoreSettings> storeSettings,
        IRedisProvider redisProvider,
        ILogger<RecommendationStore> logger)
        : IRecommendationStore
    {
        private readonly RedisStoreSettings _storeSettings = storeSettings.Value;
        private readonly IRedisProvider _redisProvider = redisProvider;
        private readonly IDatabase _redisDb = redisProvider.GetDatabase();
        private readonly ILogger<RecommendationStore> _logger = logger;
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task UploadRecommendations(IDictionary<string, IEnumerable<int>> buyersRecommendations, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            _logger.LogInformation("Attempt to upload buyers recommendations");

            try
            {
                var key = new RedisKey(_storeSettings.RecommendationsHashKey);
                var fields = buyersRecommendations
                    .Select(br => new HashEntry(br.Key, MessagePackSerializer.Serialize(br.Value)))
                    .ToArray();

                await _redisDb.HashSetAsync(key, fields);
                _logger.LogInformation("Recommendations of {count} buyers have been successfully uploaded to Redis", buyersRecommendations.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while uploading recommendations to Redis in {store}", nameof(RecommendationStore));
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
