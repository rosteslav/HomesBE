using BuildingMarket.Admins.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BuildingMarket.Admins.Application.Features.Admins.Commands.AddMultipleProperties
{
    public class AddMultiplePropertiesCommandHandler(
        IConfiguration configuration,
        ILogger<AddMultiplePropertiesCommandHandler> logger)
        : IRequestHandler<AddMultiplePropertiesCommand, Response>
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AddMultiplePropertiesCommandHandler> _logger = logger;

        public async Task<Response> Handle(AddMultiplePropertiesCommand request, CancellationToken cancellationToken)
        {
            var properties = await MapPropertiesFromFile(request.File);

            return properties.Any()
                ? await SendProperties(properties, request.JWT)
                : new Response { Status = "Nothing for add", Message = "The file you provided doesn't have any valid properties." };
        }

        private async Task<IEnumerable<PropertyModel>> MapPropertiesFromFile(IFormFile file)
        {
            var properties = new List<PropertyModel>();

            using (var streamReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                int row = 1;
                string line = await streamReader.ReadLineAsync();
                while (!string.IsNullOrWhiteSpace(line))
                {
                    string[] data = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length < 7)
                    {
                        _logger.LogWarning($"The property data is invalid on row {row++}.");
                        line = await streamReader.ReadLineAsync();
                        continue;
                    }

                    try
                    {
                        properties.Add(MapPropertyModel(data));
                    }
                    catch (FormatException ex)
                    {
                        _logger.LogWarning(ex, $"The property on row {row} contains invalid data.");
                    }

                    line = await streamReader.ReadLineAsync();
                    row++;
                }
            }

            return properties;
        }

        private static PropertyModel MapPropertyModel(string[] data)
        {
            var model = new PropertyModel
            {
                Type = data[0],
                NumberOfRooms = int.Parse(data[1]),
                District = data[2],
                Space = decimal.Parse(data[3]),
                Floor = int.Parse(data[4]),
                TotalFloorsInBuilding = int.Parse(data[5]),
                SellerId = data[6]
            };

            if (data.Length > 7)
            {
                model.BrokerId = data[7];
            }

            return model;
        }

        private async Task<Response> SendProperties(IEnumerable<PropertyModel> properties, string jwt)
        {
            string host = _configuration.GetValue<string>("PropertiesMicroserviceHost");
            string apiEndpoint = "/Admins/Properties";
            string[] jwtData = jwt.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtData[0], jwtData[1]);
            HttpResponseMessage response = await client.PostAsJsonAsync(apiEndpoint, properties);

            string responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var responseObject = JsonSerializer.Deserialize<Response>(responseBody, options);

            return responseObject;
        }
    }
}
