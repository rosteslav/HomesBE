using BuildingMarket.Images.Application.Models;

namespace BuildingMarket.Images.Application.Contracts
{
    public interface IPropertyImagesStore
    {
        Task UpdatePropertyImages(int propertyId, IEnumerable<string> imageURLs, CancellationToken cancellationToken);

        Task UploadPropertiesImages(IEnumerable<PropertyImagesModel> properties, CancellationToken cancellationToken);
    }
}
