using Demo.Domain.Enities;
using MediatR;

namespace Demo.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQuery : IRequest<IEnumerable<Property>>
    {
    }
}
