using BuildingMarket.Properties.Application.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertyOptionsRepository
    {
        Task<PropertyOptionsModel> GetAllPropertyOptions();
    }
}
