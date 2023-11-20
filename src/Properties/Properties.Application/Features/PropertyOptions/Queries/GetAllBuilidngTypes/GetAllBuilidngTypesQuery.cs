using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllBuilidngTypes
{
    public class GetAllBuildingTypesQuery : IRequest<IEnumerable<string>>
    {
        public string Description { get; set; }
    }
}
