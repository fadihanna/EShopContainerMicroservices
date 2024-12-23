using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        readonly IHostEnvironment environment;
        public ApiControllerBase(IHostEnvironment environment) {
            this.environment = environment;
        }
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
        private ILogger<ApiControllerBase> _logger => HttpContext.RequestServices.GetRequiredService<ILogger<ApiControllerBase>>();

    }
}
