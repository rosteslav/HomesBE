using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetBySeller
{
    public class GetBySellerQuery : IRequest<IEnumerable<Property>>
    {
        public string SellerId { get; set; }
    }
}
