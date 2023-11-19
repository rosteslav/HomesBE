namespace BuildingMarket.Images.Application.Contracts
{
    public interface IPropertiesService
    {
        Task<bool> IsPropertyOwner(int propertyId, string userId);

        Task<bool> PropertyExists(int propertyId);
    }
}
