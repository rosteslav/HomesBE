using BuildingMarket.Common;
using BuildingMarket.Properties.Application;
using BuildingMarket.Properties.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGenBearer();
services.AddCommonServices(configuration);
services.AddAuthenticationServices(configuration);
services.AddApplicationServices(configuration);
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