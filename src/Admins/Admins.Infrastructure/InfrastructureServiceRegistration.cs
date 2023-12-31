using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Admins.Infrastructure.Persistence;
using BuildingMarket.Admins.Infrastructure.Repositories;
using BuildingMarket.Common.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMarket.Admins.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsConfig = configuration.GetSection("ConnectionStrings").Get<ConnectionsConfig>();
            
            services.AddDbContext<AdminsDbContext>(options =>
            {
                options.UseNpgsql(connectionsConfig.PostgresConnectionString)
                    .UseLazyLoadingProxies();
            },
            ServiceLifetime.Transient);

            services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AdminsDbContext>();

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddSingleton<IReportsStore, ReportsStore>();

            return services;
        }
    }
}
