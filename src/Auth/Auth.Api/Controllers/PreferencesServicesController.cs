using BuildingMarket.Auth.Api.HostedServices;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreferencesServicesController(
        ILogger<PreferencesServicesController> logger,
        BuyerPreferencesService preferencesService
        ) : ControllerBase
    {
        private readonly ILogger<PreferencesServicesController> _logger = logger;
        private readonly BuyerPreferencesService _preferencesService = preferencesService;

        [HttpGet("force/buyerspreferences")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ForceSetBuyersPreferences()
        {
            await Task.Yield();
            _logger.LogInformation("{service} is forced.", nameof(BuyerPreferencesService));

            _preferencesService.IsForced = true;

            return NoContent();
        }
    }
}
