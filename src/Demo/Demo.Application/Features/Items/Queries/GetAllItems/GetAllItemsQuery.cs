using Demo.Domain.Enities;
using MediatR;

namespace Demo.Application.Features.Items.Queries.GetAllItems
{
    public class GetAllItemsQuery : IRequest<IEnumerable<Item>>
    {
    }
}
