using BuildingMarket.Auth.Application.Features.AuthOptions.Queries.GetAllBrokers;
using BuildingMarket.Auth.Application.Features.AuthOptions.Queries.GetUserRoles;
using BuildingMarket.Auth.Application.Models.AuthOptions;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Common.Models.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuildingMarket.Auth.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthOptionsController(
        IMediator mediator,
        ILogger<AuthOptionsController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<AuthOptionsController> _logger = logger;

        [HttpGet]
        [Route("/roles")]
        [ProducesResponseType(typeof(UserRolesModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserRoles()
        {
            _logger.LogInformation($"Attempt to retrieve user roles");
            return Ok(await _mediator.Send(new GetUserRolesQuery()));
        }

        [HttpGet]
        [Route("brokers")]
        [Authorize(Roles = UserRoles.Seller)]
        [ProducesResponseType(typeof(IEnumerable<BrokerModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllBrokers()
        {
            var sellerId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            _logger.LogInformation($"Attempt to retrieve all brokers from the seller with ID {sellerId}");
            var result = await _mediator.Send(new GetAllBrokersQuery());
            return Ok(result);
        }
    }
}
