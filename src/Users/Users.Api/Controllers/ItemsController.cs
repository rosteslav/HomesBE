using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Auth;

namespace Users.Api.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class ItemsController(ILogger<ItemsController> logger) : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger = logger;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            await Task.Yield();
            _logger.LogInformation("Get hit");
            return Ok(Enumerable.Range(1, 5).ToArray());
        }
    }
}