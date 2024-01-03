using AutoMapper;
using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Admins.Application.Models;
using MediatR;

namespace BuildingMarket.Admins.Application.Features.Admins.Commands.AddNeighbourhoodsRating
{
    public class AddNeighbourhoodsRatingCommandHandler(
        IAdminRepository adminRepository,
        IMapper mapper) 
        : IRequestHandler<AddNeighbourhoodsRatingCommand>
    {
        private readonly IAdminRepository _adminRepository = adminRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(AddNeighbourhoodsRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = _mapper.Map<NeighbourhoodsRatingModel>(request);
            await _adminRepository.AddNeighbourhoodsRating(rating, cancellationToken);
        }
    }
}
