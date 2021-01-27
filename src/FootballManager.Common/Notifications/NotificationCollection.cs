using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FootballManager.Common.Extensions;

namespace FootballManager.Common.Notifications
{
    /// <summary>
    /// https://martinfowler.com/eaaDev/Notification.html
    /// </summary>

    public sealed class NotificationCollection : IFormattable, IEnumerable<Notification>, IEnumerable
    {

        private readonly List<Notification> messages = new List<Notification>();

        public NotificationCollection Errors()
        {
            return Create(messages.Where(m => m.Severity == NotificationSeverity.Error).ToList());
        }

        public bool HasMessages()
        {
            return messages.HasMessages();
        }

        public bool HasWarnings()
        {
            return messages.HasWarnings();
        }

        public bool HasErrors()
        {
            return messages.HasErrors();
        }

        public void AddMessage(Notification notification)
        {
            if (notification == null)
                return;
            messages.Add(notification);
        }

        public void AddMessage(Notification notification, int index)
        {
            if (notification == null)
                return;
            messages.Insert(index, notification);
        }

        public void AddMessage(IEnumerable<Notification> notifications)
        {
            if (notifications == null)
                return;
            messages.AddRange(notifications);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Notification message in messages)
                stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[2]
                {
                  stringBuilder.Length > 0 ?  Environment.NewLine :   null,
                   message
                }));
            return stringBuilder.ToString();
        }

        public static NotificationCollection CreateEmpty()
        {
            return new NotificationCollection();
        }

        public static NotificationCollection Create(Notification notification)
        {
            NotificationCollection empty = CreateEmpty();
            empty.AddMessage(notification);
            return empty;
        }

        public static NotificationCollection Create(IList<Notification> notifications)
        {
            NotificationCollection empty = CreateEmpty();
            empty.AddMessage(notifications);
            return empty;
        }

        public static NotificationCollection Add(NotificationCollection left, NotificationCollection right)
        {
            return left + right;
        }

        public static NotificationCollection operator +(NotificationCollection left, NotificationCollection right)
        {
            if (right != null)
                left.AddMessage(right);
            return left;
        }

        public static NotificationCollection operator +(NotificationCollection left, Notification right)
        {
            if (right != null)
                left.AddMessage(right);
            return left;
        }

        public static implicit operator string(NotificationCollection notification)
        {
            return notification.ToString();
        }

        IEnumerator<Notification> IEnumerable<Notification>.GetEnumerator()
        {
            for (int i = 0; i < messages.Count; ++i)
                yield return messages[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Notification>)this).GetEnumerator();
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        public object Payload { get; set; }
    }
}
