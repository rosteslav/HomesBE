using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetBySeller
{
    public class GetBySellerQuery : IRequest<IEnumerable<PropertyModel>>
    {
        public string SellerId { get; set; }
    }
}
