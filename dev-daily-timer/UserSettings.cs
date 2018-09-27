using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace harvest_timer
{
    internal class UserSettings
    {
        public bool AfkEnabled { get; set; }
        public int AfkDelayMilliseconds { get; set; }
        public bool LockScreenEnabled { get; set; }
        public bool ResetDailyEnabled { get; set; }
        public int ResetDailyAtHour { get; set; }
        public bool AutoStart { get; set; }

        public UserSettings(Properties.Settings settings)
        {
            AfkEnabled = settings.AfkEnabled;
            LockScreenEnabled = settings.LockScreenEnabled;
            AfkDelayMilliseconds = settings.AfkDelayMilliseconds;
            ResetDailyEnabled = settings.ResetDailyEnabled;
            ResetDailyAtHour = settings.ResetDailyAtHour;
            AutoStart = settings.AutoStart;
        }
    }
}
