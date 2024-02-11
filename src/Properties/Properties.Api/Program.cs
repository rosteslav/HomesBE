using BuildingMarket.Common;
using BuildingMarket.Properties.Api.HostedServices;
using BuildingMarket.Properties.Application;
using BuildingMarket.Properties.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGenBearer();
services.AddCommonServices(configuration);
services.AddWorkerConfiguration(configuration);
services.AddAuthenticationServices(configuration);
services.AddApplicationServices(configuration);
services.AddInfrastructureServices(configuration);
services.AddSingleton<RecommendationUploaderService>();
services.AddSingleton<PropertiesUploaderService>();
services.AddHostedService(provider => provider.GetRequiredService<RecommendationUploaderService>());
services.AddHostedService(provider => provider.GetRequiredService<PropertiesUploaderService>());

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
app.Run();