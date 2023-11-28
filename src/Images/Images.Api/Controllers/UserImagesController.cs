using BuildingMarket.Images.Application.Features.UserImages.Commands.Add;
using BuildingMarket.Common.Models.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BuildingMarket.Images.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker}")]
    public class UserImagesController(
        IMediator mediator,
        ILogger<UserImagesController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<UserImagesController> _logger = logger;

        [HttpPost]
        [Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromRoute] string userId, [FromBody] IFormFile image)
        {
            _logger.LogInformation("Attempting to add new user image.");
            
            var imgInMB = image.Length / 1024 / 1024;

            if (imgInMB > 5)
            {
                return BadRequest();
            }

            string imageUrl = await _mediator.Send(new AddAdditionalUserDataCommand
            {
                FormFile = image,
                UserId = userId
            });

            if (string.IsNullOrEmpty(imageUrl) || userId == default)
            {
                _logger.LogError("Image upload was not successful.");
                return BadRequest();
            }

            return Ok(new string(imageUrl));
        }

        [HttpDelete]
        [Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] string userId)
        {
            _logger.LogInformation("Attempting to delete user image.");

            string data = await _mediator.Send(new DeleteAdditionalUserDataCommand
            {
                UserId = userId
            });

            if (string.IsNullOrEmpty(data) || userId == default)
            {
                _logger.LogError("Image deletion was not successful.");
                return BadRequest();
            }

            return Ok(new string(userId));
        }
    }
}
