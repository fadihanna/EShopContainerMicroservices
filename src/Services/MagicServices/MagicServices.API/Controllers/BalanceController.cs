//using Magic.Application.Common.Payment.Commands;
//using Magic.Application.Common.Payment.Queries;
//using Magic.Application.Denominations.Queries.Denominations;
//using Microsoft.AspNetCore.Mvc;

//namespace MagicServices.API.Controllers
//{
//    public class BalanceController : ApiControllerBase
//    {
//        public BalanceController(IHostEnvironment environment) : base(environment)
//        {
//        }
//        [HttpPost("get-balance-invoiceId")]
//        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status403Forbidden)]
//        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult<InquiryResponseDto>> GetBalanceByInvoiceId(
//           [FromBody] GetBalanceByInvoiceIdQuery model, CancellationToken cancellationToken = default)
//            => Ok(await Mediator.Send(new GetBalanceByInvoiceIdQuery(model.invoiceId), cancellationToken));

//        [HttpPost("balance-insert")]
//        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status403Forbidden)]
//        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult<PaymentResponseDto>> InsertDeductBalance(
//           [FromBody] DeductBalanceCommand model, CancellationToken cancellationToken = default)
//            => Ok(await Mediator.Send(new DeductBalanceCommand(model.Request), cancellationToken));


//    }
//}
