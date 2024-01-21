using BuildingMarket.Properties.Api.HostedServices;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Properties.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PropertiesServicesController(
        PropertiesUploaderService propertiesService,
        RecommendationUploaderService recommendationService,
        ILogger<PropertiesServicesController> logger)
        : ControllerBase
    {
        private readonly PropertiesUploaderService _propertiesService = propertiesService;
        private readonly RecommendationUploaderService _recommendationService = recommendationService;
        private readonly ILogger<PropertiesServicesController> _logger = logger;

        [HttpGet]
        [Route("PropertiesPopulate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ForcePropertiesUploaderService()
        {
            await Task.Yield();

            _logger.LogInformation($"{nameof(PropertiesUploaderService)} is forced");

            _propertiesService.IsForced = true;

            return NoContent();
        }

        [HttpGet]
        [Route("RecommendationsPopulate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ForceRecommendationUploaderService()
        {
            await Task.Yield();

            _logger.LogInformation($"{nameof(RecommendationUploaderService)} is forced");

            _recommendationService.IsForced = true;

            return NoContent();
        }
    }
}
