using System;
using System.Linq;
using System.Windows.Forms;

namespace Inflow
{
    public partial class Dashboard_FX : UserControl
    {
        private System.Windows.Forms.Timer timeTimer;
        public Dashboard_FX()
        {
            InitializeComponent();

            // Configure after initialization
            this.Load += Dashboard_FX_Load;
        }

        private void Dashboard_FX_Load(object sender, EventArgs e)
        {
            // Set panel colors
            StreakCounterPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFB96");
            FinishedCounter.BackColor = System.Drawing.ColorTranslator.FromHtml("#AAE4FF");
            DroppedCounter.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFACBA");
            DateDisplayPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#C96BFF");
            CurrentTaskDisplay.BackColor = System.Drawing.ColorTranslator.FromHtml("#B38DFF");
            TimeDisplayPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBCF0");
            NextTaskDisplay.BackColor = System.Drawing.ColorTranslator.FromHtml("#90B3FF");

            DateTime now = DateTime.Now;
            MonthText.Text = now.ToString("MMMM");
            DayText.Text = now.ToString("dd");
            YearText.Text = now.ToString("yyyy");

            UpdateCurrentTime();

            timeTimer = new System.Windows.Forms.Timer();
            timeTimer.Interval = 1000; // 1 second
            timeTimer.Tick += TimeTimer_Tick;
            timeTimer.Start();

            // Configure all controls properly
            ConfigureAllControls();

            // Configure main containers
            ConfigureMainContainers();

            this.Resize += Dashboard_FX_Resize;
            ResizeContent();
        }

