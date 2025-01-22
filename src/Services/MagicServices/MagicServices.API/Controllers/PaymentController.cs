using Magic.Application.Denominations.Queries.Denominations;
using MagicPaymentAPI.DTO.Login;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class PaymentController : ApiControllerBase
    {
        public PaymentController(IHostEnvironment environment) : base(environment)
        {
        }
        [HttpGet("Login")]
        public async Task<ActionResult<LoginResponse>> Login()
        {
            return Ok();
        }

    }
}
