using BuildingMarket.Admins.Application.Features.Reports.Queries;
using BuildingMarket.Common.Models.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMarket.Admins.Api.Controllers
{
    [ApiController]
    //[Authorize(Roles = UserRoles.Admin)]
    [Route("[controller]")]
    public class ReportsController(
        IMediator mediator,
        ILogger<AdminsController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<AdminsController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            _logger.LogInformation("Retrieving all the reports.");
            //var reports = await _mediator.Send(new GetAllReportsQuery());
            //return Ok(reports);
            await _mediator.Send(new GetAllReportsQuery());
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePropertyReports(int id)
        {
            return NoContent();
        }
    }
}
