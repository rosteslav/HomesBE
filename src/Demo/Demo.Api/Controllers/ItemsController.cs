using Demo.Application.Features.Items.Commands.AddItem;
using Demo.Application.Features.Items.Commands.DeleteItem;
using Demo.Application.Features.Items.Queries.GetAllItems;
using Demo.Application.Models.Security;
using Demo.Domain.Enities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController(IMediator mediator, ILogger<ItemsController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ItemsController> _logger = logger;

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Item>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllItemsQuery());
            _logger.LogInformation($"Items are: {string.Join(", ", result.Select(x => x.Name).ToArray())}");
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Add([FromBody] AddItemCommand addItemCommand)
        {
            await _mediator.Send(addItemCommand);
            _logger.LogInformation($"New Item: {addItemCommand.Name}");
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var command = new DeleteItemCommand { Id = id };
            await _mediator.Send(command);
            _logger.LogInformation($"Delete Item with id: {id}");
            return NoContent();
        }
    }
}