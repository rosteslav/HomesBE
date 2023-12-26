using BuildingMarket.Common;
using BuildingMarket.Images.Api.HostedServices;
using BuildingMarket.Images.Application;
using BuildingMarket.Images.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddSwaggerGenBearer();
services.AddWorkerConfiguration(configuration);
services.AddAuthenticationServices(configuration);
services.AddCommonServices(configuration);
services.AddApplicationServices(configuration);
services.AddInfrastructureServices(configuration);
services.AddSingleton<ImageUploaderService>();
services.AddHostedService(provider => provider.GetRequiredService<ImageUploaderService>());

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