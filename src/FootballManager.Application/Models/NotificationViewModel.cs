using FootballManager.Common.Notifications;

namespace FootballManager.Application.Models
{
    public class NotificationViewModel
    {
        public NotificationViewModel()
        {
            Notifications = NotificationCollection.CreateEmpty();
        }

        public NotificationCollection Notifications { get; set; }
    }
}
