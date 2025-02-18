using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Grpc.Protos;

namespace MagicServices.API.Controllers
{
    public class PaymentGatewayController : Controller
    {
        private readonly PaymentGatewayClientService _paymentGatewayClientService;
        public PaymentGatewayController(PaymentGatewayClientService paymentGatewayClientService)
        {
            _paymentGatewayClientService = paymentGatewayClientService;
        }

        [HttpPost("processPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            try
            {
                var response = await _paymentGatewayClientService.ProcessPaymentAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
