using Demo.Application.Contracts;
using Demo.Infrastructure.Persistence;
using Demo.Infrastructure.Persistence.Configuration;
using Demo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastructure
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

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ISecurityService, SecurityService>();

            return services;
        }
    }
}
