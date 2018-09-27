namespace harvest_timer
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAfk = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.numAfkDelay = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkAutoStart = new System.Windows.Forms.CheckBox();
            this.chkLockScreen = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkResetDaily = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numResetDailyAt = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tlpContent.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAfkDelay)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numResetDailyAt)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpContent
            // 
            this.tlpContent.ColumnCount = 2;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 214F));
            this.tlpContent.Controls.Add(this.label1, 0, 3);
            this.tlpContent.Controls.Add(this.chkAfk, 1, 3);
            this.tlpContent.Controls.Add(this.flowLayoutPanel1, 1, 4);
            this.tlpContent.Controls.Add(this.label3, 0, 2);
            this.tlpContent.Controls.Add(this.label4, 0, 0);
            this.tlpContent.Controls.Add(this.chkAutoStart, 1, 0);
            this.tlpContent.Controls.Add(this.chkLockScreen, 1, 2);
            this.tlpContent.Controls.Add(this.label5, 0, 1);
            this.tlpContent.Controls.Add(this.flowLayoutPanel2, 1, 1);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(0, 0);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.Padding = new System.Windows.Forms.Padding(15);
            this.tlpContent.RowCount = 7;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpContent.Size = new System.Drawing.Size(384, 161);
            this.tlpContent.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(18, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Away From Keyboard";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAfk
            // 
            this.chkAfk.AutoSize = true;
            this.chkAfk.Location = new System.Drawing.Point(158, 96);
            this.chkAfk.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.chkAfk.Name = "chkAfk";
            this.chkAfk.Size = new System.Drawing.Size(131, 16);
            this.chkAfk.TabIndex = 1;
            this.chkAfk.Text = "Detects when I\'m AFK";
            this.chkAfk.UseVisualStyleBackColor = true;
            this.chkAfk.CheckedChanged += new System.EventHandler(this.chkAfk_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.numAfkDelay);
            this.flowLayoutPanel1.Controls.Add(this.label8);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(155, 115);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(214, 25);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Delay";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numAfkDelay
            // 
            this.numAfkDelay.Location = new System.Drawing.Point(40, 3);
            this.numAfkDelay.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numAfkDelay.Name = "numAfkDelay";
            this.numAfkDelay.Size = new System.Drawing.Size(61, 20);
            this.numAfkDelay.TabIndex = 1;
            this.numAfkDelay.ValueChanged += new System.EventHandler(this.numAfkDelay_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(107, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 26);
            this.label8.TabIndex = 3;
            this.label8.Text = "minutes";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(18, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Lock Screen";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(18, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Auto Start Timer";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAutoStart
            // 
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Location = new System.Drawing.Point(158, 21);
            this.chkAutoStart.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new System.Drawing.Size(15, 14);
            this.chkAutoStart.TabIndex = 1;
            this.chkAutoStart.UseVisualStyleBackColor = true;
            this.chkAutoStart.CheckedChanged += new System.EventHandler(this.chkAutoStart_CheckedChanged);
            // 
            // chkLockScreen
            // 
            this.chkLockScreen.AutoSize = true;
            this.chkLockScreen.Location = new System.Drawing.Point(158, 71);
            this.chkLockScreen.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.chkLockScreen.Name = "chkLockScreen";
            this.chkLockScreen.Size = new System.Drawing.Size(191, 16);
            this.chkLockScreen.TabIndex = 1;
            this.chkLockScreen.Text = "Detects when my session is locked";
            this.chkLockScreen.UseVisualStyleBackColor = true;
            this.chkLockScreen.CheckedChanged += new System.EventHandler(this.chkLockScreen_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(18, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Reset Timer Daily";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkResetDaily);
            this.flowLayoutPanel2.Controls.Add(this.label6);
            this.flowLayoutPanel2.Controls.Add(this.numResetDailyAt);
            this.flowLayoutPanel2.Controls.Add(this.label7);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(155, 40);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(214, 25);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // chkResetDaily
            // 
            this.chkResetDaily.AutoSize = true;
            this.chkResetDaily.Location = new System.Drawing.Point(3, 6);
            this.chkResetDaily.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.chkResetDaily.Name = "chkResetDaily";
            this.chkResetDaily.Size = new System.Drawing.Size(15, 14);
            this.chkResetDaily.TabIndex = 1;
            this.chkResetDaily.UseVisualStyleBackColor = true;
            this.chkResetDaily.CheckedChanged += new System.EventHandler(this.chkResetDaily_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(21, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 26);
            this.label6.TabIndex = 0;
            this.label6.Text = "At";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numResetDailyAt
            // 
            this.numResetDailyAt.Location = new System.Drawing.Point(44, 3);
            this.numResetDailyAt.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numResetDailyAt.Name = "numResetDailyAt";
            this.numResetDailyAt.Size = new System.Drawing.Size(56, 20);
            this.numResetDailyAt.TabIndex = 1;
            this.numResetDailyAt.ValueChanged += new System.EventHandler(this.numResetDailyAt_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(106, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 26);
            this.label7.TabIndex = 2;
            this.label7.Text = "h";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.tlpContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "frmSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Harvest Timer Settings";
            this.TopMost = true;
            this.tlpContent.ResumeLayout(false);
            this.tlpContent.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAfkDelay)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numResetDailyAt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAfk;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAutoStart;
        private System.Windows.Forms.NumericUpDown numAfkDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkLockScreen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkResetDaily;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numResetDailyAt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

