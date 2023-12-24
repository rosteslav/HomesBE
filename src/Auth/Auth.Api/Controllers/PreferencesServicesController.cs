using BuildingMarket.Auth.Api.HostedServices;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Auth.Api.Controllers
{
    public class PreferencesServicesController(
        ILogger<PreferencesServicesController> logger,
        BuyerPreferencesService preferencesService
        ) : ControllerBase
    {
        private readonly ILogger<PreferencesServicesController> _logger = logger;
        private readonly BuyerPreferencesService _preferencesService = preferencesService;

        [HttpGet("force/test")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ForceTestRervice()
        {
            await Task.Yield();
            _logger.LogInformation("{service} is forced.", nameof(BuyerPreferencesService));

            _preferencesService.IsForced = true;

            return NoContent();
        }
    }
}
