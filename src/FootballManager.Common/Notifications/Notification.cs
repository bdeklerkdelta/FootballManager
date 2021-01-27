namespace FootballManager.Common.Notifications
{
    public class Notification
    {
        public Notification(string text)
        {
            Text = text;
            Severity = NotificationSeverity.Information;
        }

        public Notification(string text, NotificationSeverity severity)
        {
            Text = text;
            Severity = severity;
        }

        public Notification(string text, NotificationSeverity severity, object tag)
        {
            Text = text;
            Severity = severity;
            Tag = tag;
        }

        public Notification(string code, string text, NotificationSeverity severity)
        {
            Text = text;
            Severity = severity;
            Code = code;
        }

        public Notification(string code, string text, NotificationSeverity severity, object tag)
        {
            Text = text;
            Severity = severity;
            Tag = tag;
            Code = code;
        }

        public NotificationSeverity Severity { get; set; }

        public string Text { get; set; }

        public string Code { get; set; }

        public object Tag { get; set; }

        public static Notification Create(string message, NotificationSeverity notificationSeverity)
        {
            return new Notification(message, notificationSeverity);
        }

        public static Notification Create(string code, string message, NotificationSeverity notificationSeverity)
        {
            return new Notification(code, message, notificationSeverity);
        }

        public static Notification Create(string code, string message, NotificationSeverity notificationSeverity, object tag)
        {
            return new Notification(code, message, notificationSeverity, tag);
        }

        public override string ToString()
        {
            return $"[{ Severity }]:[{ Code }]:[{ Text }]:[{ Tag }]";
        }
    }
}
