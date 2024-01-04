using BuildingMarket.Auth.Domain.Entities;

namespace BuildingMarket.Auth.Application.Contracts
{
    public interface IAdditionalUserDataRepository
    {
        Task Add(AdditionalUserData item);
        Task<AdditionalUserData> GetById(string userId);
    }
}
