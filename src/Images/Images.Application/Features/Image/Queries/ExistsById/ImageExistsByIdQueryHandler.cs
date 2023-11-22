using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Queries.ExistsById
{
    public class ImageExistsByIdQueryHandler(IImagesRepository repository)
        : IRequestHandler<ImageExistsByIdQuery, bool>
    {
        private readonly IImagesRepository _repository = repository;

        public Task<bool> Handle(ImageExistsByIdQuery request, CancellationToken cancellationToken)
            => _repository.Exists(request.ImageId);
    }
}
