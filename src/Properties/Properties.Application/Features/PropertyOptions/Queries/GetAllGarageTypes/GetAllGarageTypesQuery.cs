using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllGarageTypes
{
    public class GetAllGarageTypesQuery : IRequest<IEnumerable<string>>
    {
        public string Description { get; set; }
    }
}
