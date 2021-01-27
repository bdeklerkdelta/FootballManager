using FootballManager.Common.Notifications;

namespace FootballManager.Common.Extensions
{
    public static class NotificationCollectionExtensions
    {
        public static void AddError(this NotificationCollection notifications, string message)
        {
            notifications += Notification.Create(message, NotificationSeverity.Error);
        }

        public static void AddWarning(this NotificationCollection notifications, string message)
        {
            notifications += Notification.Create(message, NotificationSeverity.Warning);
        }

        public static void AddInformation(this NotificationCollection notifications, string message)
        {
            notifications += Notification.Create(message, NotificationSeverity.Information);
        }
    }
}
