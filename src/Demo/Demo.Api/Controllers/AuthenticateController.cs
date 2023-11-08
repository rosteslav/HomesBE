using Demo.Application.Features.Security.Commands.Register;
using Demo.Application.Features.Security.Queries.Login;
using Demo.Application.Models.Security;
using Demo.Application.Models.Security.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Demo.Api.Controllers
{
    [ApiController]
    public class AuthenticateController(
        IMediator mediator,
        ILogger<AuthenticateController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<AuthenticateController> _logger = logger;

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(TokenObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _mediator.Send(new LoginQuery { Model = model });
            _logger.LogInformation($"Login attempt with Username: {model.Username}");
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new TokenObject
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            _logger.LogInformation($"Register attempt with Username: {model.Username}");
            var token = await _mediator.Send(new RegisterCommand
            {
                Model = model,
                Roles = new string[]
                {
                    UserRoles.User,
                    model.Role
                }
            });

            switch (token)
            {
                case RegistrationResult.Success:
                    _logger.LogInformation($"User created successfully with Username: {model.Username}");
                    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
                case RegistrationResult.AlreadyExists:
                    _logger.LogInformation($"User already exists with Username: {model.Username}");
                    return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = "User already exists!" });
                case RegistrationResult.Failure:
                default:
                    _logger.LogInformation($"User creation failed with Username: {model.Username}");
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
        }

        [HttpPost]
        [Route("admin/register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            _logger.LogInformation($"Admin register attempt with Username: {model.Username}");
            var token = await _mediator.Send(new RegisterCommand
            {
                Model = model,
                Roles = new string[]
                {
                    UserRoles.User,
                    UserRoles.Admin
                }
            });

            switch (token)
            {
                case RegistrationResult.Success:
                    _logger.LogInformation($"Admin created successfully with Username: {model.Username}");
                    return Ok(new Response { Status = "Success", Message = "Admin created successfully!" });
                case RegistrationResult.AlreadyExists:
                    _logger.LogInformation($"User already exists with Username: {model.Username}");
                    return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = "User already exists!" });
                case RegistrationResult.Failure:
                default:
                    _logger.LogInformation($"Admin creation failed with Username: {model.Username}");
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Admin creation failed! Please check user details and try again." });
            }
        }
    }
}
