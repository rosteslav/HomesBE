using BuildingMarket.Images.Api.HostedServices;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Images.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageServicesController(
        ILogger<ImageServicesController> logger,
        ImageUploaderService imageUploaderService)
        : ControllerBase
    {
        private readonly ILogger<ImageServicesController> _logger = logger;
        private readonly ImageUploaderService _imageUploaderService = imageUploaderService;

        [HttpGet]
        [Route("ImagesPopulate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ForceImageUploaderService()
        {
            await Task.Yield();

            _logger.LogInformation($"{nameof(ImageUploaderService)} is forced");

            _imageUploaderService.IsForced = true;

            return NoContent();
        }
    }
}
