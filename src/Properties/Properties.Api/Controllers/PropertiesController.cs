using BuildingMarket.Common.Models.Security;
using BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty;
using BuildingMarket.Properties.Application.Features.Properties.Commands.DeleteProperty;
using BuildingMarket.Properties.Application.Features.Properties.Commands.EditProperty;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetById;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetBySeller;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetRecommended;
using BuildingMarket.Properties.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuildingMarket.Properties.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertiesController(IMediator mediator, ILogger<PropertiesController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<PropertiesController> _logger = logger;

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker}")]
        [ProducesResponseType(typeof(AddPropertyOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] AddPropertyInputModel model)
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            _logger.LogInformation($"Attempt to add a new property from the user with ID {userId}");

            if (User.IsInRole(UserRoles.Broker))
            {
                model.BrokerId = userId;
            }

            return Ok(await _mediator.Send(new AddPropertyCommand
            {
                SellerId = userId,
                Model = model
            }));
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker}")]
        [ProducesResponseType(typeof(IEnumerable<PropertyModelWithId>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            var properties = default(IEnumerable<PropertyModelWithId>);
            var userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            _logger.LogInformation($"Attempt to get all properties for the user with ID {userId}");
            if (User.IsInRole(UserRoles.Seller))
            {
                properties = await _mediator.Send(new GetBySellerQuery
                {
                    SellerId = userId
                });
            }
            else if (User.IsInRole(UserRoles.Broker))
            {
                properties = await _mediator.Send(new GetByBrokerQuery
                {
                    BrokerId = userId
                });
            }

            return Ok(properties);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(PropertyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation($"Attempt to get property with ID {id}");

            var result = await _mediator.Send(new GetByIdQuery { Id = id });

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<GetAllPropertiesOutputModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPropertiesQuery query)
        {
            _logger.LogInformation("Attempt to get all properties");
            var properties = await _mediator.Send(query);
            return Ok(properties);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Attempting to get delete property with id: {id}", id);

            var userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;

            var result = await _mediator.Send(new DeletePropertyCommand
            {
                PropertyId = id,
                UserId = userId
            });

            switch (result)
            {
                case DeletePropertyResult.Success:
                    _logger.LogInformation("Property with id: {id} was deleted successfully!", id);

                    return NoContent();
                case DeletePropertyResult.NotFound:
                    _logger.LogInformation("Property with id: {id} was not found!", id);

                    return NotFound();
                case DeletePropertyResult.Unauthorized:
                    _logger.LogInformation("User with id {userId} tried to delete property {id} with no access to it!", userId, id);

                    return Unauthorized();
                default:
                    _logger.LogInformation("Deleting property with id {id} failed.", id);

                    return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = $"{UserRoles.Seller},{UserRoles.Broker}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(
            [FromRoute] int id,
            [FromBody] AddPropertyInputModel model)
        {
            _logger.LogInformation("Attempting to edit property with id: {id}", id);

            var userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;

            var result = await _mediator.Send(new EditPropertyCommand
            {
                PropertyId = id,
                EditedProperty = model,
                UserId = userId
            });

            switch (result)
            {
                case DeletePropertyResult.Success:
                    _logger.LogInformation("Property with id: {id} was edited successfully!", id);

                    return NoContent();
                case DeletePropertyResult.NotFound:
                    _logger.LogInformation("Property with id: {id} was not found!", id);

                    return NotFound();
                case DeletePropertyResult.Unauthorized:
                    _logger.LogInformation("User with id {userId} tried to edit property {id} with no access to it!", userId, id);

                    return Unauthorized();
                default:
                    _logger.LogInformation("Editing property with id {id} failed.", id);

                    return BadRequest();
            }
        }

        [HttpGet]
        [Route("recommended")]
        [Authorize(Roles = $"{UserRoles.Buyer}")]
        [ProducesResponseType(typeof(IEnumerable<GetAllPropertiesOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Recommended()
        {
            _logger.LogInformation($"Getting top 6 recommended properties.");

            var properties = await _mediator.Send(new GetRecommendedQuery());

            return Ok(properties);
        }
    }
}