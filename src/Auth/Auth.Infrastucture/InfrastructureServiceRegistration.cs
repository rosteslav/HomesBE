using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Infrastructure.Persistence;
using BuildingMarket.Auth.Infrastructure.Persistence.Configuration;
using BuildingMarket.Auth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMarket.Auth.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsConfig = configuration.GetSection("ConnectionStrings").Get<ConnectionsConfig>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionsConfig.PostgresConnectionString)
                    .UseLazyLoadingProxies();
            },
            ServiceLifetime.Transient);

            services.AddScoped<ISecurityService, SecurityService>();

            return services;
        }
    }
}
