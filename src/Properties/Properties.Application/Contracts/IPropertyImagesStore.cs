using BuildingMarket.Properties.Application.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertyImagesStore
    {
        Task<IEnumerable<PropertyImagesModel>> GetPropertiesImages(params string[] propertyIds);
    }
}
