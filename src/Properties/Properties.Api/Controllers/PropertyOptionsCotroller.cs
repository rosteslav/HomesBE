using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllBuilidngTypes;
using BuildingMarket.Properties.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BuildingMarket.Properties.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertyOptionsCotroller(IMediator mediator, ILogger<PropertiesController> logger) : ControllerBase
    {
        private readonly ILogger<PropertiesController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyModel>), StatusCodes.Status200OK)]

        public async Task<ActionResult> GetPropertyOptions()
        {
            _logger.LogInformation("Attempt to get all property options from Db.");

            var propertyOptionsModel = await _mediator.Send(new GetAllPropertyOptionsQuery());

            return Ok(propertyOptionsModel);
        }
    }
}
