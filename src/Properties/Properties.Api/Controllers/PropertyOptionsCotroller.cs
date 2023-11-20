using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllBuilidngTypes;
using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllNeighbourhoods;
using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllHeatingTypes;
using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllGarageTypes;
using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllFurnishmentTypes;
using BuildingMarket.Properties.Application.Features.PropertyOptions.Queries.GetAllFinishTypes;
using BuildingMarket.Properties.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;

namespace BuildingMarket.Properties.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class PropertyOptionsCotroller(IMediator mediator, ILogger<PropertiesController> logger, IMapper mapper) : ControllerBase
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
                propertyOptionsModel = new PropertyOptionsModel()
                {
                    BuildingType = await _mediator.Send(new GetAllBuildingTypesQuery()),
                    Finish = await _mediator.Send(new GetAllFinishTypesQuery()),
                    Furnishment = await _mediator.Send(new GetAllFurnishmentTypesQuery()),
                    Garage = await _mediator.Send(new GetAllGarageTypesQuery()),
                    Heating = await _mediator.Send(new GetAllHeatingTypesQuery()),
                    Neighbourhood = await _mediator.Send(new GetAllNeighbourhoodsQuery())
                };

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
