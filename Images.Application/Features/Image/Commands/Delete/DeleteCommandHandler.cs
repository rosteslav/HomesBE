using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace Images.Application.Features.Image.Commands.Delete
{
    public class DeleteCommandHandler(
        IImagesRepository repository,
        IImgbbService imgbbService)
        : IRequestHandler<DeleteCommand>
    {
        private readonly IImagesRepository _repository = repository;
        private readonly IImgbbService _imgbbService = imgbbService;

        public async Task Handle(DeleteCommand request,
            CancellationToken cancellationToken)
        {
            await _imgbbService.DeleteImage(request.DeleteURL);
            await _repository.DeleteAsync(request.DeleteURL);
        }
    }
}
