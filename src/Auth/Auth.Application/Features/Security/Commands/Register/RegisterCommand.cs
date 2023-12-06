using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Application.Models.Security.Enums;
using MediatR;

namespace BuildingMarket.Auth.Application.Features.Security.Commands.Register
{
    public class RegisterCommand : IRequest<RegistrationResult>
    {
        public RegisterModel Model { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
