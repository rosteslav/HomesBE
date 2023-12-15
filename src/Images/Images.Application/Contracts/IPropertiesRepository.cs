namespace BuildingMarket.Images.Application.Contracts
{
    public interface IPropertiesRepository
    {
        Task<bool> IsPropertyOwner(int propertyId, string userId);

        Task<bool> PropertyExists(int propertyId);
    }
}
