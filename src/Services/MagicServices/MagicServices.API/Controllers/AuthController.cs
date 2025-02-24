using Magic.Application.Common.Identity.User.Commads.Authenticate;
using Magic.Application.Common.Identity.User.Commads.Create;
using Magic.Application.Common.Identity.User.Commads.Logout;
using Magic.Application.Common.Identity.User.Commads.RefreshToken;
using Magic.Application.Dtos.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        public AuthController(IHostEnvironment environment) : base(environment)
        {
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdentityResponseDto>> Register(
           [FromBody] CreateUserCommand model, CancellationToken cancellationToken = default)
            => Ok(await Mediator.Send(new CreateUserCommand(model.Request), cancellationToken));

        [HttpPost("login")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdentityResponseDto>> Login(
           [FromBody] AuthenticateCommand model, CancellationToken cancellationToken = default)
            => Ok(await Mediator.Send(new AuthenticateCommand(model.Request), cancellationToken));
        
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdentityResponseDto>> RefreshToken(
           [FromBody] RefreshTokenCommand model, CancellationToken cancellationToken = default)
            => Ok(await Mediator.Send(new RefreshTokenCommand(model.Request), cancellationToken));

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand model, 
            CancellationToken cancellationToken = default)
        { 
            await Mediator.Send(new LogoutCommand(model.Request), cancellationToken);
            return NoContent();
        }
    }
}
