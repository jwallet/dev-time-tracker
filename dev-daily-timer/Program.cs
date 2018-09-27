using Microsoft.Win32;
using System;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace dev_daily_timer
{
    static class Program
    {
        private const string title = "Dev Daily Timer";
        private static bool isPauseForced;
        private static System.Timers.Timer timer;
        private static DateTime time;
        private static ContextMenu menu;
        private static MenuItem mnuExit;
        private static MenuItem mnuSettings;
        private static MenuItem mnuReset;
        private static MenuItem mnuPause;
        private static MenuItem mnuResume;
        private static NotifyIcon notificationIcon;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var notifyThread = new Thread(
                delegate ()
                {
                    CreateMenu();
                    CreateIcon();
                    CreateTimer();
                    BindToLockScreen();

                    Application.Run();
                }
            );

            notifyThread.Start();
        }

        private static void CreateTimer()
        {
            timer = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 1000
            };

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            if (Properties.Settings.Default.AutoStart)
            {
                timer.Start();
            }
        }

        private static void ResetTime()
        {
            time = new DateTime();
            DisplayTime();
        }

        private static void CreateMenu()
        {
            menu = new ContextMenu();

            mnuPause = new MenuItem("Pause");
            menu.MenuItems.Add(0, mnuPause);
            mnuPause.Click += new EventHandler(mnuPause_Click);
            mnuPause.Visible = false;

            mnuResume = new MenuItem("Resume");
            menu.MenuItems.Add(1, mnuResume);
            mnuResume.Click += new EventHandler(mnuResume_Click);

            mnuReset = new MenuItem("Reset");
            menu.MenuItems.Add(2, mnuReset);
            mnuReset.Click += new EventHandler(mnuReset_Click);

            mnuSettings = new MenuItem("Settings");
            menu.MenuItems.Add(3, mnuSettings);
            mnuSettings.Click += new EventHandler(mnuSettings_Click);

            mnuExit = new MenuItem("Exit");
            menu.MenuItems.Add(4, mnuExit);
            mnuExit.Click += new EventHandler(mnuExit_Click);
        }

        private static void CreateIcon()
        {
            notificationIcon = new NotifyIcon
            {
                Icon = Properties.Resources.systray,
                ContextMenu = menu,
                Text = title
            };
            notificationIcon.Visible = true;
        }

        private static void AddTime()
        { 
            time = time.AddSeconds(1);
        }

        private static void DisplayTime()
        {
            notificationIcon.Text = IsTimeRunning() ? time.ToString("HH:mm:ss") : title;
        }

        private static bool IsTimeRunning()
        {
            return timer.Enabled && time.Ticks > 0;
        }

        private static void CheckUserActivity()
        {
            if (isPauseForced || !Properties.Settings.Default.AfkEnabled) return;

            var lastActivityLogMilliseconds = AwayFromKeyboard.GetIdleTime();
            if (lastActivityLogMilliseconds > Properties.Settings.Default.AfkDelayMilliseconds)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

        private static void CheckResetTimerDaily()
        {
            if (!Properties.Settings.Default.ResetDailyEnabled) return;

            if (DateTime.Now.Hour >= Properties.Settings.Default.ResetDailyAtHour)
            {
                ResetTime();
            }
        }

        private static void Pause()
        {
            if (!timer.Enabled) return;
            timer.Stop();
        }

        private static void Resume()
        {
            if (timer.Enabled) return;
            timer.Start();
        }

        private static void timer_Elapsed(object sender, EventArgs e)
        {
            AddTime();
            DisplayTime();

            if (time.Second == 0)
            {
                CheckUserActivity();
            }
            if (time.Minute == 0)
            {
                CheckResetTimerDaily();
            }

            MenuVisibility();
        }

        private static void MenuVisibility()
        {
            var isRunning = IsTimeRunning();
            mnuPause.Visible = isRunning;
            mnuResume.Visible = !isRunning;
        }

        private static void mnuExit_Click(object sender, EventArgs e)
        {
            notificationIcon.Visible = false;
            notificationIcon.Dispose();
            Application.Exit();
        }

        private static void mnuSettings_Click(object sender, EventArgs e)
        {
            var frmSettings = new frmSettings();
            frmSettings.Left = Screen.PrimaryScreen.WorkingArea.Right - frmSettings.Width;
            frmSettings.Top = Screen.PrimaryScreen.WorkingArea.Bottom - frmSettings.Height;
            frmSettings.Show();
        }

        private static void mnuReset_Click(object sender, EventArgs e)
        {
            ResetTime();
        }

        private static void mnuPause_Click(object sender, EventArgs e)
        {
            isPauseForced = true;
            Pause();
        }

        private static void mnuResume_Click(object sender, EventArgs e)
        {
            isPauseForced = false;
            Resume();
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (!Properties.Settings.Default.LockScreenEnabled) return;

            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                Pause();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Resume();
            }
        }
        private static void BindToLockScreen()
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }
    }
}