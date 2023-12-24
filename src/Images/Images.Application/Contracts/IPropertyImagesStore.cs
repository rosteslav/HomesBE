namespace BuildingMarket.Images.Application.Contracts
{
    public interface IPropertyImagesStore
    {
        Task UpdatePropertyImages(int propertyId, IEnumerable<string> imageURLs, CancellationToken cancellationToken);
    }
}
