using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Infrasructure.Persistence;
using BuildingMarket.Properties.Infrasructure.Persistence.Configuration;
using BuildingMarket.Properties.Infrasructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMarket.Properties.Infrasructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsConfig = configuration.GetSection("ConnectionStrings").Get<ConnectionsConfig>();
            services.AddDbContext<PropertiesDbContext>(options =>
            {
                options.UseNpgsql(connectionsConfig.PostgresConnectionString)
                    .UseLazyLoadingProxies();
            },
            ServiceLifetime.Transient);
            services.AddScoped<IPropertiesRepository, PropertiesRepository>();

            return services;
        }
    }
}