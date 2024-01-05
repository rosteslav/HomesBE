using AutoMapper;
using BuildingMarket.Common.Models;
using BuildingMarket.Common.Providers.Interfaces;
using BuildingMarket.Properties.Application.Configurations;
using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Features.Properties.Commands.ReportProperty;
using BuildingMarket.Properties.Application.Models;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Collections.Frozen;

namespace BuildingMarket.Properties.Infrastructure.Repositories
{
    public class PropertiesStore(
        ILogger<PropertiesStore> logger,
        IOptions<RedisStoreSettings> storeSettings,
        IRedisProvider redisProvider,
        IMapper mapper)
        : IPropertiesStore
    {
        private readonly ILogger<PropertiesStore> _logger = logger;
        private readonly RedisStoreSettings _storeSettings = storeSettings.Value;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly IRedisProvider _redisProvider = redisProvider;
        private readonly IDatabase _redisDb = redisProvider.GetDatabase();
        private readonly IMapper _mapper = mapper;

        public async Task<IDictionary<int, PropertyRedisModel>> GetProperties(CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var key = new RedisKey(_storeSettings.PropertiesHashKey);
                var entries = await _redisDb.HashGetAllAsync(key);
                var properties = entries.ToFrozenDictionary(
                    e => int.Parse(e.Name),
                    e => MessagePackSerializer.Deserialize<PropertyRedisModel>(e.Value, cancellationToken: cancellationToken));

                return properties;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting properties from Redis in {store}", nameof(PropertiesStore));
            }
            finally
            {
                _semaphore.Release();
            }

            return default;
        }

        public async Task RemoveProperty(int id, CancellationToken cancellationToken = default)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);
            _logger.LogInformation("Attempt to remove property from Redis...");

            try
            {
                var key = new RedisKey(_storeSettings.PropertiesHashKey);
                await _redisDb.HashDeleteAsync(key, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing property from Redis.");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task UpdateProperty(int id, PropertyRedisModel model, CancellationToken cancellationToken = default)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);
            _logger.LogInformation("Attempt to update property in Redis...");

            try
            {
                var key = new RedisKey(_storeSettings.PropertiesHashKey);
                await _redisDb.HashSetAsync(key, id, MessagePackSerializer.Serialize(model, cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating property in Redis.");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task UploadProperties(IDictionary<int, PropertyRedisModel> properties, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var key = new RedisKey(_storeSettings.PropertiesHashKey);
                var entries = properties
                    .Select(p => new HashEntry(p.Key, MessagePackSerializer.Serialize(p.Value)))
                    .ToArray();

                await _redisDb.HashSetAsync(key, entries);
                _logger.LogInformation("A {count} properties have been uploaded to Redis", entries.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while uploading properties into Redis in {store}", nameof(PropertiesStore));
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task UploadReport(ReportPropertyCommand model, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var key = new RedisKey(_storeSettings.ReportsHashKey);
                var oldReports = await _redisDb.HashGetAsync(key, model.PropertyId);
                var redisModel = _mapper.Map<ReportRedisModel>(model);
                var deserializedReports = oldReports.IsNull
                    ? [redisModel]
                    : MessagePackSerializer
                        .Deserialize<List<ReportRedisModel>>(oldReports, cancellationToken: cancellationToken)
                        .Append(redisModel);

                await _redisDb.HashSetAsync(
                        key,
                        model.PropertyId,
                        MessagePackSerializer.Serialize(deserializedReports, cancellationToken: cancellationToken));

                _logger.LogInformation("Report for property with Id: {id} with Reason: {reason} has been uploaded to Redis.", model.PropertyId, model.ReportModel.Reason);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while uploading report to Redis in {store}", nameof(PropertiesStore));
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
