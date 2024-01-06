using BuildingMarket.Common.Models;
using BuildingMarket.Common.Providers.Interfaces;
using BuildingMarket.Properties.Application.Configurations;
using BuildingMarket.Properties.Application.Contracts;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Collections.Frozen;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class PreferencesStore(
        IOptions<RedisStoreSettings> storeSettings,
        IRedisProvider redisProvider,
        ILogger<PreferencesStore> logger)
        : IPreferencesStore
    {
        private readonly RedisStoreSettings _storeSettings = storeSettings.Value;
        private readonly IRedisProvider _redisProvider = redisProvider;
        private readonly IDatabase _redisDb = redisProvider.GetDatabase();
        private readonly ILogger<PreferencesStore> _logger = logger;
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<IDictionary<string, BuyerPreferencesRedisModel>> GetAllBuyersPreferences(CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            _logger.LogInformation("Attempt to retrieve all buyers preferences");

            try
            {
                var key = new RedisKey(_storeSettings.PreferencesHashKey);
                var entries = await _redisDb.HashGetAllAsync(key);
                var buyersPreferences = entries.Where(entry => entry.Value.HasValue).ToFrozenDictionary(
                    entry => entry.Name.ToString(),
                    entry => MessagePackSerializer.Deserialize<BuyerPreferencesRedisModel>(entry.Value, options: null, cancellationToken));

                return buyersPreferences;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving preferences from Redis in {store}", nameof(PreferencesStore));
            }
            finally
            {
                _semaphore.Release();
            }

            return default;
        }

        public async Task<BuyerPreferencesRedisModel> GetPreferences(string buyerId, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            _logger.LogInformation("Attempt to retrieve buyer preferences");

            try
            {
                var key = new RedisKey(_storeSettings.PreferencesHashKey);
                var value = await _redisDb.HashGetAsync(key, buyerId);

                var preferences = value.HasValue
                    ? MessagePackSerializer.Deserialize<BuyerPreferencesRedisModel>(value)
                    : default;

                return preferences;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving preferences from Redis for buyer with ID: {buyerId} in {service}", buyerId, nameof(PreferencesStore));
            }
            finally
            {
                _semaphore.Release();
            }

            return new BuyerPreferencesRedisModel();
        }
    }
}
