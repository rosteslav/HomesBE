using Demo.Application.Features.Properties.Commands.AddProperty;
using Demo.Application.Features.Properties.Commands.DeleteProperty;
using Demo.Application.Features.Properties.Commands.EditProperty;
using Demo.Application.Features.Properties.Queries.GetAllProperties;
using Demo.Domain.Enities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace BuildingMarket.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertiesController(IMediator mediator, ILogger<ItemsController> logger, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ItemsController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Add([FromBody] AddPropertyCommand addPropertyCommand)
        {
            await _mediator.Send(addPropertyCommand);
            _logger.LogInformation($"New Property: {addPropertyCommand.Type}");
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromQuery] EditPropertyCommand property)
        {
            var command = _mapper.Map<Property>(property);
            await _mediator.Send(command);
            _logger.LogInformation($"Edit Property with id: {command.Id}");
            return NoContent();

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var command = new DeletePropertyCommand { Id = id };
            await _mediator.Send(command);
            _logger.LogInformation($"Delete Property with id: {id}");
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Property>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllPropertiesQuery());
            _logger.LogInformation($"Properties are: {string.Join(", ", result.Select(x => x.Type).ToArray())}");
            return Ok(result);
        }
    }
}
