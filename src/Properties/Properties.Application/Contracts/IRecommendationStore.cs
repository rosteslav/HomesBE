namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IRecommendationStore
    {
        Task UploadRecommendations(IDictionary<string, IEnumerable<int>> buyersRecommendations, CancellationToken cancellationToken);

        Task<IEnumerable<int>> GetRecommendedByUserId(string buyerId, CancellationToken cancellationToken);
    }
}
