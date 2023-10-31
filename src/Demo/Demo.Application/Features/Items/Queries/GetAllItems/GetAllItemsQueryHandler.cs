using Demo.Application.Contracts;
using Demo.Domain.Enities;
using MediatR;

namespace Demo.Application.Features.Items.Queries.GetAllItems
{
    public class GetAllItemsQueryHandler(IItemRepository itemRepository) : IRequestHandler<GetAllItemsQuery, IEnumerable<Item>>
    {
        private readonly IItemRepository _itemRepository = itemRepository;

        public async Task<IEnumerable<Item>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
            => await _itemRepository.Get();
    }
}

