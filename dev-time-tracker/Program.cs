using Microsoft.Win32;
using System;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace DevTimeTracker
{
    internal static class Program
    {
        private const string Title = "Dev Time Tracker";
        private static bool _isPauseForced;
        private static System.Timers.Timer _timer;
        private static DateTime _time;
        private static ContextMenu _menu;
        private static MenuItem _mnuExit;
        private static MenuItem _mnuSettings;
        private static MenuItem _mnuReset;
        private static MenuItem _mnuPause;
        private static MenuItem _mnuResume;
        private static NotifyIcon _notificationIcon;

        private static bool IsTimeRunning { get => _timer.Enabled && _time.Ticks > 0; }

        [STAThread]
        private static void Main()
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
            _timer = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 1000
            };

            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            if (Properties.Settings.Default.AutoStart)
            {
                _timer.Start();
            }
        }

        private static void ResetTime()
        {
            _time = new DateTime();
            DisplayTime();
        }

        private static void CreateMenu()
        {
            _menu = new ContextMenu();

            _mnuPause = new MenuItem("Pause");
            _menu.MenuItems.Add(0, _mnuPause);
            _mnuPause.Click += new EventHandler(mnuPause_Click);
            _mnuPause.Visible = false;

            _mnuResume = new MenuItem("Resume");
            _menu.MenuItems.Add(1, _mnuResume);
            _mnuResume.Click += new EventHandler(mnuResume_Click);

            _mnuReset = new MenuItem("Reset");
            _menu.MenuItems.Add(2, _mnuReset);
            _mnuReset.Click += new EventHandler(mnuReset_Click);

            _mnuSettings = new MenuItem("Settings");
            _menu.MenuItems.Add(3, _mnuSettings);
            _mnuSettings.Click += new EventHandler(mnuSettings_Click);

            _mnuExit = new MenuItem("Exit");
            _menu.MenuItems.Add(4, _mnuExit);
            _mnuExit.Click += new EventHandler(mnuExit_Click);
        }

        private static void CreateIcon()
        {
            _notificationIcon = new NotifyIcon
            {
                Icon = Properties.Resources.systray,
                ContextMenu = _menu,
                Text = Title,
                Visible = true
            };
        }

        private static void AddTime()
        { 
            _time = _time.AddSeconds(1);
        }

        private static void DisplayTime()
        {
            _notificationIcon.Text = IsTimeRunning ? _time.ToString(Properties.Settings.Default.TimeFormat) : Title;
        }

        private static void CheckUserActivity()
        {
            if (_isPauseForced || !Properties.Settings.Default.AfkEnabled) return;

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

            var now = DateTime.Now;
            var isOClock = now.Minute == 0 && now.Second == 0;
            if (now.Hour == Properties.Settings.Default.ResetDailyAtHour && isOClock)
            {
                ResetTime();
            }
        }

        private static void Pause()
        {
            if (!_timer.Enabled) return;
            _timer.Stop();
        }

        private static void Resume()
        {
            if (_timer.Enabled) return;
            _timer.Start();
        }

        private static void timer_Elapsed(object sender, EventArgs e)
        {
            AddTime();
            DisplayTime();
            CheckResetTimerDaily();

            if (_time.Second == 0)
            {
                CheckUserActivity();
            }

            MenuVisibility();
        }

        private static void MenuVisibility()
        {
            var isRunning = IsTimeRunning;
            _mnuPause.Visible = isRunning;
            _mnuResume.Visible = !isRunning;
        }

        private static void mnuExit_Click(object sender, EventArgs e)
        {
            _notificationIcon.Visible = false;
            _notificationIcon.Dispose();
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
            _isPauseForced = true;
            Pause();
        }

        private static void mnuResume_Click(object sender, EventArgs e)
        {
            _isPauseForced = false;
            Resume();
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (!Properties.Settings.Default.LockScreenEnabled) return;

            if (e.Reason == SessionSwitchReason.SessionLock )
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