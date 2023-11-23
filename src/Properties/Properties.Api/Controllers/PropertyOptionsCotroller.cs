using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllBuilidngTypes;
using BuildingMarket.Properties.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BuildingMarket.Properties.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class PropertyOptionsCotroller(IMediator mediator, ILogger<PropertiesController> logger) : ControllerBase
    {
        private PropertyOptionsModel propertyOptionsModel;
        private readonly ILogger<PropertiesController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyModel>), StatusCodes.Status200OK)]

        public async Task<ActionResult> GetPropertyOptions()
        {
            try
            {
                await _mediator.Send(new GetAllPropertyOptionsQuery());

                _logger.LogInformation("Retreive property options from Db.");
            }
            catch (Exception e)
            {

                _logger.LogError(e, "Error occured while extracting property options.");
            }
            

            return Ok(propertyOptionsModel);
        }
    }
}
