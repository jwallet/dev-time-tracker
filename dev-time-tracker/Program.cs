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

        private static SessionSwitchReason[] unlockedReasons;
        private static SessionSwitchReason[] lockedReasons;
        private static bool _isSuspendForced;
        private static bool _isRunning;
        private static bool _wasDailyReset;
        private static System.Timers.Timer _timer;
        private static DateTime _time;
        private static DateTime _date;
        private static Menus _menus;
        private static Notification _notification;
        private static frmSettings _frmSettings;

        private static System.Drawing.Icon GetNotificationIcon { get => _isRunning ? Properties.Resources.systray : Properties.Resources.systray_inactive; }
        private static string GetTooltipTitle { get => _isRunning ? GetTime : _title; }
        private static string GetTime { get => _time.ToTimeFormat(); }
        private static bool IsDailyResetNeeded { get => Properties.Settings.Default.ResetDailyEnabled && DateTime.Now.AddTicks(-_date.Ticks).Ticks >= _dayTotalTicks; }

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
                    BindToPowerMode();
                    BindToSessionEnding();

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
            if (!manual)
            {
                _notification.ShowNotificationBalloon(Notification.GetOnResetContent());
                ValidFormInstance();
                SaveLastShift();
            }

            _time = new DateTime();
            DisplayTime();
        }

        private static void SaveLastShift()
        {
            _frmSettings.SaveLastShift(_time);
        }

        private static void CreateMenu()
        {
            var menusDictionary = new Dictionary<MenuEnum, EventHandler>()
            {
                { MenuEnum.Suspend, new EventHandler(mnuSuspend_Click) },
                { MenuEnum.Resume, new EventHandler(mnuResume_Click) },
                { MenuEnum.Reset, new EventHandler(mnuReset_Click) },
                { MenuEnum.Settings, new EventHandler(mnuSettings_Click) },
                { MenuEnum.Exit, new EventHandler(mnuExit_Click) },
            };

            _menus = new Menus(menusDictionary);
        }

        private static void CreateIcon()
        {
            _notification = new Notification(_menus.Menu, notificationIcon_Click, _title);
        }

        private static void AddTime()
        {
            if (!_isRunning) return;
            _time = _time.AddSeconds(1);
        }

        private static void DisplayTime()
        {
            _notification.NotificationIcon.Text = GetTooltipTitle;
        }

        private static void CheckUserActivity()
        {
            if (_isSuspendForced || !Properties.Settings.Default.AfkEnabled) return;

            var lastActivityLogMilliseconds = NativeMethods.GetIdleTime();
            if (lastActivityLogMilliseconds > Properties.Settings.Default.AfkDelayMilliseconds)
            {
                Suspend();
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

        private static void Suspend()
        {
            if (!_isRunning) return;
            _isRunning = false;
            MenuAndIconVisibility();
        }

        private static void Resume()
        {
            if (_isRunning) return;
            _isRunning = true;
            MenuAndIconVisibility();
        }

        private static void MenuAndIconVisibility()
        {
            _menus.GetSuspendMenu.Visible = _isRunning;
            _menus.GetResumeMenu.Visible = !_isRunning;
            _notification.NotificationIcon.Icon = GetNotificationIcon;
        }

        private static void ValidFormInstance()
        {
            if (_frmSettings == null)
            {
                _frmSettings = new frmSettings();
                _frmSettings.Left = Screen.PrimaryScreen.WorkingArea.Right - _frmSettings.Width;
                _frmSettings.Top = Screen.PrimaryScreen.WorkingArea.Bottom - _frmSettings.Height;
                _frmSettings.FormClosed += delegate { _frmSettings = null; };
            }
        }

        private static void DailyResetTime()
        {
            ResetTime();
            _date = DateTime.Now;
            _wasDailyReset = true;
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (lockedReasons.Contains(e.Reason))
            {
                if (Properties.Settings.Default.LockScreenEnabled)
                {
                    Suspend();
                }
            }
            else if (unlockedReasons.Contains(e.Reason))
            {
                if (Properties.Settings.Default.LockScreenEnabled)
                {
                    Resume();
                }
                if (IsDailyResetNeeded)
                {
                    DailyResetTime();
                }
            }
        }

        private static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend)
            {
                Suspend();
            }
            else if (e.Mode == PowerModes.Resume)
            {
                Resume();
                if (IsDailyResetNeeded)
                {
                    DailyResetTime();
                }
            }
        }

        private static void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            SaveLastShift();
            ResetTime();
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

        private static void notificationIcon_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button != MouseButtons.Left) return;
            _notification.ShowNotificationBalloon(Notification.GetOnClickContent(GetTime));
        }

        #region Menu Click Handlers
        private static void mnuExit_Click(object sender, EventArgs e)
        {
            ValidFormInstance();
            SaveLastShift();

            _notification.Dispose();
            Application.Exit();
        }

        private static void mnuSettings_Click(object sender, EventArgs e)
        {
            ValidFormInstance();
            _frmSettings.Show();
        }

        private static void mnuReset_Click(object sender, EventArgs e)
        {
            ResetTime(true);
        }

        private static void mnuSuspend_Click(object sender, EventArgs e)
        {
            _isSuspendForced = true;
            Suspend();
        }

        private static void mnuResume_Click(object sender, EventArgs e)
        {
            _isSuspendForced = false;
            Resume();
        }
        #endregion

        #region System Events Binder
        private static void BindToLockScreen()
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            unlockedReasons = new[] { SessionSwitchReason.ConsoleConnect, SessionSwitchReason.RemoteConnect, SessionSwitchReason.SessionLogon, SessionSwitchReason.SessionUnlock };
            lockedReasons = new[] { SessionSwitchReason.ConsoleDisconnect, SessionSwitchReason.RemoteDisconnect, SessionSwitchReason.SessionLogoff, SessionSwitchReason.SessionLock };
        }

        private static void BindToPowerMode()
        {
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
        }

        private static void BindToSessionEnding()
        {
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
        }
        #endregion
    }
}