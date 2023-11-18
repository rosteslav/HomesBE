using BuildingMarket.Common.Models.Security;
using BuildingMarket.Images.Application.Extensions;
using BuildingMarket.Images.Application.Features.Image.Commands.Add;
using BuildingMarket.Images.Application.Features.Image.Queries.GetAll;
using BuildingMarket.Images.Application.Features.Image.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Images.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker},{UserRoles.Admin}")]
    public class ImageController(
        IMediator mediator,
        ILogger<ImageController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ImageController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImage(int propertyId, IFormFile image)
        {
            var imgSize = image.Length / 1024 / 1024;

            if (imgSize > 32)
            {
                return BadRequest("File size should be up to 32MB!");
            }

            _logger.LogInformation("Attempting to add new image.");

            var imageUrl = await _mediator.Send(new AddCommand
            {
                Image = new()
                {
                    FileName = image.FileName,
                    FormFile = image,
                    FileExtension = Path.GetExtension(image.FileName)
                },
                PropertyId = propertyId
            });

            if (string.IsNullOrEmpty(imageUrl))
            {
                _logger.LogError("Image upload was not successful.");
                return BadRequest("Image upload was unsuccessful!");
            }

            return Ok(imageUrl);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllImages(int propertyId)
        {
            _logger.LogInformation("Getting all images for property with Id {propertyId}", propertyId);
            var imageUrls = await _mediator.Send(new GetAllCommand
            {
                PropertyId = propertyId
            });

            return Ok(imageUrls);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteImage(string deleteURL)
        {
            _logger.LogInformation("Deleting image with deleteURL {deleteURL}", deleteURL);

            await _mediator.Send(new DeleteCommand
            {
                DeleteURL = deleteURL
            });

            return NoContent();
        }
    }
}
