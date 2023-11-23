using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertyOptionsQuery : IRequest<IEnumerable<Property>>
    {
    }
}
