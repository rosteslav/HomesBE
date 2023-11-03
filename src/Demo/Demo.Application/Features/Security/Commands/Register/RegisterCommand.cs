using Demo.Application.Auth;
using Demo.Application.Models.Security;
using MediatR;

namespace Demo.Application.Features.Security.Commands.Register
{
    public class RegisterCommand : IRequest<RegistrationResult>
    {
        public RegisterModel RegisterModel { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
