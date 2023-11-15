using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.Security.Enums;
using MediatR;

namespace BuildingMarket.Auth.Application.Features.Security.Commands.Register
{
    public class RegisterCommandHandler(ISecurityService securityService) : IRequestHandler<RegisterCommand, RegistrationResult>
    {
        private readonly ISecurityService _securityService = securityService;

        public async Task<RegistrationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
            => await _securityService.Registration(
                request.Model,
                request.Roles);
    }
}
