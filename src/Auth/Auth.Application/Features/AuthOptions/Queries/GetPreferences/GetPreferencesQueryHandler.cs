using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.AuthOptions;
using MediatR;

namespace BuildingMarket.Auth.Application.Features.AuthOptions.Queries.GetPreferences
{
    public class GetPreferencesQueryHandler(IAuthOptionsRepository authOptionsRepository)
        : IRequestHandler<GetPreferencesQuery, PreferencesOutputModel>
    {
        private readonly IAuthOptionsRepository _authOptionsRepository = authOptionsRepository;

        public async Task<PreferencesOutputModel> Handle(GetPreferencesQuery request, CancellationToken cancellationToken)
            => await _authOptionsRepository.GetPreferences();
    }
}
