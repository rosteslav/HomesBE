using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuildingMarket.Admins.Application.Features.Admins.Queries.GetAllBrokers
{
    public class GetAllBrokersQuery : IRequest<IEnumerable<IdentityUser>>
    {
    }
}
