using Demo.Application.Contracts;
using Demo.Infrastucture.Persistence;
using Demo.Infrastucture.Persistence.Configuration;
using Demo.Infrastucture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastucture
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
