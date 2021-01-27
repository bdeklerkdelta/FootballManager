using System.Collections.Generic;
using System.Linq;
using FootballManager.Common.Notifications;

namespace FootballManager.Common.Extensions
{
    public static class NotificationExtensions
    {
        public static bool HasErrors(this IEnumerable<Notification> notifications)
        {
            return notifications.Contains(m => m.Severity == NotificationSeverity.Error);
        }

        public static bool HasMessages(this IEnumerable<Notification> notifications)
        {
            return notifications.Any();
        }

        public static bool HasWarnings(this IEnumerable<Notification> notifications)
        {
            return notifications.Contains(m => m.Severity == NotificationSeverity.Warning);
        }
    }
}
