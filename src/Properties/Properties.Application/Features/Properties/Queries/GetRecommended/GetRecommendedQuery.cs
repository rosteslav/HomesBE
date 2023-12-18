using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetRecommended
{
    public class GetRecommendedQuery :
        IRequest<IEnumerable<GetAllPropertiesOutputModel>> { }
}
