using BuildingMarket.Images.Application.Extensions;
using BuildingMarket.Images.Application.Features.Image.Commands.Add;
using BuildingMarket.Images.Application.Features.Image.Queries.GetAll;
using Images.Application.Features.Image.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Images.Api.Controllers
{
    [ApiController]
    public class ImagesController(
        IMediator mediator,
        ILogger<ImagesController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ImagesController> _logger = logger;

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImage(int propertyId, IFormFile image)
        {
            if (!User.IsInRole("Broker") || !User.IsInRole("Seller"))
            {
                return Unauthorized();
            }

            var imgSize = image.Length / 1024 / 1000;

            if (imgSize > 32)
            {
                return BadRequest("File size should be up to 32MB!");
            }

            var memoryStream = await FormFileExtensions.ToMemoryStream(image);

            var imageUrl = await _mediator.Send(new AddCommand
            {
                Image = new()
                {
                    FileName = image.FileName,
                    MemoryStream = memoryStream
                },
                PropertyId = propertyId
            });

            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest("Image upload was unsuccessful!");
            }

            return Ok(imageUrl);
        }

        [HttpGet]
        [Route("getall")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllImages(int propertyId)
        {
            if (!User.IsInRole("Broker") || !User.IsInRole("Seller"))
            {
                return Unauthorized();
            }

            var imageUrls = await _mediator.Send(new GetAllCommand
            {
                PropertyId = propertyId
            });

            return Ok(imageUrls);
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteImage(string deleteURL)
        {
            if (!User.IsInRole("Broker") || !User.IsInRole("Seller"))
            {
                return Unauthorized();
            }

            await _mediator.Send(new DeleteCommand
            {
                DeleteURL = deleteURL
            });

            return NoContent();
        }
    }
}
