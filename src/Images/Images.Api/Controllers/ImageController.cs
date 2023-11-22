using AutoMapper;
using BuildingMarket.Common.Models.Security;
using BuildingMarket.Images.Application.Features.Image.Commands.Add;
using BuildingMarket.Images.Application.Features.Image.Commands.Delete;
using BuildingMarket.Images.Application.Features.Image.Queries.GetAll;
using BuildingMarket.Images.Application.Models;
using BuildingMarket.Images.Application.Models.Enums;
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
            var imgInMB = image.Length / 1024 / 1024;

            if (imgInMB > 32)
            {
                return BadRequest();
            }

            _logger.LogInformation("Attempting to add new image.");

            var userId = User.Claims
                .First(x => x.Type == ClaimTypes.Sid).Value;

            var imageUrl = await _mediator.Send(new AddImageCommand
            {
                FormFile = image,
                PropertyId = propertyId,
                UserId = userId
            });

            if (string.IsNullOrEmpty(imageUrl))
            {
                _logger.LogError("Image upload was not successful.");
                return BadRequest();
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

            _logger.LogInformation("User with id: {userId} attempts to delete image with id: {id}", userId, id);

            var result = await _mediator.Send(new DeleteImageCommand
            {
                ImageId = id,
                UserId = userId
            });

            switch (result)
            {
                case DeleteImageResult.Success:
                    _logger.LogInformation("Image with Id: {id} deleted successfully!", id);
                    return Ok();
                case DeleteImageResult.ImageNotFound:
                    _logger.LogInformation("Image with Id: {id} was not found!", id);
                    return NotFound();
                case DeleteImageResult.PropertyNotFound:
                    _logger.LogInformation("Property of image with Id: {id} was not found!", id);
                    return NotFound();
                case DeleteImageResult.UserHasNoAccess:
                default:
                    _logger.LogInformation("User with Id: {userId} who has no access tried to delete image with Id: {id}", userId, id);
                    return BadRequest();
            }
        }
    }
}
