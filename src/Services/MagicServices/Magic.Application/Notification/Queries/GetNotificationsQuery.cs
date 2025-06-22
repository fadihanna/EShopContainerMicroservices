using Magic.Application.Interfaces.Specifications;

namespace Magic.Application.Notifications.Queries
{
    public class GetNotificationsQuery : IQuery<GetNotificationsResponse>;

    public record GetNotificationsResponse(List<NotificationDto> NotificationListDto);

    public class GetNotificationsHandler
        : IQueryHandler<GetNotificationsQuery, GetNotificationsResponse>
    {
        private readonly INotificationSpecification _notificationSpecification;

        public GetNotificationsHandler(INotificationSpecification notificationSpecification)
        {
            _notificationSpecification = notificationSpecification;
        }

        public async Task<GetNotificationsResponse> Handle(GetNotificationsQuery query, CancellationToken cancellationToken)
        {
            var notificationList = await _notificationSpecification.GetAllAsync(cancellationToken);
            return new GetNotificationsResponse(notificationList!.ToNotificationDtoList().ToList());
        }
    }
}
