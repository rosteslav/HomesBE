using BuildingMarket.Auth.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Auth.Application.Features.Preferences.Commands
{
    public class SetBuyersPreferencesCommandHandler(
        ILogger<SetBuyersPreferencesCommandHandler> logger,
        IPreferencesStore preferencesStore,
        IPreferencesRepository repository)
        : IRequestHandler<SetBuyersPreferencesCommand>
    {
        private readonly IPreferencesStore _preferencesStore = preferencesStore;
        private readonly IPreferencesRepository _repository = repository;
        private readonly ILogger<SetBuyersPreferencesCommandHandler> _logger = logger;

        public async Task Handle(SetBuyersPreferencesCommand request,
            CancellationToken cancellationToken)
        {
            var allBuyersPreferences = await _repository.GetAllBuyersPreferences(cancellationToken);

            if (allBuyersPreferences.Any())
            {
                await _preferencesStore.SetBuyersPreferences(allBuyersPreferences, cancellationToken);
            }
            else
            {
                _logger.LogInformation("No preferences to upload!");
            }
        }
    }
}
