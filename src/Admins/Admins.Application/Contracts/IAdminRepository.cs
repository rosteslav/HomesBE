﻿using BuildingMarket.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BuildingMarket.Admins.Application.Contracts
{
    public interface IAdminRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllBrokers();
        Task AddMultiplePropertiesFromCsvFile(IFormFile csvFile);
        Task AddNeighbourhoodsRating(NeighbourhoodsRatingModel rating, CancellationToken cancellationToken);
        Task<NeighbourhoodsRatingModel> GetNeighbourhoodsRating(CancellationToken cancellationToken);
        Task<IDictionary<string, IEnumerable<string>>> GetNeighbourhoodsRegions(CancellationToken cancellationToken);
    }
}
