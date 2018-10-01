using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

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
        private static Dictionary<MenuEnum, EventHandler> _menus;
        private static NotifyIcon _notificationIcon;
        private static frmSettings _frmSettings;

        private static System.Drawing.Icon GetNotificationIcon { get => _timer.Enabled ? Properties.Resources.systray : Properties.Resources.systray_inactive; }
        private static MenuItem GetResumeMenu { get => _menu.MenuItems.Find(MenuEnum.Resume.ToKey(), false).First(); }
        private static MenuItem GetPauseMenu { get => _menu.MenuItems.Find(MenuEnum.Pause.ToKey(), false).First(); }
        private static string GetTooltipTitle { get => _timer.Enabled ? GetTime : _title; }
        private static string GetTime { get => _time.ToString(Properties.Settings.Default.TimeFormat); }

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

            MenuAndIconVisibility();
        }

        private static void ResetTime(bool manual = false)
        {
            _time = new DateTime();
            DisplayTime();

            if (!manual)
            {
                ShowNotificationBalloon($"New day! Time has been reset.");
            }
        }

        private static void CreateMenu()
        {
            _menu = new ContextMenu();
            _menus = new Dictionary<MenuEnum, EventHandler>()
            {
                { MenuEnum.Pause, new EventHandler(mnuPause_Click) },
                { MenuEnum.Resume, new EventHandler(mnuResume_Click) },
                { MenuEnum.Reset, new EventHandler(mnuReset_Click) },
                { MenuEnum.Settings, new EventHandler(mnuSettings_Click) },
                { MenuEnum.Exit, new EventHandler(mnuExit_Click) },
            };

            foreach (var menu in _menus)
            {
                var menuItem = new MenuItem(menu.Key.ToString(), menu.Value)
                {
                    Name = menu.Key.ToKey()
                };
                _menu.MenuItems.Add(menuItem);
            }
        }

        private static void CreateIcon()
        {
            _notificationIcon = new NotifyIcon
            {
                Icon = Properties.Resources.systray,
                ContextMenu = _menu,
                Text = _title,
                Visible = true,
                BalloonTipTitle = _title,
            };
            _notificationIcon.Click += notificationIcon_Click;
        }

        private static void AddTime()
        { 
            _time = _time.AddSeconds(1);
        }

        private static void DisplayTime()
        {
            _notificationIcon.Text = GetTooltipTitle;
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

        private static void FlipNotificationIconWith(System.Drawing.Icon icon)
        {
            if (_notificationIcon.Icon == icon) return;
            _notificationIcon.Icon = icon;
        }

        private static void Pause()
        {
            if (!_timer.Enabled) return;
            _timer.Stop();
            MenuAndIconVisibility();
        }

        private static void Resume()
        {
            if (_timer.Enabled) return;
            _timer.Start();
            MenuAndIconVisibility();
        }

        private static void MenuAndIconVisibility()
        {
            GetPauseMenu.Visible = _timer.Enabled;
            GetResumeMenu.Visible = !_timer.Enabled;
            _notificationIcon.Icon = GetNotificationIcon;
        }

        private static void ShowNotificationBalloon(string content)
        {
            _notificationIcon.BalloonTipText = content;
            _notificationIcon.ShowBalloonTip(3000);
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
            ResetTime(true);
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

        private static void notificationIcon_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button != MouseButtons.Left) return;
            ShowNotificationBalloon($"{GetTime} currently logged");
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