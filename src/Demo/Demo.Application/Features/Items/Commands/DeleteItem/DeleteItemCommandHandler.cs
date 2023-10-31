using Demo.Application.Contracts;
using MediatR;

namespace Demo.Application.Features.Items.Commands.DeleteItem
{
    public class DeleteItemCommandHandler(IItemRepository repository) : IRequestHandler<DeleteItemCommand>
    {
        private readonly IItemRepository _repository = repository;

        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken) 
            => await _repository.Delete(request.Id);
    }
}