        private void ConfigureAllControls()
        {
            // Greeting section - use Anchor instead of Dock
            label4.AutoSize = false;
            label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label4.TextAlign = ContentAlignment.MiddleLeft;

            NamePlaceholder.AutoSize = false;
            NamePlaceholder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NamePlaceholder.TextAlign = ContentAlignment.MiddleLeft;

            // Stats section - Streak
            flowLayoutPanel2.Height = 40;
            flowLayoutPanel2.Dock = DockStyle.Top;

            label1.AutoSize = false;
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.TextAlign = ContentAlignment.MiddleCenter;

            flowLayoutPanel3.Dock = DockStyle.Fill;
            flowLayoutPanel3.FlowDirection = FlowDirection.LeftToRight;

            pictureBox1.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            label2.AutoSize = false;
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.TextAlign = ContentAlignment.MiddleCenter;

            // Stats section - Finished
            flowLayoutPanel5.Height = 40;
            flowLayoutPanel5.Dock = DockStyle.Top;

            label5.AutoSize = false;
            label5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label5.TextAlign = ContentAlignment.MiddleCenter;

            flowLayoutPanel4.Dock = DockStyle.Fill;

            label3.AutoSize = false;
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.TextAlign = ContentAlignment.MiddleCenter;

            // Stats section - Dropped
            flowLayoutPanel9.Height = 40;
            flowLayoutPanel9.Dock = DockStyle.Top;

            label7.AutoSize = false;
            label7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label7.TextAlign = ContentAlignment.MiddleCenter;

            flowLayoutPanel8.Dock = DockStyle.Fill;

            label6.AutoSize = false;
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.TextAlign = ContentAlignment.MiddleCenter;

            // Date panel
            flowLayoutPanel12.Height = 45;
            flowLayoutPanel12.Dock = DockStyle.Top;

            MonthText.AutoSize = false;
            MonthText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            MonthText.TextAlign = ContentAlignment.MiddleCenter;

            flowLayoutPanel11.Dock = DockStyle.Fill;

            DayText.AutoSize = false;
            DayText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            DayText.TextAlign = ContentAlignment.MiddleCenter;

            flowLayoutPanel15.Height = 35;
            flowLayoutPanel15.Dock = DockStyle.Bottom;

            YearText.AutoSize = false;
            YearText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            YearText.TextAlign = ContentAlignment.MiddleCenter;

            // Current task panel
            flowLayoutPanel14.Height = 40;
            flowLayoutPanel14.Dock = DockStyle.Bottom;

            label11.AutoSize = false;
            label11.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label11.TextAlign = ContentAlignment.MiddleCenter;

            flowLayoutPanel13.Dock = DockStyle.Fill;

            NameTaskText.AutoSize = false;
            NameTaskText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NameTaskText.TextAlign = ContentAlignment.MiddleCenter;

            flowLayoutPanel16.Height = 30;
            flowLayoutPanel16.Dock = DockStyle.Bottom;

            DecriptionText.AutoSize = false;
            DecriptionText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            DecriptionText.TextAlign = ContentAlignment.MiddleCenter;

            // Time panel
            flowLayoutPanel21.Dock = DockStyle.Fill;

            Timetext.AutoSize = false;
            Timetext.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Timetext.TextAlign = ContentAlignment.MiddleCenter;

            // Next task panel
            flowLayoutPanel23.Height = 40;
            flowLayoutPanel23.Dock = DockStyle.Top;

            label10.AutoSize = false;
            label10.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label10.TextAlign = ContentAlignment.MiddleCenter;

            flowLayoutPanel22.Dock = DockStyle.Fill;

            NameNextTaskText.AutoSize = false;
            NameNextTaskText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NameNextTaskText.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void ConfigureMainContainers()
        {
            flowLayoutPanel6.Dock = DockStyle.Top;
            flowLayoutPanel6.WrapContents = false;
            flowLayoutPanel6.Margin = new Padding(0);
            flowLayoutPanel6.Padding = new Padding(0);

            flowLayoutPanel7.Dock = DockStyle.Top;
            flowLayoutPanel7.WrapContents = false;
            flowLayoutPanel7.Margin = new Padding(0);
            flowLayoutPanel7.Padding = new Padding(0);

            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoSize = false;
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Padding = new Padding(0);

            flowLayoutPanel10.WrapContents = false;
            flowLayoutPanel10.AutoSize = false;
            flowLayoutPanel10.Dock = DockStyle.Top;
            flowLayoutPanel10.Margin = new Padding(0);
            flowLayoutPanel10.Padding = new Padding(0);

            flowLayoutPanel18.WrapContents = false;
            flowLayoutPanel18.AutoSize = false;
            flowLayoutPanel18.Dock = DockStyle.Top;
            flowLayoutPanel18.Margin = new Padding(0);
            flowLayoutPanel18.Padding = new Padding(0);
        }





        public void UpdateUserName(string userName)
        {
            if (!string.IsNullOrEmpty(userName) && NamePlaceholder != null)
            {
                NamePlaceholder.Text = userName;
            }
        }

        private User_BX currentUser;

        internal void SetUser(User_BX user)
        {
            if (user != null && NamePlaceholder != null)
            {
                currentUser = user;
                NamePlaceholder.Text = user.Username;
            }
        }

        private void Dashboard_FX_Resize(object sender, EventArgs e)
        {
            ResizeContent();
        }

        private void MainContent_Resize(object sender, EventArgs e)
        {
            ResizeContent();
        }

        public void ResizeContent()
        {
            if (flowLayoutPanel1 == null || this.IsDisposed) return;

            try
            {
                flowLayoutPanel1.SuspendLayout();
                flowLayoutPanel10.SuspendLayout();
                flowLayoutPanel18.SuspendLayout();

                int panelMargin = 6;
                int topBottomMargin = 10;

                // Calculate total available height
                int availableTotalHeight = this.ClientSize.Height - (topBottomMargin * 2);
                if (availableTotalHeight < 400) availableTotalHeight = 400;

                // Height proportions
                float greetingSectionRatio = 0.20f;
                float statsSectionRatio = 0.25f;
                float currentTaskSectionRatio = 0.35f;
                float timeSectionRatio = 0.20f;

                // Calculate heights
                int greetingTotalHeight = (int)(availableTotalHeight * greetingSectionRatio);
                int statsHeight = (int)(availableTotalHeight * statsSectionRatio);
                int currentTaskHeight = (int)(availableTotalHeight * currentTaskSectionRatio);
                int timeHeight = (int)(availableTotalHeight * timeSectionRatio);

                // Split greeting section
                int greetingTopHeight = Math.Max(40, greetingTotalHeight / 3);
                int greetingBottomHeight = Math.Max(40, greetingTotalHeight - greetingTopHeight);

                // Set heights
                flowLayoutPanel6.Height = greetingTopHeight;
                flowLayoutPanel7.Height = greetingBottomHeight;
                flowLayoutPanel1.Height = Math.Max(100, statsHeight);
                flowLayoutPanel10.Height = Math.Max(120, currentTaskHeight);
                flowLayoutPanel18.Height = Math.Max(80, timeHeight);

                // Set widths
                int fullWidth = this.ClientSize.Width;
                flowLayoutPanel6.Width = fullWidth;
                flowLayoutPanel7.Width = fullWidth;
                flowLayoutPanel1.Width = fullWidth;
                flowLayoutPanel10.Width = fullWidth;
                flowLayoutPanel18.Width = fullWidth;

                // Calculate internal panel sizes with EVEN distribution
                int gapsBetweenPanels1 = panelMargin * 2;
                int gapsBetweenPanels2 = panelMargin * 1;

                int availableWidth1 = flowLayoutPanel1.ClientSize.Width - gapsBetweenPanels1;
                int availableWidth2 = flowLayoutPanel10.ClientSize.Width - gapsBetweenPanels2;

                if (availableWidth1 <= 0) return;

                // Calculate panel dimensions - USE EVEN DISTRIBUTION
                int availableHeight1 = flowLayoutPanel1.ClientSize.Height - (panelMargin * 2);

                // FOR STATS PANELS: Split evenly 3 ways (33.3% each)
                int panelWidth1 = availableWidth1 / 3;
                int panelHeight1 = availableHeight1;

                // FOR 2-PANEL SECTIONS: Use 40/60 or 50/50 distribution instead of 33/67
                int availableHeight2 = flowLayoutPanel10.ClientSize.Height - (panelMargin * 2);
                int panelHeight2 = availableHeight2;

                // Option A: 50/50 split (even)
                int panelWidth2 = availableWidth2 / 2;

                // Option B: 40/60 split (if you want date slightly smaller)
                // int datePanelWidth = availableWidth2 * 40 / 100;
                // int taskPanelWidth = availableWidth2 * 60 / 100;

                int availableHeight3 = flowLayoutPanel18.ClientSize.Height - (panelMargin * 2);
                int panelHeight3 = availableHeight3;
                int panelWidth3 = availableWidth2 / 2; // Even split for time section

                Control[] panels1 = { StreakCounterPanel, FinishedCounter, DroppedCounter };
                Control[] panels2 = { DateDisplayPanel, CurrentTaskDisplay };
                Control[] panels3 = { TimeDisplayPanel, NextTaskDisplay };

                // Update font sizes dynamically
                UpdateDynamicFontSizes(panelHeight1, panelHeight2, panelHeight3);

                // Apply sizes to all panels with EVEN distribution
                ApplyStatsPanelSizes(panels1, panelWidth1, panelHeight1, panelMargin);
                ApplyTwoPanelSizesEven(panels2, panelWidth2, panelHeight2, panelMargin);
                ApplyTwoPanelSizesEven(panels3, panelWidth3, panelHeight3, panelMargin);

                // Set label widths
                SetLabelWidths();
            }
            finally
            {
                flowLayoutPanel1.ResumeLayout();
                flowLayoutPanel18.ResumeLayout();
                flowLayoutPanel10.ResumeLayout();
            }
        }

        private void ApplyTwoPanelSizesEven(Control[] panels, int width, int height, int margin)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                var panel = panels[i];
                if (panel != null && !panel.IsDisposed)
                {
                    // Give each panel the same width (even distribution)
                    panel.Width = width;

                    if (i == panels.Length - 1)
                    {
                        panel.Margin = new Padding(margin / 2, margin / 2, 0, margin / 2);
                    }
                    else
                    {
                        panel.Margin = new Padding(margin / 2, margin / 2, margin / 2, margin / 2);
                    }

                    panel.Height = height;
                }
            }
        }

