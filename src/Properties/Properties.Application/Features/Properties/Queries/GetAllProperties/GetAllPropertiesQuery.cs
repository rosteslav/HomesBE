using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQuery : IRequest<IEnumerable<GetAllPropertiesOutputModel>>
    {
        public GetAllPropertiesInputModel Input { get; set; }

        public bool IsAdmin { get; set; }
    }
}
