using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertyOptionsRepository
    {
        Task<IEnumerable<string>> GetBuidingTypes();
        Task<IEnumerable<string>> GetFinishTypes();
        Task<IEnumerable<string>> GetFurnishmentTypes();
        Task<IEnumerable<string>> GetGarageTypes();
        Task<IEnumerable<string>> GetHeatingTypes();
        Task<IEnumerable<string>> GetNeighbourhoods();
    }
}
