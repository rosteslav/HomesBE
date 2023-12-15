using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetBySeller
{
    public class GetBySellerQuery : IRequest<IEnumerable<PropertyModelWithId>>
    {
        public string SellerId { get; set; }
    }
}
