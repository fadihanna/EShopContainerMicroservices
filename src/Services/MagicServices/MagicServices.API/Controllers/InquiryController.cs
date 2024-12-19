using BuildingBlocks.Exceptions;
using Magic.Application.Common.Inquiry.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ApiControllerBase
    {
        public InquiryController(IHostEnvironment environment) : base(environment)
        {
        }

        [HttpPost("inquiry")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(InternalServerException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InquiryResponseDto>> Inquiry(
           [FromBody] InquiryRequestDto model, CancellationToken cancellationToken = default)
            => Ok(await Mediator.Send(new InquiryCommand(model), cancellationToken));

    }
}
