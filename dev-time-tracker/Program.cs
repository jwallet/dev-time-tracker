using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Linq;

namespace DevTimeTracker
{
    internal static class Program
    {
        private const string _title = "Dev Time Tracker";
        private const long _dayTotalTicks = 864000000000;
        private static bool _isPauseForced;
        private static bool _wasDailyReset;
        private static System.Timers.Timer _timer;
        private static DateTime _time;
        private static DateTime _date;
        private static ContextMenu _menu;
        private static MenuItem _mnuExit;
        private static MenuItem _mnuSettings;
        private static MenuItem _mnuReset;
        private static MenuItem _mnuPause;
        private static MenuItem _mnuResume;
        private static NotifyIcon _notificationIcon;
        private static frmSettings _frmSettings;

        private static bool IsTimeRunning { get => _timer.Enabled && _time.Ticks > 0; }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Process.GetProcessesByName(typeof(Program).Assembly.GetName().Name).Count() > 1) return;

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
            _date = DateTime.Now;

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
                Text = _title,
                Visible = true
            };
        }

        private static void AddTime()
        { 
            _time = _time.AddSeconds(1);
        }

        private static void DisplayTime()
        {
            _notificationIcon.Text = IsTimeRunning ? _time.ToString(Properties.Settings.Default.TimeFormat) : _title;
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
            var resetAtHour = Properties.Settings.Default.ResetDailyAtHour;

            if (_wasDailyReset = (_wasDailyReset && now.Hour >= resetAtHour)) return;
 
            if (now.Hour == resetAtHour && now.Minute == 0 && now.Second == 0)
            {
                ResetTime();
                _wasDailyReset = true;
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
            if (_frmSettings == null)
            {
                _frmSettings = new frmSettings();
                _frmSettings.Left = Screen.PrimaryScreen.WorkingArea.Right - _frmSettings.Width;
                _frmSettings.Top = Screen.PrimaryScreen.WorkingArea.Bottom - _frmSettings.Height;
                _frmSettings.FormClosed += delegate { _frmSettings = null; };
            }
            _frmSettings.Show();
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
            if (e.Reason == SessionSwitchReason.SessionLock )
            {
                if (Properties.Settings.Default.LockScreenEnabled)
                {
                    Pause();
                }
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                if (Properties.Settings.Default.LockScreenEnabled)
                {
                    Resume();
                }
                if (Properties.Settings.Default.ResetDailyEnabled && DateTime.Now.AddTicks(-_date.Ticks).Ticks >= _dayTotalTicks)
                {
                    ResetTime();
                    _date = DateTime.Now;
                    _wasDailyReset = true;
                }
            }
        }
        private static void BindToLockScreen()
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }
    }
}