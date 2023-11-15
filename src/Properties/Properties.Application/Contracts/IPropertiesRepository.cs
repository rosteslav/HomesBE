using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesRepository
    {
        Task<IEnumerable<Property>> Get();

        Task<IEnumerable<Property>> GetBySeller(string sellerId);

        Task<IEnumerable<Property>> GetByBroker(string brokerId);

        Task Add(Property item);
    }
}