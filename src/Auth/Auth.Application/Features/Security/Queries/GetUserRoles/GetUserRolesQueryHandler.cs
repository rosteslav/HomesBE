using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.Security;
using MediatR;

namespace BuildingMarket.Auth.Application.Features.Security.Queries.GetUserRoles
{
    public class GetUserRolesQueryHandler(ISecurityService securityService) : IRequestHandler<GetUserRolesQuery, UserRolesModel>
    {
        private readonly ISecurityService _securityService = securityService;

        public async Task<UserRolesModel> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
            => await _securityService.GetUserRoles();
    }
}
