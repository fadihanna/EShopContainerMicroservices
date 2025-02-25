using BuildingBlocks.Models;
using Magic.Application.Common.Payment.Commands;
using Magic.Application.Common.Payment.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class TransactionController : ApiControllerBase
    {
        public TransactionController(IHostEnvironment environment) : base(environment)
        {
        }

        [HttpPost("add-transaction")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaymentResponseDto>> PaymentRequest(
           [FromBody] InsertTransactionCommand model, CancellationToken cancellationToken = default)
            => Ok(await Mediator.Send(new InsertTransactionCommand(model.Transaction), cancellationToken));

        [HttpGet("get-transaction-invoiceId")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaymentResponseModel>> GetTransactionByInvoiceId(
          int Id, CancellationToken cancellationToken = default)
           => Ok(await Mediator.Send(new GetTransactionByIdQuery(Id), cancellationToken));
        
        [HttpGet("get-transactions-userId")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PaymentResponseModel>>> GetTransactionByUserId(
             string userId, CancellationToken cancellationToken = default)
              => Ok(await Mediator.Send(new GetTransactionByUserIdQuery(userId), cancellationToken));

    }
}
