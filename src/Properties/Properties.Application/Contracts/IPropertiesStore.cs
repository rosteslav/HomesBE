using BuildingMarket.Properties.Application.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesStore
    {
        Task UploadProperties(IDictionary<int, PropertyRedisModel> properties, CancellationToken cancellationToken);
    }
}
