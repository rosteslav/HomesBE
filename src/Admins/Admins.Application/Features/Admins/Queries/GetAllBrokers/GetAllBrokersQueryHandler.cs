using BuildingMarket.Admins.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuildingMarket.Admins.Application.Features.Admins.Queries.GetAllBrokers
{
    public class GetAllBrokersQueryHandler(IAdminRepository adminsRepository)
        : IRequestHandler<GetAllBrokersQuery, IEnumerable<IdentityUser>>
    {
        private readonly IAdminRepository _adminsRepository = adminsRepository;

        public Task<IEnumerable<IdentityUser>> Handle(GetAllBrokersQuery request, CancellationToken cancellationToken)
            => _adminsRepository.GetAllBrokers();
    }
}
