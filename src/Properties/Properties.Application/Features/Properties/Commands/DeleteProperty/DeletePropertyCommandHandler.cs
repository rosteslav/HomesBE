using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler(IPropertiesRepository repository, IPropertiesStore store)
        : IRequestHandler<DeletePropertyCommand, PropertyResult>
    {
        private readonly IPropertiesRepository _repository = repository;
        private readonly IPropertiesStore _store = store;

        public async Task<PropertyResult> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.Exists(request.PropertyId))
                return PropertyResult.NotFound;

            if (!request.IsAdmin && !await _repository.IsOwner(request.UserId, request.PropertyId))
                return PropertyResult.Unauthorized;

            await _repository.DeleteById(request.PropertyId);
            await _store.RemoveProperty(request.PropertyId, cancellationToken);

            return PropertyResult.Success;
        }
    }
}
