using BuildingBlocks.Models;
using Magic.Application.Common.Payment.Commands;
using Magic.Application.Common.Payment.Queries;
using Magic.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MagicServices.API.Controllers
{
    [Authorize]
    public class TransactionController : ApiControllerBase
    {
        public TransactionController(IHostEnvironment environment) : base(environment)
        {
        }
        [HttpPost("initiate-transaction")]
        [ProducesResponseType(typeof(PaymentResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaymentResponseDto>> InitiateTransaction(
        [FromBody] InsertTransactionCommand model,
         CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return Ok((await Mediator.Send(new InitiateTransactionCommand(model.Transaction, userId), cancellationToken)).paymentResponseDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("add-transaction")]
        [ProducesResponseType(typeof(PaymentResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaymentResponseDto>> PaymentRequest(
        [FromBody] InsertTransactionCommand model,
         CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return Ok((await Mediator.Send(new InsertTransactionCommand(model.Transaction,userId), cancellationToken)).paymentResponseDto);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("confirm-transaction")]
        [ProducesResponseType(typeof(PaymentResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaymentResponseDto>> ConfirmTransaction(
        [FromQuery] int requestId,string checkoutId,
         CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok((await Mediator.Send(new ConfirmTransactionCommand(requestId,userId,checkoutId),cancellationToken)).paymentResponseDto);
        }
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
