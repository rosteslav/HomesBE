using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.EditProperty
{
    public class EditPropertyCommandHandler(
        IPropertiesRepository repository)
        : IRequestHandler<EditPropertyCommand, DeletePropertyResult>
    {
        private readonly IPropertiesRepository _repository = repository;

        public async Task<DeletePropertyResult> Handle(
            EditPropertyCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _repository.Exists(request.PropertyId))
            {
                return DeletePropertyResult.NotFound;
            }

            if (!await _repository.IsOwner(request.UserId, request.PropertyId))
            {
                return DeletePropertyResult.Unauthorized;
            }

            await _repository
                .EditById(request.PropertyId, request.EditedProperty);

            return DeletePropertyResult.Success;
        }
    }
}
