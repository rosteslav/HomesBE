namespace BuildingMarket.Images.Application.Contracts
{
    public interface IUserImagesRepository
    {
        Task Add(string imageUrl, string userId);
        Task Delete(string userId);
    }
}
