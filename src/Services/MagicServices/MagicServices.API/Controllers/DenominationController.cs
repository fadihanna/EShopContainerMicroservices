using Magic.Application.Denominations.Commands;
using Magic.Application.Denominations.Queries.Denominations;
using Magic.Application.Providers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MagicServices.API.Controllers
{
    public class DenominationController : ApiControllerBase
    {
        public DenominationController(IHostEnvironment environment) : base(environment)
        {
        }

        [HttpPost("get-denomination")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetDenominationsResponse>> GetDenominationById(
           [FromBody] GetDenominationByIdQuery model, CancellationToken cancellationToken = default)
            => Ok(await Mediator.Send(new GetDenominationByIdQuery(model.Id), cancellationToken));

        [HttpGet("get-denomination-list")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetDenominationsResponse>> GetDenominationList(CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new GetDenominationsQuery(), cancellationToken));

        [HttpPost("insert-denomination")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> InsertDenomination([FromBody] InsertDenominationCommand model, CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new InsertDenominationCommand(model.Denomination), cancellationToken));

        [HttpPut("update-denomination/{Id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> UpdateDenomination([FromBody] UpdateDenominationCommand model, [FromRoute] int Id, CancellationToken cancellationToken = default) =>
                   Ok(await Mediator.Send(new UpdateDenominationCommand(model.Denomination, Id), cancellationToken));

        [HttpDelete("delete-denomination/{Id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> DeleteDenomination([FromRoute] int Id, CancellationToken cancellationToken = default) =>
                    Ok(await Mediator.Send(new DeleteDenominationCommand(Id), cancellationToken));
    }
}
