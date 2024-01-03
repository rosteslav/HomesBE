using BuildingMarket.Admins.Application.Contracts;
using MediatR;

namespace BuildingMarket.Admins.Application.Features.Admins.Queries.GetNeighbourhoodsRegions
{
    public class GetNeighbourhoodsRegionsQueryHandler(IAdminRepository adminRepository)
        : IRequestHandler<GetNeighbourhoodsRegionsQuery, IDictionary<string, IEnumerable<string>>>
    {
        private readonly IAdminRepository _adminRepository = adminRepository;

        public async Task<IDictionary<string, IEnumerable<string>>> Handle(GetNeighbourhoodsRegionsQuery request, CancellationToken cancellationToken)
            => await _adminRepository.GetNeighbourhoodsRegions(cancellationToken);
    }
}
