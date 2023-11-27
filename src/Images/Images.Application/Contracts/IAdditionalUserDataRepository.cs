namespace BuildingMarket.Images.Application.Contracts
{
    using BuildingMarket.Auth.Domain.Entities;

    public interface IAdditionalUserDataRepository
    {
        Task AddUserImage(AdditionalUserData userData);

        Task DeleteUserImage(string userId);
    }
}
