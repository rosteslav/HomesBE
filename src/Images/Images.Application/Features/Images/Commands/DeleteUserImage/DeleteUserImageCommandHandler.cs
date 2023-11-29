using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Images.Commands.DeleteUserImage
{
    public class DeleteUserImageCommandHandler(
        IImagesRepository imagesRepository) : IRequestHandler<DeleteUserImageCommand>
    {
        private readonly IImagesRepository _imagesRepository = imagesRepository;

        public async Task Handle(
            DeleteUserImageCommand request,
            CancellationToken cancellationToken)
            => await _imagesRepository.DeleteUserImage(request.UserId);
    }
}
