using BuildingMarket.Admins.Application.Models;

namespace BuildingMarket.Admins.Application.Contracts
{
    public interface IReportsStore
    {
        Task<List<AllReportsModel>> GetAllReports(CancellationToken cancellationToken);

        Task DeletePropertyReports(int propertyId, CancellationToken cancellationToken);
    }
}
