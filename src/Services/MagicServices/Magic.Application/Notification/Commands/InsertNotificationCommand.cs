using Magic.Application.Interfaces.Specifications;
using Magic.Domain.Models;

namespace Magic.Application.Notifications.Commands
{
    public record InsertNotificationCommand(
        string Title,
        string Description,
        string IconName,
        int Status,
        DateTime NotificationTime,
        int SortOrder
    ) : ICommand<InsertNotificationResponse>;

    public record InsertNotificationResponse(int Id);

    public class InsertNotificationHandler
        : ICommandHandler<InsertNotificationCommand, InsertNotificationResponse>
    {
        private readonly INotificationSpecification _notificationSpecification;

        public InsertNotificationHandler(INotificationSpecification notificationSpecification)
        {
            _notificationSpecification = notificationSpecification;
        }

        public async Task<InsertNotificationResponse> Handle(InsertNotificationCommand command, CancellationToken cancellationToken)
        {
            var newNotification = Notification.Create(
                title: command.Title,
                description: command.Description,
                iconName: command.IconName,
                status: command.Status,
                notificationTime: command.NotificationTime,
                sortOrder: command.SortOrder
            );

            await _notificationSpecification.AddAsync(newNotification, cancellationToken);

            return new InsertNotificationResponse(newNotification.Id);
        }
    }
}
