using BuildingMarket.Common.Models.Security;
using BuildingMarket.Images.Application.Extensions;
using BuildingMarket.Images.Application.Features.Image.Commands.Add;
using BuildingMarket.Images.Application.Features.Image.Queries.GetAll;
using Images.Application.Features.Image.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Images.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker},{UserRoles.Admin}")]
    public class ImagesController(
        IMediator mediator,
        ILogger<ImagesController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ImagesController> _logger = logger;

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker},{UserRoles.Admin}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImage(int propertyId, IFormFile image)
        {
            var imgSize = image.Length / 1024 / 1024;

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
        [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker},{UserRoles.Admin}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllImages(int propertyId)
        {
            var imageUrls = await _mediator.Send(new GetAllCommand
            {
                PropertyId = propertyId
            });

            return Ok(imageUrls);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker},{UserRoles.Admin}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteImage(string deleteURL)
        {
            await _mediator.Send(new DeleteCommand
            {
                DeleteURL = deleteURL
            });

            return NoContent();
        }
    }
}
