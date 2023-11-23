using BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties;
using BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetById;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetBySeller;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;
using BuildingMarket.Common.Models.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;
using MediatR;

namespace BuildingMarket.Properties.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoles.Seller + "," + UserRoles.Broker + "," + UserRoles.Admin)]
    [Route("[controller]")]
    public class PropertiesController(IMediator mediator, ILogger<PropertiesController> logger, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<PropertiesController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
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
        [ProducesResponseType(typeof(IEnumerable<PropertyModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            var properties = default(IEnumerable<Property>);
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

            var result = _mapper.Map<IEnumerable<PropertyModel>>(properties);
            return Ok(properties);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PropertyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation($"Attempt to get property with ID {id}");
            var property = await _mediator.Send(new GetByIdQuery { Id = id });

            return property is not null ? Ok(property) : NotFound();
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<GetAllPropertiesOutputModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Attempt to get all properties");
            var properties = await _mediator.Send(new GetAllPropertiesQuery());
            var result = _mapper.Map<IEnumerable<GetAllPropertiesOutputModel>>(properties);
            return Ok(result);
        }
    }
}