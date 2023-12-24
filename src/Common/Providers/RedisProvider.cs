using AutoMapper;
using BuildingMarket.Common.Configuration;
using BuildingMarket.Common.Providers.Interfaces;
using StackExchange.Redis;

namespace BuildingMarket.Common.Providers
{
    public class RedisProvider(
        RedisConnectionConfig config,
        IMapper mapper) : IRedisProvider, IDisposable
    {
        private IMapper _mapper = mapper;
        private ConfigurationOptions _configuration;
        private IConnectionMultiplexer _connection;

        public IDatabase GetDatabase()
        {
            _configuration = _mapper.Map<ConfigurationOptions>(config);
            _connection = ConnectionMultiplexer.Connect(_configuration);
            return _connection.GetDatabase();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
