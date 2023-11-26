using BuildingMarket.Auth.Domain.Entities;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IAdditionalUserDataRepository
    {
        Task AddAsync(AdditionalUserData item);
    }
}
