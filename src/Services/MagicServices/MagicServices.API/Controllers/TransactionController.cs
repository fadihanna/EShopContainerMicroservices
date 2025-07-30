using BuildingBlocks.Models;
using Magic.Application.Common.Payment.Commands;
using Magic.Application.Common.Payment.Queries;
using Magic.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MagicServices.API.Controllers
{
    //[Authorize]
    public class TransactionController : ApiControllerBase
    {
        public TransactionController(IHostEnvironment environment) : base(environment)
        {
        }


        /* [HttpPost("add-transaction")]
         [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [ProducesResponseType(StatusCodes.Status401Unauthorized)]
         [ProducesResponseType(StatusCodes.Status403Forbidden)]
         [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         public async Task<ActionResult<PaymentResponseDto>> PaymentRequest(
            [FromBody] InsertTransactionCommand model,[FromQuery]string userId ,CancellationToken cancellationToken = default)
             => Ok(await Mediator.Send(new InsertTransactionCommand(model.Transaction,userId), cancellationToken));
 */

        [HttpPost("add-transaction")]
        [ProducesResponseType(typeof(PaymentResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaymentResponseDto>> PaymentRequest(
        [FromBody] InsertTransactionCommand model,
         CancellationToken cancellationToken = default)
        {
            return Ok((await Mediator.Send(new InsertTransactionCommand(model.Transaction), cancellationToken)).paymentResponseDto);
        }

        [HttpGet("transaction-details")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaymentResponseModel>> GetTransactionByInvoiceId(
          int transactionId, CancellationToken cancellationToken = default)
           => Ok(await Mediator.Send(new GetTransactionByIdQuery(transactionId), cancellationToken));

        [HttpGet("transaction-history")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TransactionDto>>> GetTransactionByUserId(
             string userId, CancellationToken cancellationToken = default)
              => Ok(await Mediator.Send(new GetTransactionByUserIdQuery(userId), cancellationToken));

    }
}
