using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.DeleteUserImage
{
    public class DeleteUserImageCommandHandler(
        IUserImagesRepository repository) : IRequestHandler<DeleteUserImageCommand>
    {
        private readonly IUserImagesRepository _repository = repository;

        public async Task Handle(
            DeleteUserImageCommand request,
            CancellationToken cancellationToken)
            => await _repository.Delete(request.UserId);
    }
}