        private void SetLabelWidths()
        {
            // Greeting labels - subtract padding
            if (label4 != null && flowLayoutPanel6 != null)
            {
                label4.Width = flowLayoutPanel6.ClientSize.Width - 30;
                label4.Height = flowLayoutPanel6.ClientSize.Height - 10;
            }

            if (NamePlaceholder != null && flowLayoutPanel7 != null)
            {
                NamePlaceholder.Width = flowLayoutPanel7.ClientSize.Width - 30;
                NamePlaceholder.Height = flowLayoutPanel7.ClientSize.Height - 10;
            }

            // Stats labels - Streak
            if (label1 != null && flowLayoutPanel2 != null)
            {
                label1.Width = flowLayoutPanel2.ClientSize.Width - 20;
                label1.Height = flowLayoutPanel2.ClientSize.Height;
            }

            if (label2 != null && flowLayoutPanel3 != null)
            {
                label2.Width = flowLayoutPanel3.ClientSize.Width - pictureBox1.Width - 40;
                label2.Height = flowLayoutPanel3.ClientSize.Height - 20;
            }

            // Stats labels - Finished
            if (label5 != null && flowLayoutPanel5 != null)
            {
                label5.Width = flowLayoutPanel5.ClientSize.Width - 20;
                label5.Height = flowLayoutPanel5.ClientSize.Height;
            }

            if (label3 != null && flowLayoutPanel4 != null)
            {
                label3.Width = flowLayoutPanel4.ClientSize.Width - 20;
                label3.Height = flowLayoutPanel4.ClientSize.Height - 20;
            }

            // Stats labels - Dropped
            if (label7 != null && flowLayoutPanel9 != null)
            {
                label7.Width = flowLayoutPanel9.ClientSize.Width - 20;
                label7.Height = flowLayoutPanel9.ClientSize.Height;
            }

            if (label6 != null && flowLayoutPanel8 != null)
            {
                label6.Width = flowLayoutPanel8.ClientSize.Width - 20;
                label6.Height = flowLayoutPanel8.ClientSize.Height - 20;
            }

            // Date labels
            if (MonthText != null && flowLayoutPanel12 != null)
            {
                MonthText.Width = flowLayoutPanel12.ClientSize.Width - 20;
                MonthText.Height = flowLayoutPanel12.ClientSize.Height;
            }

            if (DayText != null && flowLayoutPanel11 != null)
            {
                DayText.Width = flowLayoutPanel11.ClientSize.Width - 20;
                DayText.Height = flowLayoutPanel11.ClientSize.Height;
            }

            if (YearText != null && flowLayoutPanel15 != null)
            {
                YearText.Width = flowLayoutPanel15.ClientSize.Width - 20;
                YearText.Height = flowLayoutPanel15.ClientSize.Height;
            }

            // Current task labels
            if (label11 != null && flowLayoutPanel14 != null)
            {
                label11.Width = flowLayoutPanel14.ClientSize.Width - 20;
                label11.Height = flowLayoutPanel14.ClientSize.Height;
            }

            if (NameTaskText != null && flowLayoutPanel13 != null)
            {
                NameTaskText.Width = flowLayoutPanel13.ClientSize.Width - 20;
                NameTaskText.Height = flowLayoutPanel13.ClientSize.Height;
            }

            if (DecriptionText != null && flowLayoutPanel16 != null)
            {
                DecriptionText.Width = flowLayoutPanel16.ClientSize.Width - 20;
                DecriptionText.Height = flowLayoutPanel16.ClientSize.Height;
            }

            // Time label
            if (Timetext != null && flowLayoutPanel21 != null)
            {
                Timetext.Width = flowLayoutPanel21.ClientSize.Width - 20;
                Timetext.Height = flowLayoutPanel21.ClientSize.Height;
            }

            // Next task labels
            if (label10 != null && flowLayoutPanel23 != null)
            {
                label10.Width = flowLayoutPanel23.ClientSize.Width - 20;
                label10.Height = flowLayoutPanel23.ClientSize.Height;
            }

            if (NameNextTaskText != null && flowLayoutPanel22 != null)
            {
                NameNextTaskText.Width = flowLayoutPanel22.ClientSize.Width - 20;
                NameNextTaskText.Height = flowLayoutPanel22.ClientSize.Height;
            }
        }
        private void UpdateDynamicFontSizes(int statsHeight, int taskHeight, int timeHeight)
        {
            // Update streak counter font
            if (label2 != null)
            {
                float streakFontSize = Math.Max(12, Math.Min(28, statsHeight / 2.5f));
                label2.Font = new Font("Inter", streakFontSize, FontStyle.Bold);
            }

            // Update finished counter font
            if (label3 != null)
            {
                float finishedFontSize = Math.Max(12, Math.Min(28, statsHeight / 2.5f));
                label3.Font = new Font("Inter", finishedFontSize, FontStyle.Bold);
            }

            // Update dropped counter font
            if (label6 != null)
            {
                float droppedFontSize = Math.Max(12, Math.Min(28, statsHeight / 2.5f));
                label6.Font = new Font("Inter", droppedFontSize, FontStyle.Bold);
            }

            // Update date font
            if (DayText != null)
            {
                float dayFontSize = Math.Max(16, Math.Min(36, taskHeight / 2.5f));
                DayText.Font = new Font("Inter", dayFontSize, FontStyle.Bold);
            }

            // Update time font
            if (Timetext != null)
            {
                float timeFontSize = Math.Max(16, Math.Min(48, timeHeight / 2f));
                Timetext.Font = new Font("Inter", timeFontSize, FontStyle.Bold);
            }
        }

