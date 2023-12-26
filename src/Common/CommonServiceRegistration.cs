using BuildingMarket.Common.Configuration;
using BuildingMarket.Common.Models;
using BuildingMarket.Common.Providers;
using BuildingMarket.Common.Providers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

namespace BuildingMarket.Common
{
    public static class CommonServiceRegistration
    {
        public static IServiceCollection AddSwaggerGenBearer(this IServiceCollection services)
        {
            services.AddSwaggerGen(delegate (SwaggerGenOptions c)
            {
                c.CustomSchemaIds((Type x) => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Building Market",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insert Bearer and access token in the field. Example: Bearer 00000000-0000-0000-0000-000000000000",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JWT").Get<JWT>();
            services.AddSingleton(jwt);
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = jwt.ValidAudience,
                        ValidIssuer = jwt.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret))
                    };
                });

            return services;
        }

        public static IServiceCollection AddWorkerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(Options.Create(configuration.GetSection(nameof(WorkerConfiguration)).Get<WorkerConfiguration>()));

            return services;
        }

        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton(configuration.GetSection(nameof(RedisConnectionConfig)).Get<RedisConnectionConfig>());
            services.AddSingleton<IRedisProvider, RedisProvider>();

            return services;
        }
    }
}