using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetById
{
    public class GetByIdQuery : IRequest<PropertyModel>
    {
        public required int Id { get; set; }
    }
}
