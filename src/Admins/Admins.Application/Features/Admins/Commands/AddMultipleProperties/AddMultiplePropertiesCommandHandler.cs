using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Admins.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;

namespace BuildingMarket.Admins.Application.Features.Admins.Commands.AddMultipleProperties
{
    public class AddMultiplePropertiesCommandHandler(
        IAdminRepository adminRepository,
        ILogger<AddMultiplePropertiesCommandHandler> logger)
        : IRequestHandler<AddMultiplePropertiesCommand>
    {
        private const int TypePosition = 0;
        private const int NumberOfRoomsPosition = 1;
        private const int DistrictPosition = 2;
        private const int SpacePosition = 3;
        private const int FloorPosition = 4;
        private const int TotalFloorsInBuildingPosition = 5;
        private const int SellerIdPosition = 6;
        private const int BrokerIdPosition = 7;
        private const int TotalNumberOfPositions = 8;

        private readonly IAdminRepository _adminRepository = adminRepository;
        private readonly ILogger<AddMultiplePropertiesCommandHandler> _logger = logger;

        public async Task Handle(AddMultiplePropertiesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Reading the .csv file...");
            var properties = await MapPropertiesFromFile(request.File);
            if (properties.Any())
            {
                await _adminRepository.AddMultipleProperties(properties);
            }
        }

        private async Task<IEnumerable<Property>> MapPropertiesFromFile(IFormFile file)
        {
            var properties = new List<Property>();

            using (var streamReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                int row = 1;
                string line = await streamReader.ReadLineAsync();
                while (!string.IsNullOrWhiteSpace(line))
                {
                    string[] data = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length < TotalNumberOfPositions)
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

        private static Property MapPropertyModel(string[] data)
            => new Property
            {
                Type = data[TypePosition],
                NumberOfRooms = int.Parse(data[NumberOfRoomsPosition]),
                District = data[DistrictPosition],
                Space = decimal.Parse(data[SpacePosition]),
                Floor = int.Parse(data[FloorPosition]),
                TotalFloorsInBuilding = int.Parse(data[TotalFloorsInBuildingPosition]),
                SellerId = data[SellerIdPosition],
                BrokerId = data[BrokerIdPosition]
            };
    }
}
