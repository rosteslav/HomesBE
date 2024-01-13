using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler(IPropertiesRepository repository)
        : IRequestHandler<DeletePropertyCommand, PropertyResult>
    {
        private readonly IPropertiesRepository _repository = repository;

        public async Task<PropertyResult> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.Exists(request.PropertyId, cancellationToken))
                return PropertyResult.NotFound;

            if (!request.IsAdmin && !await _repository.IsOwner(request.UserId, request.PropertyId, cancellationToken))
                return PropertyResult.Unauthorized;

            await _repository.DeleteById(request.PropertyId, cancellationToken);

            return PropertyResult.Success;
        }
    }
}
