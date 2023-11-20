using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllFurnishmentTypes
{
    public class GetAllFurnishmentTypesQuery : IRequest<IEnumerable<string>>
    {
        public string Description { get; set; }
    }
}
