using BuildingMarket.Auth.Domain.Entities;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IAdditionalUserDataRepository
    {
        Task<IEnumerable<AdditionalUserData>> GetAllAsync();

        Task AddAsync(AdditionalUserData item);
    }
}
