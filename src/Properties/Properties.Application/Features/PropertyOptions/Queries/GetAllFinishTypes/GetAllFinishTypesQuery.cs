using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllFinishTypes
{
    public class GetAllFinishTypesQuery : IRequest<IEnumerable<string>>
    {
        public string Description { get; set; }
    }
}
