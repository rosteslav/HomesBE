using BuildingMarket.Properties.Application.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingMarket.Properties.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RedisStoreSettings>(config.GetSection(nameof(RedisStoreSettings)));
            services.Configure<PropertiesConfiguration>(config.GetSection(nameof(PropertiesConfiguration)));
            services.AddSingleton<NextTo>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
