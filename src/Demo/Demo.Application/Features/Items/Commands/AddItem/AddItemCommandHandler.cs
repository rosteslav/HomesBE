using Demo.Application.Contracts;
using Demo.Domain.Enities;
using MediatR;

namespace Demo.Application.Features.Items.Commands.AddItem
{
    public class AddItemCommandHandler(IItemRepository itemRepository) : IRequestHandler<AddItemCommand>
    {
        private readonly IItemRepository _itemRepository = itemRepository;

        public async Task Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Item(request.Name);
            await _itemRepository.Add(item);
        }
    }
}
