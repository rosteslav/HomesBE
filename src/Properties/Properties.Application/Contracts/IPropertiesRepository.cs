using BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesRepository
    {
        Task<IEnumerable<GetAllPropertiesOutputModel>> Get(GetAllPropertiesQuery query);
        
        Task<PropertyModel> GetById(int id);

        Task<IEnumerable<PropertyModel>> GetBySeller(string sellerId);

        Task<IEnumerable<PropertyModel>> GetByBroker(string brokerId);

        Task<AddPropertyOutputModel> Add(Property item);

        Task DeleteById(int id);

        Task<bool> Exists(int id);

        Task<bool> IsOwner(string userId, int propertyId);
    }
}