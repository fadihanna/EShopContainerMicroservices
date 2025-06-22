using Magic.Application.Dtos;
using Magic.Domain.Models;

namespace Magic.Application.Extensions
{
    public static class NotificationExtensions  
    {
        public static IEnumerable<NotificationDto> ToNotificationDtoList(this IEnumerable<Notification> notifications)
        {
            return notifications.Select(ToNotificationDto);
        }

        public static NotificationDto ToNotificationDto(this Notification notification)
        {
            return new NotificationDto
            (
                Id: notification.Id,
                Title: notification.Title,
                Description: notification.Description,
                IconName: notification.IconName,
                Status: notification.Status,
                NotificationTime: notification.NotificationTime,
                SortOrder: notification.SortOrder
            );
        }

        public static Notification DtoToNotification(this NotificationDto dto)
        {
            return new Notification
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                IconName = dto.IconName,
                Status = dto.Status,
                NotificationTime = dto.NotificationTime,
                SortOrder = dto.SortOrder
            };
        }
    }
}
 
