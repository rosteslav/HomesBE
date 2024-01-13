using BuildingMarket.Properties.Application.Features.Properties.Commands.ReportProperty;
using BuildingMarket.Properties.Application.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesStore
    {
        Task<IEnumerable<PropertyRedisModel>> GetProperties(CancellationToken cancellationToken = default);

        Task RemoveProperty(PropertyRedisModel model, CancellationToken cancellationToken = default);

        Task UpdateProperty(PropertyRedisModel model, CancellationToken cancellationToken = default);

        Task UploadProperties(IEnumerable<PropertyRedisModel> properties, CancellationToken cancellationToken);

        Task UploadReport(ReportPropertyCommand model, CancellationToken cancellationToken = default);
    }
}
