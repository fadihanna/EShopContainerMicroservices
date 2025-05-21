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
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> InsertServiceCategory([FromBody] InsertServiceCategoryCommand command, CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(command, cancellationToken));

        [HttpGet("get-serviceCategory-list")]
        [ProducesResponseType(typeof(GetServiceCategoriesResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetServiceCategoriesResponse>> GetServiceCategoryList(CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new GetServiceCategoriesQuery(), cancellationToken));

        [HttpGet("get-serviceCategory/{id}")]
        [ProducesResponseType(typeof(ServiceCategoryDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceCategoryDto>> GetServiceCategoryById(
           int id, CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new GetServiceCategoryByIdQuery(id), cancellationToken));

        [HttpDelete("delete-serviceCategory/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> DeleteServiceCategory(int id, CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new DeleteServiceCategoryCommand(id), cancellationToken));

        [HttpPut("update-serviceCategory/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> UpdateServiceCategory([FromBody] UpdateServiceCategoryCommand command, int id, CancellationToken cancellationToken = default)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}
