using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Queries.GetAll
{
    public class GetAllImagesCommandHandler(IPropertyImagesRepository repository) : IRequestHandler<GetAllImagesCommand, IEnumerable<Domain.Entities.Image>>
    {
        private readonly IPropertyImagesRepository _repository = repository;

        public async Task<IEnumerable<Domain.Entities.Image>> Handle(
            GetAllImagesCommand request,
            CancellationToken cancellationToken)
        => await _repository.GetAllForProperty(request.PropertyId);
    }
}
