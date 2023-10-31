using Demo.Application;
using Demo.Infrastucture;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(); 
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
