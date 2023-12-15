using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BuildingMarket.Admins.Application.Contracts
{
    public interface IAdminRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllBrokers();
        Task AddMultiplePropertiesFromCsvFile(IFormFile csvFile);
    }
}
