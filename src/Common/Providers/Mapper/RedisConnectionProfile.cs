using AutoMapper;
using BuildingMarket.Common.Configuration;
using StackExchange.Redis;

namespace BuildingMarket.Common.Providers.Mapper
{
    public class RedisConnectionProfile : Profile
    {
        public RedisConnectionProfile() 
        {
            CreateMap<RedisConnectionConfig, ConfigurationOptions>()
                .ForMember(c => c.EndPoints, opt => opt.MapFrom(src => new EndPointCollection() { $"{src.Host}:{src.Port}" }))
                .ForMember(c => c.Ssl, opt => opt.MapFrom(_ => false))
                .ForMember(c => c.AllowAdmin, opt => opt.MapFrom(_ => false));
        }
    }
}
