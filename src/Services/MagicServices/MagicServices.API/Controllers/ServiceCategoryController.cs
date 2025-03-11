using Magic.Application.Commands;
using Magic.Application.Dtos;
using Magic.Application.ServiceCategories.Queries.ServiceCategories;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class ServiceCategoryController : ApiControllerBase
    {
        public ServiceCategoryController(IHostEnvironment environment) : base(environment)
        {
        }
       
        [HttpPost("insert-serviceCategory")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> InsertServiceCategory([FromBody] InsertServiceCategoryCommand model, CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new InsertServiceCategoryCommand(model.serviceCategory), cancellationToken));

        [HttpGet("get-serviceCategory-list")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetServiceCategoriesResponse>> GetServiceCategoryList(CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new GetServiceCategoriesQuery(), cancellationToken));

        [HttpGet("get-serviceCategory")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ServiceCategoryDto>> GetDenominationById(
           int Id, CancellationToken cancellationToken = default)
            => Ok(await Mediator.Send(new GetServiceCategoryByIdQuery(Id), cancellationToken));

        [HttpDelete("delete-serviceCategory")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> DeleteServiceCategory(int Id, CancellationToken cancellationToken = default) =>
          Ok(await Mediator.Send(new DeleteServiceCategoryCommand(Id), cancellationToken));

        [HttpPost("update-serviceCategory")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> UpdateServiceCategory([FromBody] UpdateServiceCategoryCommand model,int Id, CancellationToken cancellationToken = default) =>
        Ok(await Mediator.Send(new UpdateServiceCategoryCommand(model.serviceCategory,Id), cancellationToken));
    }
}
