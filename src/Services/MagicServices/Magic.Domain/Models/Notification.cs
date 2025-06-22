namespace Magic.Domain.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
        public int Status { get; set; }
        public DateTime NotificationTime { get; set; }
        public int SortOrder { get; set; }

         public static Notification Create(string title, string description, string iconName, int status, DateTime notificationTime, int sortOrder = 0)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be empty.", nameof(description));
            if (string.IsNullOrWhiteSpace(iconName)) throw new ArgumentException("Icon name cannot be empty.", nameof(iconName));

            return new Notification
            {
                Title = title,
                Description = description,
                IconName = iconName,
                Status = status,
                NotificationTime = notificationTime,
                SortOrder = sortOrder
            };
        }

         public void Update(string title, string description, string iconName, int status, DateTime notificationTime, int sortOrder)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be empty.", nameof(description));
            if (string.IsNullOrWhiteSpace(iconName)) throw new ArgumentException("Icon name cannot be empty.", nameof(iconName));

            Title = title;
            Description = description;
            IconName = iconName;
            Status = status;
            NotificationTime = notificationTime;
            SortOrder = sortOrder;
        }
    }
}
