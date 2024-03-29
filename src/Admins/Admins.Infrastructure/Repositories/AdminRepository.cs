﻿using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Admins.Domain.Entities;
using BuildingMarket.Admins.Infrastructure.Persistence;
using BuildingMarket.Common.Models;
using BuildingMarket.Common.Models.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace BuildingMarket.Admins.Infrastructure.Repositories
{
    public class AdminRepository(
        AdminsDbContext context,
        UserManager<IdentityUser> userManager,
        ILogger<AdminRepository> logger)
        : IAdminRepository
    {
        private const int NumberOfRoomsPosition = 0;
        private const int SpacePosition = 1;
        private const int PricePosition = 2;
        private const int FloorPosition = 3;
        private const int TotalFloorsInBuildingPosition = 4;
        private const int BuildingTypePosition = 5;
        private const int FinishPosition = 6;
        private const int FurnishmentPosition = 7;
        private const int GaragePosition = 8;
        private const int HeatingPosition = 9;
        private const int NeighborhoodPosition = 10;
        private const int SellerIdPosition = 11;
        private const int BrokerIdPosition = 12;
        private const int DescriptionPosition = 13;
        private const int TotalNumberOfPositions = 14;
        private const int ExposurePosition = 15;

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

        public async Task AddNeighbourhoodsRating(NeighbourhoodsRatingModel rating, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"DB add neighbourhoods rating");

            try
            {
                var ratings = new[]
                {
                    new NeighbourhoodsRating
                    {
                        Id = 1,
                        ForLiving = JsonSerializer.Serialize(rating.ForLiving.First()),
                        ForInvestment = JsonSerializer.Serialize(rating.ForInvestment.First()),
                        Budget = JsonSerializer.Serialize(rating.Budget.First()),
                        Luxury = JsonSerializer.Serialize(rating.Luxury.First())
                    },
                    new NeighbourhoodsRating
                    {
                        Id = 2,
                        ForLiving = JsonSerializer.Serialize(rating.ForLiving.Last()),
                        ForInvestment = JsonSerializer.Serialize(rating.ForInvestment.Last()),
                        Budget = JsonSerializer.Serialize(rating.Budget.Last()),
                        Luxury = JsonSerializer.Serialize(rating.Luxury.Last())
                    }
                };

                await _context.NeighbourhoodsRating.ExecuteDeleteAsync(cancellationToken);

                await _context.NeighbourhoodsRating.AddRangeAsync(ratings, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding neighbourhoods rating.");
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

        public async Task<NeighbourhoodsRatingModel> GetNeighbourhoodsRating(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DB get neighbourhoods rating");

            try
            {
                var ratings = await _context.NeighbourhoodsRating.ToArrayAsync(cancellationToken);
                var result = new NeighbourhoodsRatingModel
                {
                    ForLiving = ratings.Select(r => JsonSerializer.Deserialize<IEnumerable<string>>(r.ForLiving)),
                    ForInvestment = ratings.Select(r => JsonSerializer.Deserialize<IEnumerable<string>>(r.ForInvestment)),
                    Budget = ratings.Select(r => JsonSerializer.Deserialize<IEnumerable<string>>(r.Budget)),
                    Luxury = ratings.Select(r => JsonSerializer.Deserialize<IEnumerable<string>>(r.Luxury))
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting neighbourhoods rating");
            }

            return new NeighbourhoodsRatingModel();
        }

        public async Task<IDictionary<string, IEnumerable<string>>> GetNeighbourhoodsRegions(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DB get neighbourhoods regions");

            try
            {
                var regions = await _context.Neighborhoods
                    .GroupBy(n => n.Region)
                    .ToDictionaryAsync(model => model.Key, model => model.Select(n => n.Description), cancellationToken);

                return regions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting neighbourhoods regions");
            }

            return default;
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
                    string[] data = line.Split(',');
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
                NumberOfRooms = SetValueOrThrow(data[NumberOfRoomsPosition]),
                Space = decimal.Parse(data[SpacePosition]),
                Price = decimal.Parse(data[PricePosition]),
                BuildingType = SetValueOrThrow(data[BuildingTypePosition]),
                Exposure = SetValueOrThrow(data[ExposurePosition]),
                Finish = SetValueOrThrow(data[FinishPosition]),
                Furnishment = SetValueOrThrow(data[FurnishmentPosition]),
                Garage = SetValueOrThrow(data[GaragePosition]),
                Heating = SetValueOrThrow(data[HeatingPosition]),
                Neighbourhood = SetValueOrThrow(data[NeighborhoodPosition]),
                Floor = int.Parse(data[FloorPosition]),
                TotalFloorsInBuilding = int.Parse(data[TotalFloorsInBuildingPosition]),
                SellerId = SetValueOrThrow(data[SellerIdPosition]),
                BrokerId = SetValueOrNull(data[BrokerIdPosition]),
                Description = SetValueOrNull(data[DescriptionPosition]),
                CreatedOnUtcTime = DateTime.UtcNow
            };

        private static string SetValueOrThrow(string value)
            => string.IsNullOrWhiteSpace(value)
                ? throw new FormatException("The value cannot be null, empty or white space!")
                : value;

        private static string SetValueOrNull(string value)
            => string.IsNullOrWhiteSpace(value) ? null : value;
    }
}
