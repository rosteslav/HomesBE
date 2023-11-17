using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Image.Queries.GetAll
{
    public class GetAllCommandHandler(IImagesRepository repository) : IRequestHandler<GetAllCommand, IEnumerable<Domain.Entities.Image>>
    {
        private readonly IImagesRepository _repository = repository;

        public async Task<IEnumerable<Domain.Entities.Image>> Handle(
            GetAllCommand request,
            CancellationToken cancellationToken)
        => await _repository.GetAllForPropertyAsync(request.PropertyId);
    }
}
