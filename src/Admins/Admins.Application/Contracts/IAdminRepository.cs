using Microsoft.AspNetCore.Identity;

namespace BuildingMarket.Admins.Application.Contracts
{
    public interface IAdminRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllBrokers();
    }
}
