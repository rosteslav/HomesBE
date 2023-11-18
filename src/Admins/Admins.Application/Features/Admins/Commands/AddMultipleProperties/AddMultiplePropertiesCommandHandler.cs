using BuildingMarket.Admins.Application.Contracts;
using MediatR;

namespace BuildingMarket.Admins.Application.Features.Admins.Commands.AddMultipleProperties
{
    public class AddMultiplePropertiesCommandHandler(IAdminRepository adminRepository) 
        : IRequestHandler<AddMultiplePropertiesCommand>
    {
        private readonly IAdminRepository _adminRepository = adminRepository;

        public async Task Handle(AddMultiplePropertiesCommand request, CancellationToken cancellationToken)
            => await _adminRepository.AddMultiplePropertiesFromCsvFile(request.File);
    }
}
