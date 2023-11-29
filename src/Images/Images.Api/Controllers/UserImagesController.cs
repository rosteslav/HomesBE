using BuildingMarket.Common.Models.Security;
using BuildingMarket.Images.Application.Features.Images.Commands.AddUserImage;
using BuildingMarket.Images.Application.Features.Images.Commands.DeleteUserImage;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddImage(string userId, IFormFile image)
        {
            var imgInMB = image.Length / 1024 / 1024;

            if (imgInMB > 5)
            {
                return BadRequest();
            }

            _logger.LogInformation("Attempting to add image to user with id: {userId}.", userId);

            var imageId = await _mediator.Send(new AddUserImageCommand
            {
                FormFile = image,
                UserId = userId
            });

            if (string.IsNullOrEmpty(imageId))
            {
                _logger.LogError("User image upload was not successful.");
                return BadRequest();
            }

            return Ok(imageId);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

            return Ok();
        }
    }
}
