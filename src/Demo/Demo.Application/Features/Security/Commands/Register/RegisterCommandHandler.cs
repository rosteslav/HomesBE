using Demo.Application.Contracts;
using Demo.Application.Models.Security;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Demo.Application.Features.Security.Commands.Register
{
    public class RegisterCommandHandler(ISecurityService securityService) : IRequestHandler<RegisterCommand, RegistrationResult>
    {
        private readonly ISecurityService _securityService = securityService;
        public async Task<RegistrationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
            => await _securityService.Registration(
                request.RegisterModel.Username,
                request.RegisterModel.Email,
                request.RegisterModel.Password,
                request.Roles);
    }
}
