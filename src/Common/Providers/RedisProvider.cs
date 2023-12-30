using AutoMapper;
using BuildingMarket.Common.Configuration;
using BuildingMarket.Common.Providers.Interfaces;
using StackExchange.Redis;

namespace BuildingMarket.Common.Providers
{
    public class RedisProvider(RedisConnectionConfig config, IMapper mapper) : IRedisProvider
    {
        private IConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(mapper.Map<ConfigurationOptions>(config));

        public IDatabase GetDatabase() => _connection.GetDatabase();
    }
}
