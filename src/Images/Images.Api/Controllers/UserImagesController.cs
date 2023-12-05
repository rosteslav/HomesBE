using BuildingMarket.Common.Models.Security;
using BuildingMarket.Images.Application.Attributes;
using BuildingMarket.Images.Application.Features.Images.Commands.AddUserImage;
using BuildingMarket.Images.Application.Features.Images.Commands.DeleteUserImage;
using BuildingMarket.Images.Application.Features.Images.Commands.EditUserImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuildingMarket.Images.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker}")]
    public class UserImagesController(
        IMediator mediator,
        ILogger<ImageController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ImageController> _logger = logger;

        [HttpPost]
        [Route("{userId}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddImage([FromRoute] string userId, [ValidImage] IFormFile image)
        {
            _logger.LogInformation("Attempting to add image to user with id: {userId}.", userId);

            var imageUrl = await _mediator.Send(new AddUserImageCommand
            {
                UserId = userId,
                FormFile = image
            });

            if (string.IsNullOrEmpty(imageUrl))
            {
                _logger.LogError("User image upload was not successful.");
                return BadRequest();
            }

            return Ok(imageUrl);
        }

        [HttpPut]
        [Route("{userId}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> EditImage([FromRoute] string userId, [ValidImage] IFormFile image)
        {
            _logger.LogInformation("Attempting to edit image to user with id: {userId}.", userId);

            var imageUrl = await _mediator.Send(new EditUserImageCommand
            {
                FormFile = image,
                UserId = userId
            });

            if (string.IsNullOrEmpty(imageUrl))
            {
                _logger.LogError("User image upload was not successful.");
                return BadRequest();
            }

            return Ok(imageUrl);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteImage()
        {
            var userId = User.Claims
                .First(x => x.Type == ClaimTypes.Sid).Value;

            await _mediator.Send(new DeleteUserImageCommand
            {
                UserId = userId
            });

            return NoContent();
        }
    }
}
