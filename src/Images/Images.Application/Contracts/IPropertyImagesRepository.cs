using BuildingMarket.Images.Application.Models;
using BuildingMarket.Images.Domain.Entities;

namespace BuildingMarket.Images.Application.Contracts
{
    public interface IPropertyImagesRepository
    {
        Task<IEnumerable<Image>> GetAllForProperty(int propertyId);
        Task<IEnumerable<PropertyImagesModel>> GetAllForAllProperties();
        Task Add(Image image);
        Task Delete(int imageId);
        Task<int> GetPropertyIdOfImageById(int imageId);
        Task<bool> Exists(int imageId);
    }
}
