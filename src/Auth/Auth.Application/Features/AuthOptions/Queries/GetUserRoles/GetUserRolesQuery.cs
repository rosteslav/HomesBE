using BuildingMarket.Auth.Application.Models.Security;
using MediatR;

namespace BuildingMarket.Auth.Application.Features.AuthOptions.Queries.GetUserRoles
{
    public class GetUserRolesQuery : IRequest<UserRolesModel>
    {
    }
}
