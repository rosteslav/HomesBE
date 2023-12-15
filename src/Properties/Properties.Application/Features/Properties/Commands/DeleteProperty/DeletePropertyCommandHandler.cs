using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler(
        IPropertiesRepository repository)
        : IRequestHandler<DeletePropertyCommand, DeletePropertyResult>
    {
        private readonly IPropertiesRepository _repository = repository;

        public async Task<DeletePropertyResult> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.Exists(request.PropertyId))
            {
                return DeletePropertyResult.NotFound;
            }

            if (!await _repository.IsOwner(request.UserId, request.PropertyId))
            {
                return DeletePropertyResult.Unauthorized;
            }

            await _repository.DeleteById(request.PropertyId);
            return DeletePropertyResult.Success;
        }
    }
}
