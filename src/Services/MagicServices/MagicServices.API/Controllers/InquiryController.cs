using Magic.Application.Common.Inquiry.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class InquiryController : ApiControllerBase
    {
        public InquiryController(IHostEnvironment environment) : base(environment)
        {
        }

        [HttpPost("inquiry")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InquiryResponseDto>> Inquiry(
           [FromBody] InquiryCommand model, CancellationToken cancellationToken = default)
            => Ok(await Mediator.Send(new InquiryCommand(model.Request), cancellationToken));

    }
} 
