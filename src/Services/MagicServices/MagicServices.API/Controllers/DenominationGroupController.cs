using Magic.Application.DenominationGroups.Commands;
using Magic.Application.DenominationGroups.Queries;
using Magic.Application.Denominations.Commands;
using Magic.Application.Denominations.Queries.Denominations;
using Magic.Application.Providers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class DenominationGroupController : ApiControllerBase
    {
        public DenominationGroupController(IHostEnvironment environment) : base(environment)
        {
        }

        [HttpGet("get-denominationGroup-list")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetDenominationGroupsResponse>> GetDenominationGroupList(CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new GetDenominationGroupsQuery(), cancellationToken));


        [HttpPost("insert-denomination-group")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> InsertDenominationGroup([FromBody] InsertDenominationGroupCommand model, CancellationToken cancellationToken = default) =>
     Ok(await Mediator.Send(model, cancellationToken));

        //[HttpPut("update-denomination-group/{id}")]
        //[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        //public async Task<ActionResult<int>> UpdateDenominationGroup([FromBody] UpdateDenominationGroupCommand model, [FromRoute] int id, CancellationToken cancellationToken = default) =>
        //    Ok(await Mediator.Send(new UpdateDenominationGroupCommand(model.DenominationGroup, id), cancellationToken));

        [HttpDelete("delete-denomination-group/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> DeleteDenominationGroup([FromRoute] int id, CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new DeleteDenominationGroupCommand(id), cancellationToken));


    }
}
