using AutoMapper;
using BuildingMarket.Common.Models.Security;
using BuildingMarket.Properties.Application.Features.Properties.Commands.AddMultipleProperties.Commands;
using BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetAllProperties;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker;
using BuildingMarket.Properties.Application.Features.Properties.Queries.GetBySeller;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Add([FromBody] PropertyModel model)
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            await _mediator.Send(new AddPropertyCommand
            {
                SellerId = userId,
                Model = model
            });

            return NoContent();
        }

        [HttpPost]
        [Route("/Admins/Properties")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddMultiple([FromBody] IEnumerable<PropertyModel> properties)
        {
            var command = new AddMultiplePropertiesCommand { Properties = properties };
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var properties = default(IEnumerable<Property>);
            var userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
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
        [Route("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<PropertyModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _mediator.Send(new GetAllPropertiesQuery());
            var result = _mapper.Map<IEnumerable<PropertyModel>>(properties);
            return Ok(properties);
        }
    }
}