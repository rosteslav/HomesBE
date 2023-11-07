using Demo.Application.Models.Security;
using Demo.Application.Models.Security.Enums;
using MediatR;

namespace Demo.Application.Features.Security.Commands.Register
{
    public class RegisterCommand : IRequest<RegistrationResult>
    {
        public RegisterModel Model { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
