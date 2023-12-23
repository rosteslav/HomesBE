using BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesRepository
    {
        Task<IList<GetAllPropertiesOutputModel>> Get(GetAllPropertiesQuery query);

        Task<PropertyModel> GetById(int id, CancellationToken cancellationToken);

        Task<IList<PropertyModelWithId>> GetBySeller(string sellerId, CancellationToken cancellationToken);

        Task<IList<PropertyModelWithId>> GetByBroker(string brokerId, CancellationToken cancellationToken);

        Task<AddPropertyOutputModel> Add(Property item);

        Task DeleteById(int id);

        Task EditById(int id, AddPropertyInputModel editedProperty);

        Task<bool> Exists(int id);

        Task<bool> IsOwner(string userId, int propertyId);

        Task<IEnumerable<GetAllPropertiesOutputModel>> GetRecommended(CancellationToken cancellationToken);
    }
}