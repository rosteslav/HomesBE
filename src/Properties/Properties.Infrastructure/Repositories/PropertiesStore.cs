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
                List<ReportRedisModel> deserializedReports = !oldReports.IsNull
                    ? [.. MessagePackSerializer.Deserialize<List<ReportRedisModel>>(oldReports, cancellationToken: cancellationToken), redisModel]
                    : [redisModel];

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
