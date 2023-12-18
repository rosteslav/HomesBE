using BuildingMarket.Images.Application.Models;

namespace BuildingMarket.Images.Application.Contracts
{
    public interface IPropertyImagesStore
    {
        Task UploadPropertiesImages(IEnumerable<PropertyImagesModel> properties);
    }
}
