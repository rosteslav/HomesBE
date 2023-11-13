using Demo.Application.Contracts;
using MediatR;

namespace Demo.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler(IPropertyRepository repository) : IRequestHandler<DeletePropertyCommand>
    {
        private readonly IPropertyRepository _repository = repository;

        public async Task Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
            => await _repository.Delete(request.Id);
    }
}
