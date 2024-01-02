using BuildingMarket.Admins.Application.Features.Admins.Commands.AddNeighbourhoodsRating;
using BuildingMarket.Admins.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BuildingMarket.Admins.Application.Contracts
{
    public interface IAdminRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllBrokers();
        Task AddMultiplePropertiesFromCsvFile(IFormFile csvFile);
        Task AddNeighbourhoodsRating(AddNeighbourhoodsRatingCommand command, CancellationToken cancellationToken);
        Task<NeighbourhoodsRatingModel> GetNeighbourhoodsRating(CancellationToken cancellationToken);
    }
}
