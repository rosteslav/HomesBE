namespace BuildingMarket.Images.Application.Contracts
{
    using BuildingMarket.Auth.Domain.Entities;

    public interface IAdditionalUserDataRepository
    {
        Task AddUserImage(string userId, string imageUrl);

        Task DeleteUserImage(string userId);
    }
}
