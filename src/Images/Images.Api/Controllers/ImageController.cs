using AutoMapper;
using BuildingMarket.Common.Models.Security;
using BuildingMarket.Images.Application.Features.Image.Commands.Add;
using BuildingMarket.Images.Application.Features.Image.Commands.Delete;
using BuildingMarket.Images.Application.Features.Image.Queries.ExistsById;
using BuildingMarket.Images.Application.Features.Image.Queries.GetAll;
using BuildingMarket.Images.Application.Features.Image.Queries.IsPropertyOwnerOfImage;
using BuildingMarket.Images.Application.Features.Property.Queries.IsPropertyOwner;
using BuildingMarket.Images.Application.Features.Property.Queries.PropertyExists;
using BuildingMarket.Images.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuildingMarket.Images.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker},{UserRoles.Admin}")]
    public class ImageController(
        IMediator mediator,
        ILogger<ImageController> logger,
        IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ImageController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImage([FromQuery] int propertyId, IFormFile image)
        {
            var imgSize = image.Length / 1024 / 1024;

            if (imgSize > 32)
            {
                return BadRequest("File size should be up to 32MB!");
            }

            var userId = User.Claims
                .First(x => x.Type == ClaimTypes.Sid).Value;

            if (await PropertyExistsAndIsOwner(propertyId, userId))
            {
                _logger.LogError("Invalid request for property with Id: {propertyId}!", propertyId);

                return BadRequest($"Invalid request for property with Id: {propertyId}!");
            }

            _logger.LogInformation("Attempting to add new image.");

            var imageUrl = await _mediator.Send(new AddImageCommand
            {
                FormFile = image,
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
        [Route("{propertyId}")]
        public async Task<IActionResult> GetAllImages([FromRoute] int propertyId)
        {
            _logger.LogInformation("Getting all images for property with Id {propertyId}", propertyId);

            var images = await _mediator.Send(new GetAllImagesCommand
            {
                PropertyId = propertyId
            });

            var result = _mapper.Map<IEnumerable<ImagesResult>>(images);

            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("{id}")]
        public async Task<IActionResult> DeleteImage([FromRoute] int id)
        {
            var userId = User.Claims
               .First(x => x.Type == ClaimTypes.Sid).Value;

            if (await ImageExistsAndIsOwnerOfProperty(id, userId))
            {
                _logger.LogError("Invalid request for image with Id: {id}!", id);

                return BadRequest($"Invalid request for image with Id: {id}!");
            }

            _logger.LogInformation("Deleting image with id: {id}", id);

            await _mediator.Send(new DeleteImageCommand
            {
                Id = id,
            });

            return NoContent();
        }

        private async Task<bool> PropertyExistsAndIsOwner(int propertyId, string userId)
        {
            bool propertyExists = await _mediator.Send(new PropertyExistsQuery
            {
                PropertyId = propertyId
            });

            bool isPropertyOwner = await _mediator.Send(new IsPropertyOwnerQuery
            {
                PropertyId = propertyId,
                UserId = userId
            });

            return !propertyExists || !isPropertyOwner;
        }

        private async Task<bool> ImageExistsAndIsOwnerOfProperty(
            int imageId,
            string userId)
        {
            bool imageExists = await _mediator.Send(new ImageExistsByIdQuery
            {
                ImageId = imageId
            });

            bool isPropertyOwner = await _mediator.Send(new IsPropertyOwnerOfImageQuery
            {
                ImageId = imageId,
                UserId = userId
            });

            return !imageExists || !isPropertyOwner;
        }
    }
}
