using BuildingMarket.Auth.Application.Configurations;
using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Common.Providers.Interfaces;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class PreferencesStore(
        ILogger<PreferencesStore> logger,
        IOptions<RedisStoreSettings> storeSettings,
        IRedisProvider redisProvider
        ) : IPreferencesStore
    {
        private readonly ILogger<PreferencesStore> _logger = logger;
        private readonly RedisStoreSettings _storeSettings = storeSettings.Value;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly IRedisProvider _redisProvider = redisProvider;
        private readonly IDatabase _redisDb = redisProvider.GetDatabase();

        public async Task SetBuyersPreferences(
            IEnumerable<BuyerPreferencesRedisModel> buyersPreferences,
            CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var key = new RedisKey(_storeSettings.PreferencesHashKey);
                var entries = buyersPreferences
                    .Select(p => new HashEntry(p.UserId, MessagePackSerializer.Serialize(new { p.Purpose, p.Region, p.BuildingType, p.PriceHigherEnd })))
                    .ToArray();

                await _redisDb.HashSetAsync(key, entries);
                _logger.LogInformation("Preferences of {0} buyers have been uploaded to Redis.", entries.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while uploading buyers preferences into Redis in {0}", nameof(PreferencesStore));
            }
            finally
            {
                _redisProvider.Dispose();
                _semaphore.Release();
            }
        }
    }
}
