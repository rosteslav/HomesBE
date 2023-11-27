using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.Security;
using MediatR;

namespace BuildingMarket.Auth.Application.Features.AuthOptions.Queries.GetUserRoles
{
    public class GetUserRolesQueryHandler(IAuthOptionsRepository authOptionsRepository) : IRequestHandler<GetUserRolesQuery, UserRolesModel>
    {
        private readonly IAuthOptionsRepository _authOptionsRepository = authOptionsRepository;

        public async Task<UserRolesModel> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
            => await _authOptionsRepository.GetUserRoles();
    }
}
