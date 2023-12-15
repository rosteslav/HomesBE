using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllBuilidngTypes;
using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetPropertyOptionsWithFilter;
using BuildingMarket.Properties.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Properties.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertyOptionsController(IMediator mediator, ILogger<PropertiesController> logger) : ControllerBase
    {
        private readonly ILogger<PropertiesController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyOptionsModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPropertyOptions()
        {
            _logger.LogInformation("Attempt to get all property options from Db.");

            var propertyOptionsModel = await _mediator.Send(new GetAllPropertyOptionsQuery());

            return Ok(propertyOptionsModel);
        }

        [HttpGet]
        [Route("filter")]
        [ProducesResponseType(typeof(IEnumerable<PropertyOptionsWithFilterModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertyOptionsWithFilter()
        {
            _logger.LogInformation("Attempt to get all property options with filter from Db.");

            var propertyOptionsModel = await _mediator.Send(new GetPropertyOptionsWithFilterQuery());

            return Ok(propertyOptionsModel);
        }
    }
}
