using AutoMapper;
using BuildingMarket.Admins.Application.Attributes;
using BuildingMarket.Admins.Application.Features.Admins.Commands.AddMultipleProperties;
using BuildingMarket.Admins.Application.Features.Admins.Queries.GetAllBrokers;
using BuildingMarket.Admins.Application.Models;
using BuildingMarket.Common.Models.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BuildingMarket.Admins.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    [Route("[controller]")]
    public class AdminsController(IMediator mediator, ILogger<AdminsController> logger, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<AdminsController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [Route("brokers")]
        [ProducesResponseType(typeof(IEnumerable<BrokerModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllBrokers()
        {
            var adminId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            _logger.LogInformation($"Attempt to retrieve all brokers from the admin with ID {adminId}");
            var brokers = await _mediator.Send(new GetAllBrokersQuery());
            var result = _mapper.Map<IEnumerable<BrokerModel>>(brokers);
            return Ok(result);
        }

        [HttpPost]
        [Route("properties")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMultipleProperties([Required][ValidCsvFile] IFormFile csvFile)
        {
            var adminId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            _logger.LogInformation($"Attempt to insert multiple properties from the admin with ID {adminId}");
            await _mediator.Send(new AddMultiplePropertiesCommand { File = csvFile });
            return NoContent();
        }
    }
}