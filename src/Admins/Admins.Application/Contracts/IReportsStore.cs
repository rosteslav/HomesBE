namespace BuildingMarket.Admins.Application.Contracts
{
    public interface IReportsStore
    {
        Task GetAllReports(CancellationToken cancellationToken);

        Task DeletePropertyReports(int propertyId, CancellationToken cancellationToken);
    }
}
