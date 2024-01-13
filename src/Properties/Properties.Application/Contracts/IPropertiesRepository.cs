using BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesRepository
    {
        Task<IEnumerable<GetAllPropertiesOutputModel>> Get(GetAllPropertiesQuery query);

        Task<IEnumerable<PropertyRedisModel>> GetForRecommendations(CancellationToken cancellationToken = default);

        Task<PropertyModel> GetById(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<GetAllPropertiesOutputModel>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default);

        Task<IEnumerable<PropertyModelWithId>> GetBySeller(string sellerId, CancellationToken cancellationToken = default);

        Task<IEnumerable<PropertyModelWithId>> GetByBroker(string brokerId, CancellationToken cancellationToken = default);

        Task<AddPropertyOutputModel> Add(Property item, CancellationToken cancellationToken = default);

        Task DeleteById(int id, CancellationToken cancellationToken = default);

        Task EditById(int id, AddPropertyInputModel editedProperty, CancellationToken cancellationToken = default);

        Task<bool> Exists(int id, CancellationToken cancellationToken = default);

        Task<bool> IsOwner(string userId, int propertyId, CancellationToken cancellationToken = default);
    }
}