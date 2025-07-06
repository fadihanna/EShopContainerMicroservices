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

        [HttpPost("insert-category")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> InsertServiceCategory([FromBody] InsertServiceCategoryCommand command, CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(command, cancellationToken));

        [HttpGet("get-category-list")]
        [ProducesResponseType(typeof(GetServiceCategoriesResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetServiceCategoriesResponse>> GetServiceCategoryList(CancellationToken cancellationToken = default) =>
            Ok((await Mediator.Send(new GetServiceCategoriesQuery(), cancellationToken))?.serviceCategoryListDto);

        [HttpGet("get-category/{id}")]
        [ProducesResponseType(typeof(ServiceCategoryDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceCategoryDto>> GetServiceCategoryById(
           int id, CancellationToken cancellationToken = default) =>
            Ok((await Mediator.Send(new GetServiceCategoryByIdQuery(id), cancellationToken))?.serviceCategoryDto);

        [HttpDelete("delete-category/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> DeleteServiceCategory(int id, CancellationToken cancellationToken = default) =>
            Ok(await Mediator.Send(new DeleteServiceCategoryCommand(id), cancellationToken));

        [HttpPut("update-category/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> UpdateServiceCategory([FromBody] UpdateServiceCategoryCommand command, int id, CancellationToken cancellationToken = default)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}
