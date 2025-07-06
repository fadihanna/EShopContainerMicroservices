using Magic.Application.Common.Fees.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class FeesController : ApiControllerBase
    {
        public FeesController(IHostEnvironment environment) : base(environment)
        {
        }
        [HttpPost("fees")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FeesResponseDto>> Fees(
           [FromBody] GetFeesQuery model, CancellationToken cancellationToken = default)
            => Ok((await Mediator.Send(new GetFeesQuery(model.Request), cancellationToken)).FeesResponseDto);

    }
}
