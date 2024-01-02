using BuildingMarket.Admins.Application.Contracts;
using MediatR;

namespace BuildingMarket.Admins.Application.Features.Admins.Commands.AddNeighbourhoodsRating
{
    public class AddNeighbourhoodsRatingCommandHandler(IAdminRepository adminRepository) : IRequestHandler<AddNeighbourhoodsRatingCommand>
    {
        private readonly IAdminRepository _adminRepository = adminRepository;

        public async Task Handle(AddNeighbourhoodsRatingCommand request, CancellationToken cancellationToken)
            => await _adminRepository.AddNeighbourhoodsRating(request, cancellationToken);
    }
}
