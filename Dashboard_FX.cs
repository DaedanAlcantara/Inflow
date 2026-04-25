using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Inflow
{
    public partial class Dashboard_FX : UserControl
    {
        private Panel loadingPanel;
        private bool isInitializing = true;
        private PictureBox[] stars;
        private int currentRating = 0;
        public Dashboard_FX()
        {
            this.DoubleBuffered = true;
            EnableDoubleBufferingForAllControls();
            InitializeComponent();
            InitializeStarRating();

            // Create loading panel
            CreateLoadingPanel();

            // Start loading in background
            System.Threading.Tasks.Task.Run(() => InitializeContent());

            this.Load += Dashboard_FX_Load;
        }

        private void CreateLoadingPanel()
        {
            loadingPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = true
            };

            var loadingLabel = new Label
            {
                Text = "Loading Dashboard...",
                Font = new Font("Segoe UI", 16F, FontStyle.Regular),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            loadingPanel.Controls.Add(loadingLabel);
            this.Controls.Add(loadingPanel);
            loadingPanel.BringToFront();
        }

        private void InitializeContent()
        {
            // Simulate loading time (remove in production)
            System.Threading.Thread.Sleep(100);

            // Update UI on main thread
            this.Invoke(new Action(() =>
            {
                // Set all panel colors at once
                StreakCounterPanel.BackColor = ColorTranslator.FromHtml("#FFFB96");
                FinishedCounter.BackColor = ColorTranslator.FromHtml("#AAE4FF");
                DroppedCounter.BackColor = ColorTranslator.FromHtml("#FFACBA");
                DateDisplayPanel.BackColor = ColorTranslator.FromHtml("#C96BFF");
                CurrentTaskDisplay.BackColor = ColorTranslator.FromHtml("#B38DFF");
                TimeDisplayPanel.BackColor = ColorTranslator.FromHtml("#FFBCF0");
                NextTaskDisplay.BackColor = ColorTranslator.FromHtml("#90B3FF");

                // Configure all controls
                ConfigureControls();

                // Remove loading panel and show content
                loadingPanel.Visible = false;
                this.Controls.Remove(loadingPanel);
                loadingPanel.Dispose();

                isInitializing = false;
                this.Refresh();
            }));
        }

        private void Dashboard_FX_Load(object sender, EventArgs e)
        {
            this.Resize += Dashboard_FX_Resize;
            ResizeContent();
        }

        private void ConfigureControls()
        {
            // IMPORTANT: Only set properties that WON'T be changed by ResizeContent()
            // Properties like Height, Width, Dock, etc. will be overwritten by ResizeContent()
            // Fix flowLayoutPanel14 - Current Task header with trash icon
            flowLayoutPanel14.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel14.WrapContents = false;

            // Configure label11 - don't let it take all the space
            label11.AutoSize = false;
            label11.Anchor = AnchorStyles.Left;  // Remove AnchorStyles.Right
            label11.TextAlign = ContentAlignment.MiddleLeft;
            label11.Width = flowLayoutPanel14.Width - 50; // Leave space for trash icon

            // Configure pictureBox3 (trash icon)
            pictureBox3.Visible = true;
            pictureBox3.Anchor = AnchorStyles.Right;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Width = 25;
            pictureBox3.Height = 25;
            pictureBox3.Cursor = Cursors.Hand; // Make it clickable


            // Greeting section
            label4.AutoSize = false;
            label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label4.TextAlign = ContentAlignment.MiddleLeft;

            NamePlaceholder.AutoSize = false;
            NamePlaceholder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NamePlaceholder.TextAlign = ContentAlignment.MiddleLeft;

            // Stats section - Streak
            label1.AutoSize = false;
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.TextAlign = ContentAlignment.MiddleLeft;

            flowLayoutPanel3.FlowDirection = FlowDirection.LeftToRight;
            pictureBox1.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.TextAlign = ContentAlignment.MiddleRight;

            // Stats section - Finished
            label5.AutoSize = false;
            label5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label5.TextAlign = ContentAlignment.MiddleLeft;

            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.TextAlign = ContentAlignment.MiddleRight;

            // Stats section - Dropped
            label7.AutoSize = false;
            label7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label7.TextAlign = ContentAlignment.MiddleLeft;

            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.TextAlign = ContentAlignment.MiddleRight;

            // Date panel
            MonthText.AutoSize = false;
            MonthText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            MonthText.TextAlign = ContentAlignment.MiddleLeft;

            DayText.AutoSize = false;
            DayText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            DayText.TextAlign = ContentAlignment.MiddleLeft;

            YearText.AutoSize = false;
            YearText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            YearText.TextAlign = ContentAlignment.MiddleLeft;

            // Current task panel
            label11.AutoSize = false;
            label11.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label11.TextAlign = ContentAlignment.MiddleLeft;

            NameTaskText.AutoSize = false;
            NameTaskText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NameTaskText.TextAlign = ContentAlignment.MiddleCenter;

            DecriptionText.AutoSize = false;
            DecriptionText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            DecriptionText.TextAlign = ContentAlignment.MiddleCenter;

            // Time panel
            Timetext.AutoSize = false;
            Timetext.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Timetext.TextAlign = ContentAlignment.MiddleCenter;

            // Next task panel
            label10.AutoSize = false;
            label10.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label10.TextAlign = ContentAlignment.MiddleLeft;

            NameNextTaskText.AutoSize = false;
            NameNextTaskText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NameNextTaskText.TextAlign = ContentAlignment.MiddleCenter;

            // Main containers - ONLY set properties that ResizeContent doesn't change
            flowLayoutPanel6.WrapContents = false;
            flowLayoutPanel6.Margin = new Padding(0);
            flowLayoutPanel6.Padding = new Padding(0);

            flowLayoutPanel7.WrapContents = false;
            flowLayoutPanel7.Margin = new Padding(0);
            flowLayoutPanel7.Padding = new Padding(0);

            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Padding = new Padding(0);

            flowLayoutPanel10.WrapContents = false;
            flowLayoutPanel10.Margin = new Padding(0);
            flowLayoutPanel10.Padding = new Padding(0);

            flowLayoutPanel18.WrapContents = false;
            flowLayoutPanel18.Margin = new Padding(0);
            flowLayoutPanel18.Padding = new Padding(0);

            // Set correct control order (this is safe - ResizeContent doesn't change order)
            if (flowLayoutPanel1.Controls.Count >= 3)
            {
                flowLayoutPanel1.Controls.SetChildIndex(StreakCounterPanel, 0);
                flowLayoutPanel1.Controls.SetChildIndex(FinishedCounter, 1);
                flowLayoutPanel1.Controls.SetChildIndex(DroppedCounter, 2);
            }

            if (flowLayoutPanel10.Controls.Count >= 2)
            {
                flowLayoutPanel10.Controls.SetChildIndex(DateDisplayPanel, 0);
                flowLayoutPanel10.Controls.SetChildIndex(CurrentTaskDisplay, 1);
            }

            if (flowLayoutPanel18.Controls.Count >= 2)
            {
                flowLayoutPanel18.Controls.SetChildIndex(TimeDisplayPanel, 0);
                flowLayoutPanel18.Controls.SetChildIndex(NextTaskDisplay, 1);
            }
        }

        private void EnableDoubleBufferingForAllControls()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.DoubleBuffer |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            EnableDoubleBufferingRecursive(this);
        }

        private void EnableDoubleBufferingRecursive(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Panel || control is FlowLayoutPanel)
                {
                    typeof(Control).InvokeMember("DoubleBuffered",
                        System.Reflection.BindingFlags.SetProperty |
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic,
                        null, control, new object[] { true });
                }

                if (control.HasChildren)
                {
                    EnableDoubleBufferingRecursive(control);
                }
            }
        }
        private void InitializeStarRating()
        {
            // Collect all star PictureBoxes into an array (ordered correctly)
            stars = new PictureBox[]
            {
        star1,  // 1st star
        star2,  // 2nd star
        star3,  // 3rd star
        star4,  // 4th star
        star5   // 5th star
            };

            // Optionally disable clicking if you want to ensure no user interaction
            foreach (var star in stars)
            {
                star.Enabled = false;  // Makes stars non-interactive
                star.Cursor = Cursors.Default;  // Normal cursor instead of hand
                star.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        /// <summary>
        /// Updates the star rating display (1-5 stars)
        /// </summary>
        /// <param name="rating">Rating value from 0 to 5</param>
        public void SetTaskRating(int rating)
        {
            if (rating < 0) rating = 0;
            if (rating > 5) rating = 5;

            currentRating = rating;

            for (int i = 0; i < stars.Length; i++)
            {
                if (i < rating)
                {
                    stars[i].Image = Properties.Resources.Rating;
                    stars[i].Enabled = true;
                }
                else
                {
                    stars[i].Image = Properties.Resources.Rating;
                    // Make empty stars semi-transparent
                    stars[i].Enabled = false;
                }
            }
        }

        /// <summary>
        /// Creates a dimmed/grayscale version of an image for empty stars
        /// </summary>
        private Image CreateDimmedImage(Image original)
        {
            Bitmap dimmed = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(dimmed))
            {
                var colorMatrix = new System.Drawing.Imaging.ColorMatrix(
                    new float[][]
                    {
                new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                new float[] {0, 0, 0, 0.5f, 0},
                new float[] {0, 0, 0, 0, 1}
                    });

                using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                        0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return dimmed;
        }

        public void UpdateUserName(string userName)
        {
            if (!string.IsNullOrEmpty(userName) && NamePlaceholder != null)
            {
                NamePlaceholder.Text = userName;
            }
        }

        private void Dashboard_FX_Resize(object sender, EventArgs e)
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

                int availableTotalHeight = this.ClientSize.Height - (topBottomMargin * 2);
                if (availableTotalHeight < 400) availableTotalHeight = 400;

                float greetingSectionRatio = 0.20f;
                float statsSectionRatio = 0.25f;
                float currentTaskSectionRatio = 0.35f;
                float timeSectionRatio = 0.20f;

                int greetingTotalHeight = (int)(availableTotalHeight * greetingSectionRatio);
                int statsHeight = (int)(availableTotalHeight * statsSectionRatio);
                int currentTaskHeight = (int)(availableTotalHeight * currentTaskSectionRatio);
                int timeHeight = (int)(availableTotalHeight * timeSectionRatio);

                int greetingTopHeight = Math.Max(40, greetingTotalHeight / 3);
                int greetingBottomHeight = Math.Max(40, greetingTotalHeight - greetingTopHeight);

                flowLayoutPanel6.Height = greetingTopHeight;
                flowLayoutPanel7.Height = greetingBottomHeight;
                flowLayoutPanel1.Height = Math.Max(100, statsHeight);
                flowLayoutPanel10.Height = Math.Max(120, currentTaskHeight);
                flowLayoutPanel18.Height = Math.Max(80, timeHeight);

                int fullWidth = this.ClientSize.Width;
                flowLayoutPanel6.Width = fullWidth;
                flowLayoutPanel7.Width = fullWidth;
                flowLayoutPanel1.Width = fullWidth;
                flowLayoutPanel10.Width = fullWidth;
                flowLayoutPanel18.Width = fullWidth;

                int gapsBetweenPanels1 = panelMargin * 2;
                int gapsBetweenPanels2 = panelMargin * 1;

                int availableWidth1 = flowLayoutPanel1.ClientSize.Width - gapsBetweenPanels1;
                int availableWidth2 = flowLayoutPanel10.ClientSize.Width - gapsBetweenPanels2;

                if (availableWidth1 <= 0) return;

                int availableHeight1 = flowLayoutPanel1.ClientSize.Height - (panelMargin * 2);
                int panelWidth1 = availableWidth1 / 3;
                int panelHeight1 = availableHeight1;

                int availableHeight2 = flowLayoutPanel10.ClientSize.Height - (panelMargin * 2);
                int panelHeight2 = availableHeight2;
                int panelWidth2 = availableWidth2 / 2;

                int availableHeight3 = flowLayoutPanel18.ClientSize.Height - (panelMargin * 2);
                int panelHeight3 = availableHeight3;
                int panelWidth3 = availableWidth2 / 2;

                Control[] panels1 = { StreakCounterPanel, FinishedCounter, DroppedCounter };
                Control[] panels2 = { DateDisplayPanel, CurrentTaskDisplay };
                Control[] panels3 = { TimeDisplayPanel, NextTaskDisplay };



                ApplyStatsPanelSizes(panels1, panelWidth1, panelHeight1, panelMargin);
                ApplyTwoPanelSizesEven(panels2, panelWidth2, panelHeight2, panelMargin);
                ApplyTwoPanelSizesEven(panels3, panelWidth3, panelHeight3, panelMargin);

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

        private void SetLabelWidths()
        {
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

            if (Timetext != null && flowLayoutPanel21 != null)
            {
                Timetext.Width = flowLayoutPanel21.ClientSize.Width - 20;
                Timetext.Height = flowLayoutPanel21.ClientSize.Height;
            }

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
            if (label2 != null)
            {
                float streakFontSize = Math.Max(12, Math.Min(28, statsHeight / 2.5f));
                label2.Font = new Font("Segoe UI", streakFontSize, FontStyle.Bold);
            }

            if (label3 != null)
            {
                float finishedFontSize = Math.Max(12, Math.Min(28, statsHeight / 2.5f));
                label3.Font = new Font("Segoe UI", finishedFontSize, FontStyle.Bold);
            }

            if (label6 != null)
            {
                float droppedFontSize = Math.Max(12, Math.Min(28, statsHeight / 2.5f));
                label6.Font = new Font("Segoe UI", droppedFontSize, FontStyle.Bold);
            }

            if (DayText != null)
            {
                float dayFontSize = Math.Max(16, Math.Min(36, taskHeight / 2.5f));
                DayText.Font = new Font("Segoe UI", dayFontSize, FontStyle.Bold);
            }

            if (Timetext != null)
            {
                float timeFontSize = Math.Max(16, Math.Min(48, timeHeight / 2f));
                Timetext.Font = new Font("Segoe UI", timeFontSize, FontStyle.Bold);
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}