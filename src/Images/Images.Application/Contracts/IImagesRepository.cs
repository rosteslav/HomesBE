using BuildingMarket.Images.Domain.Entities;

namespace BuildingMarket.Images.Application.Contracts
{
    public interface IImagesRepository
    {
        Task<IEnumerable<Image>> GetAllForProperty(int propertyId);
        Task AddPropertyImage(Image image);
        Task DeletePropertyImage(int imageId);
        Task AddUserImage(string imageUrl, string userId);
        Task DeleteUserImage(string userId);
        Task<int> GetPropertyIdOfImageById(int imageId);
        Task<bool> Exists(int imageId);
    }
}
