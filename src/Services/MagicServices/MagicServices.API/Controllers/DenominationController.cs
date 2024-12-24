using Magic.Application.Denominations.Queries.Denominations;
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
        public async Task<ActionResult<InquiryResponseDto>> GetDenominationById(
           [FromBody] GetDenominationByIdQuery model, CancellationToken cancellationToken = default)
            => Ok(await Mediator.Send(new GetDenominationByIdQuery(model.Id), cancellationToken));

        [HttpGet("get-denomination-list")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InquiryResponseDto>> GetDenominationList(CancellationToken cancellationToken = default) => 
            Ok(await Mediator.Send(new GetDenominationsQuery(), cancellationToken));
    }
}
