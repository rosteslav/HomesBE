using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesRepository
    {
        Task<IEnumerable<GetAllPropertiesOutputModel>> Get();
        
        Task<PropertyModel> GetById(int id);

        Task<IEnumerable<PropertyModel>> GetBySeller(string sellerId);

        Task<IEnumerable<PropertyModel>> GetByBroker(string brokerId);

        Task<AddPropertyOutputModel> Add(Property item);
    }
}