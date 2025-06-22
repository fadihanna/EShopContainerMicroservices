using Magic.Application.Commands;
using Magic.Application.Denominations.Queries.Denominations;
using Magic.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class ServicesController : ApiControllerBase
    {
        public ServicesController(IHostEnvironment environment) : base(environment)
        {
        }
        [HttpPost("get-service")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InquiryResponseDto>> GetServiceById(
         [FromBody] GetServiceByIdQuery model, CancellationToken cancellationToken = default)
          => Ok(await Mediator.Send(new GetServiceByIdQuery(model.Id), cancellationToken));

        [HttpGet("get-service-list")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InquiryResponseDto>> GetServiceList(CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new GetServiceQuery(), cancellationToken));
        [HttpGet("services-with-denomination-group")]
        public async Task<ActionResult<GetServicesWithDenominationResponse>> GetServicesWithDenominationGroup(CancellationToken cancellationToken = default)
    => Ok(await Mediator.Send(new GetServicesWithDenominationQuery(), cancellationToken));
        [HttpGet("services-with-denomination")]
        public async Task<ActionResult<GetServicesDenominationsResponse>> GetServicesWithDenomination(CancellationToken cancellationToken = default)
=> Ok(await Mediator.Send(new GetServicesDenominationsQuery(), cancellationToken));

        [HttpPost("insert-service")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> InsertService([FromBody] InsertServiceCommand model, CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new InsertServiceCommand(model.service), cancellationToken));

        [HttpDelete("delete-service")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> DeleteService(int Id, CancellationToken cancellationToken = default) =>
          Ok(await Mediator.Send(new DeleteServiceCommand(Id), cancellationToken));

        [HttpPost("update-service")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> UpdateService([FromBody] UpdateServiceCommand model,int Id, CancellationToken cancellationToken = default) =>
        Ok(await Mediator.Send(new UpdateServiceCommand(model.service,Id), cancellationToken));
        [HttpGet("services/by-category/{categoryId}")]
        public async Task<ActionResult<GetServiceByCategoryResponse>> GetServicesByCategory(int categoryId, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetServiceByCategoryQuery(categoryId), cancellationToken));
        }
    }

}
