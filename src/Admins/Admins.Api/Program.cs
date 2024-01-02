using BuildingMarket.Admins.Application;
using BuildingMarket.Admins.Infrastructure;
using BuildingMarket.Common;

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