        private void ApplyStatsPanelSizes(Control[] panels, int width, int height, int margin)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                var panel = panels[i];
                if (panel != null && !panel.IsDisposed)
                {
                    panel.Width = width;
                    panel.Height = height;
                    if (i == panels.Length - 1)
                    {
                        panel.Margin = new Padding(margin / 2, margin / 2, 0, margin / 2);
                    }
                    else
                    {
                        panel.Margin = new Padding(margin / 2, margin / 2, margin / 2, margin / 2);
                    }
                }
            }
        }

        private void UpdateCurrentTime()
        {
            if (Timetext != null && !Timetext.IsDisposed)
            {
                Timetext.Text = DateTime.Now.ToString("hh:mm:ss tt");
            }
        }
        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            UpdateCurrentTime();
        }

        private void NamePlaceholder_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void DayText_Click(object sender, EventArgs e)
        {

        }

        private void MonthText_Click(object sender, EventArgs e)
        {

        }

        private void YearText_Click(object sender, EventArgs e)
        {

        }

        private void NameTaskText_Click(object sender, EventArgs e)
        {

        }

        private void DecriptionText_Click(object sender, EventArgs e)
        {

        }

        private void Timetext_Click(object sender, EventArgs e)
        {

        }

        private void NameNextTaskText_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}