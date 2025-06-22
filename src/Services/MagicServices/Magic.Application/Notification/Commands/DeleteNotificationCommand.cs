using Magic.Application.Interfaces.Specifications;

namespace Magic.Application.Notifications.Commands
{
    public record DeleteNotificationCommand(int Id)
        : ICommand<DeleteNotificationResponse>;

    public record DeleteNotificationResponse(int Id);

    public class DeleteNotificationHandler
        : ICommandHandler<DeleteNotificationCommand, DeleteNotificationResponse>
    {
        private readonly INotificationSpecification _notificationSpecification;

        public DeleteNotificationHandler(INotificationSpecification notificationSpecification)
        {
            _notificationSpecification = notificationSpecification;
        }

        public async Task<DeleteNotificationResponse> Handle(DeleteNotificationCommand command, CancellationToken cancellationToken)
        {
            var existingNotification = await _notificationSpecification.GetByIdAsync(n => n.Id == command.Id, cancellationToken);

            if (existingNotification == null)
            {
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);
            }

            await _notificationSpecification.DeleteAsync(existingNotification, cancellationToken);

            return new DeleteNotificationResponse(existingNotification.Id);
        }
    }
}
