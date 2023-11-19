using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Infrastructure.Persistence;
using BuildingMarket.Properties.Infrastructure.Persistence.Configuration;
using BuildingMarket.Properties.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMarket.Properties.Infrastructure
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