using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Infrastructure.Persistence;
using BuildingMarket.Images.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMarket.Images.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration
                .GetConnectionString("PostgresConnectionString");

            services.AddDbContext<ImagesDbContext>(options =>
            {
                options.UseNpgsql(connectionString)
                    .UseLazyLoadingProxies();
            },
            ServiceLifetime.Transient);

            services.AddScoped<IImgbbService, ImgbbService>();
            services.AddScoped<IPropertyImagesRepository, PropertyImagesRepository>();
            services.AddScoped<IPropertiesRepository, PropertiesRepository>();
            services.AddScoped<IUserImagesRepository, UserImagesRepository>();
            services.AddTransient<IPropertyImagesStore, PropertyImagesStore>();

            return services;
        }
    }
}
