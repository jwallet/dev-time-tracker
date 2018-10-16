using System;
using System.Windows.Forms;

namespace DevTimeTracker
{
    internal class Notification
    {
        internal NotifyIcon NotificationIcon { get; private set; }
        private static string GetLastShift => new DateTime().AddTicks(Properties.Settings.Default.LastShiftTicks).ToTimeFormat();

        internal Notification(ContextMenu menu, EventHandler mouseClick, string title)
        {
            NotificationIcon = new NotifyIcon
            {
                Icon = Properties.Resources.systray,
                ContextMenu = menu,
                Text = title,
                Visible = true,
                BalloonTipTitle = title,
            };
            NotificationIcon.Click += mouseClick;
        }

        internal void ShowNotificationBalloon(string content)
        {
            NotificationIcon.BalloonTipText = content;
            NotificationIcon.ShowBalloonTip(3000);
        }

        internal void Dispose()
        {
            NotificationIcon.Visible = false;
            NotificationIcon.Dispose();
        }

        internal static string GetOnClickContent(string currentTime)
        {
            var currently = $"{currentTime} currently logged";
            var lastShift = $"Last time: {GetLastShift}";

            return $"{currently}\n{lastShift}";
        }

        internal static string GetOnResetContent()
        {
            return "New day! Time has been reset.";
        }
    }
}
