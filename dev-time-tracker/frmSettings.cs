using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DevTimeTracker
{
    public partial class frmSettings : Form
    {
        private static Dictionary<string, string> _timeFormatOptions;

        public frmSettings()
        {
            InitializeComponent();

            _timeFormatOptions = new Dictionary<string, string>
            {
                { "H:mm:ss", "13:37:00" },
                { "H'h'mm'm'ss's'", "13h37m00s" },
                { "H:mm", "13:37" },
                { "H'h'mm", "13h37" }
            };

            cbTimeFormat.Items.AddRange(_timeFormatOptions.Select(x => x.Value).ToArray());

            chkAutoStart.Checked = Properties.Settings.Default.AutoStart;

            chkResetDaily.Checked = Properties.Settings.Default.ResetDailyEnabled;
            numResetDailyAt.Value = Properties.Settings.Default.ResetDailyAtHour;
            numResetDailyAt.Enabled = chkResetDaily.Checked;

            cbTimeFormat.SelectedItem = _timeFormatOptions.FirstOrDefault(x => x.Key == Properties.Settings.Default.TimeFormat).Value;

            chkLockScreen.Checked = Properties.Settings.Default.LockScreenEnabled;

            chkAfk.Checked = Properties.Settings.Default.AfkEnabled;
            numAfkDelay.Value = Math.Floor(Properties.Settings.Default.AfkDelayMilliseconds / 60000M);
            numAfkDelay.Enabled = chkAfk.Checked;
        }

        internal void SaveLastShift(DateTime time)
        {
            Properties.Settings.Default.LastShiftTicks = time.Ticks;
            Properties.Settings.Default.Save();
        }

        private void chkResetDaily_CheckedChanged(object sender, EventArgs e)
        {
            numResetDailyAt.Enabled = chkResetDaily.Checked;
            Properties.Settings.Default.ResetDailyEnabled = chkResetDaily.Checked;
            Properties.Settings.Default.Save();
        }

        private void chkAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoStart = chkAutoStart.Checked;
            Properties.Settings.Default.Save();
        }

        private void chkLockScreen_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LockScreenEnabled = chkLockScreen.Checked;
            Properties.Settings.Default.Save();
        }

        private void chkAfk_CheckedChanged(object sender, EventArgs e)
        {
            numAfkDelay.Enabled = chkAfk.Checked;
            Properties.Settings.Default.AfkEnabled = chkAfk.Checked;
            Properties.Settings.Default.Save();
        }

        private void numAfkDelay_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AfkDelayMilliseconds = (int)(numAfkDelay.Value * 60000);
            Properties.Settings.Default.Save();
        }

        private void cbTimeFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TimeFormat = _timeFormatOptions.FirstOrDefault(x => x.Value == cbTimeFormat.SelectedItem?.ToString()).Key;
            Properties.Settings.Default.Save();
        }

        private void numResetDailyAt_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ResetDailyAtHour = (int)numResetDailyAt.Value;
            Properties.Settings.Default.Save();
        }
    }
}
