using Magic.Application.Notifications.Commands;
using Magic.Application.Notifications.Queries;
using Magic.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class NotificationController : ApiControllerBase
    {
        public NotificationController(IHostEnvironment environment) : base(environment) { }

        [HttpGet("get-notification-list")]
        [ProducesResponseType(typeof(GetNotificationsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetNotificationsResponse>> GetNotificationList(CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetNotificationsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPost("insert-notification")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> InsertNotification(
            [FromBody] InsertNotificationCommand command, CancellationToken cancellationToken = default)
        {
            var response = await Mediator.Send(command, cancellationToken);
            return Ok(response.Id);
        }

        [HttpPut("update-notification/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> UpdateNotification(
            [FromBody] NotificationDto notificationDto, [FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var command = new UpdateNotificationCommand(notificationDto, id);
            var response = await Mediator.Send(command, cancellationToken);
            return Ok(response.Id);
        }

        [HttpDelete("delete-notification/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> DeleteNotification([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await Mediator.Send(new DeleteNotificationCommand(id), cancellationToken);
            return Ok(id);
        }
    }
}
