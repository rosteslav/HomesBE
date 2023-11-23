using BuildingMarket.Images.Domain.Entities;

namespace BuildingMarket.Images.Application.Contracts
{
    public interface IImagesRepository
    {
        Task<IEnumerable<Image>> GetAllForProperty(int propertyId);
        Task Add(Image image);
        Task Delete(int imageId);
        Task<int> GetPropertyIdOfImageById(int imageId);
        Task<bool> Exists(int imageId);
    }
}
