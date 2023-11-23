using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllBuilidngTypes
{
    public class GetAllPropertyOptionsQuery : IRequest<PropertyOptionsModel>
    {
        public PropertyOptionsModel PropertyOptions { get; set; }
    }
}
