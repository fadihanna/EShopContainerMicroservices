using Magic.Application.Dtos;
using Magic.Application.Interfaces.Specifications;

namespace Magic.Application.Notifications.Commands
{
    public record UpdateNotificationCommand(NotificationDto Notification, int Id)
        : ICommand<UpdateNotificationResponse>;

    public record UpdateNotificationResponse(int Id);

    public class UpdateNotificationHandler
        : ICommandHandler<UpdateNotificationCommand, UpdateNotificationResponse>
    {
        private readonly INotificationSpecification _notificationSpecification;

        public UpdateNotificationHandler(INotificationSpecification notificationSpecification)
        {
            _notificationSpecification = notificationSpecification;
        }

        public async Task<UpdateNotificationResponse> Handle(UpdateNotificationCommand command, CancellationToken cancellationToken)
        {
            var existingNotification = await _notificationSpecification
                .GetByIdAsync(n => n.Id == command.Id, cancellationToken);

            if (existingNotification == null)
            {
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);
            }

            existingNotification.Update(
                command.Notification.Title,
                command.Notification.Description,
                command.Notification.IconName,
                command.Notification.Status,
                command.Notification.NotificationTime,
                command.Notification.SortOrder
            );

            await _notificationSpecification.UpdateAsync(existingNotification, cancellationToken);

            return new UpdateNotificationResponse(existingNotification.Id);
        }
    }
}
