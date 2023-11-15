using BuildingMarket.Admins.Application;
using BuildingMarket.Admins.Infrastructure;
using BuildingMarket.Admins.Infrastructure.Persistence;
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
    .AddEntityFrameworkStores<AdminsDbContext>()
    .AddDefaultTokenProviders();

services.AddSwaggerGenBearer();
services.AddCommonServices(configuration);
services.AddApplicationServices();
services.AddInfrastructureServices(configuration);

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
