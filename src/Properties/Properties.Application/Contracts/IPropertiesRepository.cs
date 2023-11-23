using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesRepository
    {
        Task<IEnumerable<Property>> Get();
        
        Task<PropertyModel> GetById(int id);

        Task<IEnumerable<Property>> GetBySeller(string sellerId);

        Task<IEnumerable<Property>> GetByBroker(string brokerId);

        Task<AddPropertyOutputModel> Add(Property item);
    }
}