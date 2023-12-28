using BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesRepository
    {
        Task<IEnumerable<GetAllPropertiesOutputModel>> Get(GetAllPropertiesQuery query);

        Task<PropertyModel> GetById(int id, CancellationToken cancellationToken);

        Task<IEnumerable<GetAllPropertiesOutputModel>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken);

        Task<IEnumerable<PropertyModelWithId>> GetBySeller(string sellerId, CancellationToken cancellationToken);

        Task<IEnumerable<PropertyModelWithId>> GetByBroker(string brokerId, CancellationToken cancellationToken);

        Task<AddPropertyOutputModel> Add(Property item);

        Task DeleteById(int id);

        Task EditById(int id, AddPropertyInputModel editedProperty);

        Task<bool> Exists(int id);

        Task<bool> IsOwner(string userId, int propertyId);
    }
}