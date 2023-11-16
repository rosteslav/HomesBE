using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.AddMultipleProperties.Commands
{
    public class AddMultiplePropertiesCommandHandler(
        IPropertiesRepository propertiesRepository,
        ILogger<AddMultiplePropertiesCommandHandler> logger)
        : IRequestHandler<AddMultiplePropertiesCommand, int>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly ILogger<AddMultiplePropertiesCommandHandler> _logger = logger;

        public async Task<int> Handle(AddMultiplePropertiesCommand request, CancellationToken cancellationToken)
        {
            var properties = new List<Property>();
            using (var streamReader = new StreamReader(request.File.OpenReadStream(), Encoding.UTF8))
            {
                int row = 1;
                string line = await streamReader.ReadLineAsync();
                while (!string.IsNullOrWhiteSpace(line))
                {
                    string[] data = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length < 6)
                    {
                        _logger.LogWarning($"The property data is invalid on row {row++}.");
                        line = await streamReader.ReadLineAsync();
                        continue;
                    }

                    try
                    {
                        var property = new Property
                        {
                            Type = data[0],
                            NumberOfRooms = int.Parse(data[1]),
                            District = data[2],
                            Space = decimal.Parse(data[3]),
                            Floor = int.Parse(data[4]),
                            TotalFloorsInBuilding = int.Parse(data[5]),
                            SellerId = request.SellerId
                        };

                        if (data.Length > 6)
                        {
                            property.BrokerId = data[6];
                        }

                        properties.Add(property);
                    }
                    catch (FormatException ex)
                    {
                        _logger.LogWarning(ex, $"The property on row {row} contains invalid data.");
                    }

                    line = await streamReader.ReadLineAsync();
                    row++;
                }
            }

            int result = 0;
            if (properties.Any())
            {
                result = await _propertiesRepository.AddMultiple(properties);
            }

            return result;
        }
    }
}
