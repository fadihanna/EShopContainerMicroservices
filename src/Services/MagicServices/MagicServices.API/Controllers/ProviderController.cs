using Magic.Application.Dtos;
using Magic.Application.Providers.Commands;
using Magic.Application.Providers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class ProviderController : ApiControllerBase
    {
        public ProviderController(IHostEnvironment environment) : base(environment)
        {
        }

        [HttpGet("get-provider-list")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetProvidersResponse>> GetProviderList(CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new GetProvidersQuery(), cancellationToken));

        [HttpPost("insert-provider")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> InsertProvider(
            [FromBody] InsertProviderCommand command, CancellationToken cancellationToken = default)
        {
            var response = await Mediator.Send(command, cancellationToken);
            return Ok(response.Id);
        }

        [HttpPut("update-provider/{Id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> UpdateProvider(
    [FromBody] ProviderDto providerDto, [FromRoute] int Id, CancellationToken cancellationToken = default)
        {
            var command = new UpdateProviderCommand(providerDto, Id);
            var response = await Mediator.Send(command, cancellationToken);
            return Ok(response.Id);
        }

        [HttpDelete("delete-provider/{Id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> DeleteProvider([FromRoute] int Id, CancellationToken cancellationToken = default)
        {
            await Mediator.Send(new DeleteProviderCommand(Id), cancellationToken);
            return Ok(Id);
        }
    }
}
