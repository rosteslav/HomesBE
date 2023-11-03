using Demo.Application.Contracts;
using Demo.Application.Models.Security.Enums;
using MediatR;

namespace Demo.Application.Features.Security.Commands.Register
{
    public class RegisterCommandHandler(ISecurityService securityService) : IRequestHandler<RegisterCommand, RegistrationResult>
    {
        private readonly ISecurityService _securityService = securityService;

        public async Task<RegistrationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
            => await _securityService.Registration(
                request.Model.Username,
                request.Model.Email,
                request.Model.Password,
                request.Roles);
    }
}
