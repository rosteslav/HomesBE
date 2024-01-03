using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Admins.Application.Models;
using MediatR;

namespace BuildingMarket.Admins.Application.Features.Admins.Queries.GetNeighbourhoodsRating
{
    public class GetNeighbourhoodsRatingQueryHandler(IAdminRepository adminRepository) 
        : IRequestHandler<GetNeighbourhoodsRatingQuery, NeighbourhoodsRatingModel>
    {
        private readonly IAdminRepository _adminRepository = adminRepository;

        public async Task<NeighbourhoodsRatingModel> Handle(GetNeighbourhoodsRatingQuery request, CancellationToken cancellationToken)
            => await _adminRepository.GetNeighbourhoodsRating(cancellationToken);
    }
}
