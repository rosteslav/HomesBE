using BuildingMarket.Properties.Application.Models.Security;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IRecommendationRepository
    {
        Task<IEnumerable<int>> GetRecommended(PreferencesModel preferences, CancellationToken cancellationToken);
    }
}
