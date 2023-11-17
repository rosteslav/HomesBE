using BuildingMarket.Images.Domain.Entities;

namespace BuildingMarket.Images.Application.Contracts
{
    public interface IImagesRepository
    {
        Task<IEnumerable<Image>> GetAllForPropertyAsync(int propertyId);
        Task AddAsync(Image image);
        Task DeleteAsync(string imageUrl);
    }
}
