using System;
using System.Globalization;
using FootballManager.Common.Notifications;

namespace FootballManager.Application.ResponseNotifications.ErrorResponses
{
    public static class ErrorResponseNotifications
    {
        public static class GeneralErrors
        {
            public static Notification Unhandled(Guid callReference)
            {
                var message = string.Format(CultureInfo.InvariantCulture, ErrorResponseText.GeneralUnhandled);
                return Notification.Create("GE001", message, NotificationSeverity.Error, callReference);
            }

            public static Notification EntityDoesNotExist(Guid callReference)
            {
                var message = string.Format(CultureInfo.InvariantCulture, ErrorResponseText.EntityDoesntExist);
                return Notification.Create("GE002", message, NotificationSeverity.Error, callReference);
            }
        }
    }
}
