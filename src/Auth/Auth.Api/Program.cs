using BuildingMarket.Auth.Api.HostedServices;
using BuildingMarket.Auth.Application;
using BuildingMarket.Auth.Infrastructure;
using BuildingMarket.Auth.Infrastructure.Persistence;
using BuildingMarket.Common;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddSwaggerGenBearer();
services.AddWorkerConfiguration(configuration);
services.AddAuthenticationServices(configuration);
services.AddCommonServices(configuration);
services.AddApplicationServices(configuration);
services.AddInfrastructureServices(configuration);
services.AddSingleton<BuyerPreferencesService>();
services.AddHostedService(provider => provider.GetRequiredService<BuyerPreferencesService>());

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
app.Run();
