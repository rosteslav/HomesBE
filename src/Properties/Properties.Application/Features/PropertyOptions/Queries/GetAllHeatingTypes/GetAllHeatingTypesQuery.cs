using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllHeatingTypes
{
    public class GetAllHeatingTypesQuery : IRequest<IEnumerable<string>>
    {
        public string Description { get; set; }
    }
}
