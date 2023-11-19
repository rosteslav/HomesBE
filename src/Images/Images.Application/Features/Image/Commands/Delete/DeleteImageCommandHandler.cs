using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Commands.Delete
{
    public class DeleteImageCommandHandler(
        IImagesRepository repository)
        : IRequestHandler<DeleteImageCommand>
    {
        private readonly IImagesRepository _repository = repository;

        public async Task Handle(DeleteImageCommand request,
            CancellationToken cancellationToken)
        {
            await _repository.Delete(request.ImageId);
        }
    }
}
