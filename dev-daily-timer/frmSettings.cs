﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace harvest_timer
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();

            chkAutoStart.Checked = Properties.Settings.Default.AutoStart;

            chkResetDaily.Checked = Properties.Settings.Default.ResetDailyEnabled;
            numResetDailyAt.Value = Properties.Settings.Default.ResetDailyAtHour;
            numResetDailyAt.Enabled = chkResetDaily.Checked;

            chkLockScreen.Checked = Properties.Settings.Default.LockScreenEnabled;

            chkAfk.Checked = Properties.Settings.Default.AfkEnabled;
            numAfkDelay.Value = Math.Floor(Properties.Settings.Default.AfkDelayMilliseconds / 60000M);
            numAfkDelay.Enabled = chkAfk.Checked;
        }

        private void chkResetDaily_CheckedChanged(object sender, EventArgs e)
        {
            numResetDailyAt.Enabled = chkResetDaily.Checked;
            Properties.Settings.Default.ResetDailyEnabled = chkResetDaily.Checked;
        }

        private void chkAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoStart = chkAutoStart.Checked;
        }

        private void numResetDailyAt_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ResetDailyAtHour = (int)numResetDailyAt.Value;
        }

        private void chkLockScreen_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LockScreenEnabled = chkLockScreen.Checked;
        }

        private void chkAfk_CheckedChanged(object sender, EventArgs e)
        {
            numAfkDelay.Enabled = chkAfk.Checked;
            Properties.Settings.Default.AfkEnabled = chkAfk.Checked;
        }

        private void numAfkDelay_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AfkDelayMilliseconds = (int)(numAfkDelay.Value * 60000);
        }
    }
}
