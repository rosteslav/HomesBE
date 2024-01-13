using BuildingMarket.Admins.Application.Features.Reports.Commands.DeletePropertyReports;
using BuildingMarket.Admins.Application.Features.Reports.Queries.GetAllReports;
using BuildingMarket.Admins.Application.Models;
using BuildingMarket.Common.Models.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Admins.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    [Route("[controller]")]
    public class ReportsController(
        IMediator mediator,
        ILogger<AdminsController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<AdminsController> _logger = logger;

        [HttpGet]
        [ProducesResponseType(typeof(List<AllReportsModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllReports()
        {
            _logger.LogInformation("Retrieving all the reports.");
            var reports = await _mediator.Send(new GetAllReportsQuery());
            return Ok(reports);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletePropertyReports(int id)
        {
            _logger.LogInformation("Attempting to delete reports of property with id: {id}.", id);
            await _mediator.Send(new DeletePropertyReportsCommand
            {
                PropertyId = id
            });
            return NoContent();
        }
    }
}
