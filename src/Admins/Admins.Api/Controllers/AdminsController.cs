using AutoMapper;
using BuildingMarket.Admins.Application.Attributes;
using BuildingMarket.Admins.Application.Features.Admins.Commands.AddMultipleProperties;
using BuildingMarket.Admins.Application.Features.Admins.Queries.GetAllBrokers;
using BuildingMarket.Admins.Application.Models;
using BuildingMarket.Common.Models.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

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
            var brokers = await _mediator.Send(new GetAllBrokersQuery());
            var result = _mapper.Map<IEnumerable<BrokerModel>>(brokers);
            return Ok(result);
        }

        [HttpPost]
        [Route("properties")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddMultipleProperties([Required][ValidCsvFile] IFormFile csvFile)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues jwt);
            var response = await _mediator.Send(new AddMultiplePropertiesCommand
            {
                JWT = jwt,
                File = csvFile
            });

            return Ok(response);
        }
    }
}