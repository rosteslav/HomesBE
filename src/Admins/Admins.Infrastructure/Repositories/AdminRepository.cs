using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Admins.Domain.Entities;
using BuildingMarket.Admins.Infrastructure.Persistence;
using BuildingMarket.Common.Models.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text;

namespace BuildingMarket.Admins.Infrastructure.Repositories
{
    public class AdminRepository(
        AdminsDbContext context,
        UserManager<IdentityUser> userManager,
        ILogger<AdminRepository> logger)
        : IAdminRepository
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

        private readonly AdminsDbContext _context = context;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly ILogger<AdminRepository> _logger = logger;

        public async Task AddMultiplePropertiesFromCsvFile(IFormFile csvFile)
        {
            _logger.LogInformation($"DB add multiple properties");
            
            var properties = await MapPropertiesFromCsvFile(csvFile);
            if (properties.Any())
            {
                try
                {
                    await _context.Properties.AddRangeAsync(properties);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"DB the properties have been successfully added");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while adding multiple properties");
                }
            }
            else
            {
                _logger.LogWarning($"The .csv file doesn't have any valid properties to add");
            }
        }

        public async Task<IEnumerable<IdentityUser>> GetAllBrokers()
        {
            _logger.LogInformation("DB get all brokers");

            try
            {
                var brokers = await _userManager.GetUsersInRoleAsync(UserRoles.Broker);
                return brokers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting brokers");
            }

            return Enumerable.Empty<IdentityUser>();
        }

        private async Task<IEnumerable<Property>> MapPropertiesFromCsvFile(IFormFile file)
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
                        properties.Add(MapProperty(data));
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

        private static Property MapProperty(string[] data)
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
