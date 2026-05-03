using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Inflow
{
    public partial class Dashboard_FX : UserControl
    {
        // ── Timers ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Timer timeTimer;
        private System.Windows.Forms.Timer _resizeDebounceTimer;

        // ── Loading ───────────────────────────────────────────────────────────
        private Panel loadingPanel;
        private bool isInitializing = true;
        private bool _isInitialized = false;

        // ── Star rating ───────────────────────────────────────────────────────
        private PictureBox[] stars;
        private int currentRating = 0;
        private User_BX currentUser;

        // ── Font caches (avoids re-allocating Font on every resize) ───────────
        private float _lastStreakFontSize = -1f;
        private float _lastFinishedFontSize = -1f;
        private float _lastDroppedFontSize = -1f;
        private float _lastDayFontSize = -1f;
        private float _lastTimeFontSize = -1f;

        // ── Reflection cache (set DoubleBuffered via private property once) ───
        private static readonly System.Reflection.PropertyInfo _doubleBufferedProp =
            typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic);

        // ─────────────────────────────────────────────────────────────────────
        public Dashboard_FX()
        {
            // Enable double-buffering on this control first
            this.DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            this.UpdateStyles();

            InitializeComponent();

            // ── Static configuration ──────────────────────────────────────────
            InitializeStarRating();
            ConfigureStaticControlProperties();

            // ── Double-buffer all child panels ────────────────────────────────
            EnableDoubleBufferingRecursive(this);

            // ── Resize debounce timer (50 ms) ─────────────────────────────────
            _resizeDebounceTimer = new System.Windows.Forms.Timer { Interval = 50 };
            _resizeDebounceTimer.Tick += (s, e) =>
            {
                _resizeDebounceTimer.Stop();
                ResizeContent();
            };

            // ── Loading overlay ───────────────────────────────────────────────
            CreateLoadingPanel();

            this.Load += Dashboard_FX_Load;
        }

        // ── Loading panel ─────────────────────────────────────────────────────
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

        // ── Async initialization ─────────────────────────────────────────────
        private async void Dashboard_FX_Load(object sender, EventArgs e)
        {
            this.Resize += Dashboard_FX_Resize;

            // Show loading panel
            if (loadingPanel != null)
                loadingPanel.Visible = true;

            try
            {
                // Do async work here (database, network, etc.)
                await InitializeContentAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in InitializeContent: {ex.Message}");
            }
            finally
            {
                // Hide loading panel on UI thread
                if (loadingPanel != null && !this.IsDisposed)
                {
                    loadingPanel.Visible = false;
                    this.Controls.Remove(loadingPanel);
                    loadingPanel.Dispose();
                    loadingPanel = null;
                }

                isInitializing = false;

                // Apply UI updates after loading
                ApplyUISettings();

                // Start timers and set initial values
                DateTime now = DateTime.Now;
                MonthText.Text = now.ToString("MMMM");
                DayText.Text = now.ToString("dd");
                YearText.Text = now.ToString("yyyy");

                UpdateCurrentTime();

                timeTimer = new System.Windows.Forms.Timer { Interval = 1000 };
                timeTimer.Tick += TimeTimer_Tick;
                timeTimer.Start();

                ResizeContent();
                this.Refresh();

                // Delay to ensure layout is fully settled before applying task colors
                System.Windows.Forms.Timer colorTimer = new System.Windows.Forms.Timer { Interval = 100 };
                colorTimer.Tick += (s2, e2) =>
                {
                    colorTimer.Stop();
                    colorTimer.Dispose();
                    RefreshAll();
                };
                colorTimer.Start();
            }
        }

        // Async initialization method
        private async System.Threading.Tasks.Task InitializeContentAsync()
        {
            // Simulate async work (database calls, API calls, etc.)
            // Replace with your actual async work
            await System.Threading.Tasks.Task.Delay(100); // Small delay for loading panel visibility
            // await yourDatabaseCallAsync();
            // await yourNetworkCallAsync();
        }

        // Separate method for UI updates (called on UI thread)
        private void ApplyUISettings()
        {
            if (this.IsDisposed) return;

            // Apply panel colours
            if (StreakCounterPanel != null && !StreakCounterPanel.IsDisposed)
                StreakCounterPanel.BackColor = ColorTranslator.FromHtml("#FFFB96");

            if (FinishedCounter != null && !FinishedCounter.IsDisposed)
                FinishedCounter.BackColor = ColorTranslator.FromHtml("#AAE4FF");

            if (DroppedCounter != null && !DroppedCounter.IsDisposed)
                DroppedCounter.BackColor = ColorTranslator.FromHtml("#FFACBA");

            if (DateDisplayPanel != null && !DateDisplayPanel.IsDisposed)
                DateDisplayPanel.BackColor = ColorTranslator.FromHtml("#C96BFF");

            // CurrentTaskDisplay color is set dynamically in UpdateCurrentAndNextTasks — not set here

            if (TimeDisplayPanel != null && !TimeDisplayPanel.IsDisposed)
                TimeDisplayPanel.BackColor = ColorTranslator.FromHtml("#FFBCF0");

            if (NextTaskDisplay != null && !NextTaskDisplay.IsDisposed)
                NextTaskDisplay.BackColor = ColorTranslator.FromHtml("#90B3FF");

            // Set control order inside flow panels
            if (flowLayoutPanel1 != null && !flowLayoutPanel1.IsDisposed &&
                flowLayoutPanel1.Controls.Count >= 3 &&
                StreakCounterPanel != null && !StreakCounterPanel.IsDisposed &&
                FinishedCounter != null && !FinishedCounter.IsDisposed &&
                DroppedCounter != null && !DroppedCounter.IsDisposed)
            {
                flowLayoutPanel1.Controls.SetChildIndex(StreakCounterPanel, 0);
                flowLayoutPanel1.Controls.SetChildIndex(FinishedCounter, 1);
                flowLayoutPanel1.Controls.SetChildIndex(DroppedCounter, 2);
            }

            if (flowLayoutPanel10 != null && !flowLayoutPanel10.IsDisposed &&
                flowLayoutPanel10.Controls.Count >= 2 &&
                DateDisplayPanel != null && !DateDisplayPanel.IsDisposed &&
                CurrentTaskDisplay != null && !CurrentTaskDisplay.IsDisposed)
            {
                flowLayoutPanel10.Controls.SetChildIndex(DateDisplayPanel, 0);
                flowLayoutPanel10.Controls.SetChildIndex(CurrentTaskDisplay, 1);
            }

            if (flowLayoutPanel18 != null && !flowLayoutPanel18.IsDisposed &&
                flowLayoutPanel18.Controls.Count >= 2 &&
                TimeDisplayPanel != null && !TimeDisplayPanel.IsDisposed &&
                NextTaskDisplay != null && !NextTaskDisplay.IsDisposed)
            {
                flowLayoutPanel18.Controls.SetChildIndex(TimeDisplayPanel, 0);
                flowLayoutPanel18.Controls.SetChildIndex(NextTaskDisplay, 1);
            }
        }

        // ── Cleanup on handle destroyed ───────────────────────────────────────
        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (timeTimer != null)
            {
                timeTimer.Stop();
                timeTimer.Dispose();
                timeTimer = null;
            }

            if (_resizeDebounceTimer != null)
            {
                _resizeDebounceTimer.Stop();
                _resizeDebounceTimer.Dispose();
                _resizeDebounceTimer = null;
            }

            base.OnHandleDestroyed(e);
        }

        // ── Static control configuration ──────────────────────────────────────
        private void ConfigureStaticControlProperties()
        {
            // flowLayoutPanel14 – Current Task header
            flowLayoutPanel14.SuspendLayout();
            flowLayoutPanel14.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel14.WrapContents = false;
            flowLayoutPanel14.Padding = new Padding(10, 0, 10, 0);
            flowLayoutPanel14.Height = 40;
            flowLayoutPanel14.Dock = DockStyle.Top;

            // Configure Current Task label
            label11.AutoSize = false;
            label11.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label11.TextAlign = ContentAlignment.MiddleLeft;
            label11.Height = flowLayoutPanel14.Height;

            // Configure trash button
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Visible = true;
            pictureBox3.Anchor = AnchorStyles.None;

            // Configure NextTaskBtn
            if (NextTaskBtn != null)
            {
                NextTaskBtn.SizeMode = PictureBoxSizeMode.StretchImage;
                NextTaskBtn.Visible = true;
                NextTaskBtn.BackColor = Color.Transparent;
                NextTaskBtn.Anchor = AnchorStyles.None;
            }

            // Set control order (label first, then trash, then next button)
            flowLayoutPanel14.Controls.SetChildIndex(label11, 0);
            flowLayoutPanel14.Controls.SetChildIndex(pictureBox3, 1);
            flowLayoutPanel14.Controls.SetChildIndex(NextTaskBtn, 2);

            flowLayoutPanel14.ResumeLayout(false);

            // Greeting section
            label4.AutoSize = false;
            label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label4.TextAlign = ContentAlignment.MiddleLeft;
            NamePlaceholder.AutoSize = false;
            NamePlaceholder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NamePlaceholder.TextAlign = ContentAlignment.MiddleLeft;

            // Stats – Streak
            label1.AutoSize = false;
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            flowLayoutPanel3.FlowDirection = FlowDirection.LeftToRight;
            pictureBox1.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.TextAlign = ContentAlignment.MiddleRight;

            // Stats – Finished
            label5.AutoSize = false;
            label5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label5.TextAlign = ContentAlignment.MiddleLeft;
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.TextAlign = ContentAlignment.MiddleRight;

            // Stats – Dropped
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
            DayText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            YearText.TextAlign = ContentAlignment.MiddleLeft;

            // Current task
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

            // Main flow containers
            foreach (var flp in new[] { flowLayoutPanel6, flowLayoutPanel7,
                                flowLayoutPanel1, flowLayoutPanel10,
                                flowLayoutPanel18 })
            {
                if (flp != null)
                {
                    flp.WrapContents = false;
                    flp.Margin = new Padding(0);
                    flp.Padding = new Padding(0);
                }
            }
        }

        // ── Double-buffering helpers ──────────────────────────────────────────
        private void EnableDoubleBufferingRecursive(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Panel || control is FlowLayoutPanel)
                    _doubleBufferedProp?.SetValue(control, true, null);

                if (control.HasChildren)
                    EnableDoubleBufferingRecursive(control);
            }
        }

        // ── Star rating ───────────────────────────────────────────────────────
        private void InitializeStarRating()
        {
            stars = new PictureBox[]
            {
                star1, star2, star3, star4, star5
            };

            foreach (var star in stars)
            {
                if (star != null)
                {
                    star.Enabled = false;
                    star.Cursor = Cursors.Default;
                    star.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        public void SetTaskRating(int rating)
        {
            rating = Math.Max(0, Math.Min(5, rating));
            currentRating = rating;

            for (int i = 0; i < stars.Length; i++)
            {
                if (stars[i] != null)
                {
                    stars[i].Image = Properties.Resources.Rating;
                    stars[i].Enabled = i < rating;
                }
            }
        }

        public void UpdateUserName(string userName)
        {
            if (!string.IsNullOrEmpty(userName) && NamePlaceholder != null && !NamePlaceholder.IsDisposed)
                NamePlaceholder.Text = userName;
        }

        // ── User management ───────────────────────────────────────────────────
        internal void SetUser()
        {
            currentUser = AppState.CurrentUser;
            if (currentUser != null)
            {
                NamePlaceholder.Text = currentUser.Username;

                // FIX: Force immediate refresh even during initialization
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        if (!this.IsDisposed && CurrentTaskDisplay != null)
                        {
                            RefreshAll();
                        }
                    }));
                }
            }
        }



        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (!isInitializing)
                RefreshAll();
        }

        // ── Resize (debounced) ────────────────────────────────────────────────
        private void Dashboard_FX_Resize(object sender, EventArgs e)
        {
            if (_resizeDebounceTimer != null)
            {
                _resizeDebounceTimer.Stop();
                _resizeDebounceTimer.Start();
            }

            // Update header layout on resize using BeginInvoke to ensure proper timing
            this.BeginInvoke(new Action(() =>
            {
                if (!this.IsDisposed && flowLayoutPanel14 != null && !flowLayoutPanel14.IsDisposed)
                {
                    UpdateCurrentTaskHeaderLayout();
                }
            }));
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

                int availableTotalHeight = Math.Max(400, this.ClientSize.Height - (topBottomMargin * 2));

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

                if (flowLayoutPanel6 != null)
                    flowLayoutPanel6.Height = greetingTopHeight;
                if (flowLayoutPanel7 != null)
                    flowLayoutPanel7.Height = greetingBottomHeight;
                if (flowLayoutPanel1 != null)
                    flowLayoutPanel1.Height = Math.Max(100, statsHeight);
                if (flowLayoutPanel10 != null)
                    flowLayoutPanel10.Height = Math.Max(120, currentTaskHeight);
                if (flowLayoutPanel18 != null)
                    flowLayoutPanel18.Height = Math.Max(80, timeHeight);

                int fullWidth = this.ClientSize.Width;
                if (flowLayoutPanel6 != null)
                    flowLayoutPanel6.Width = fullWidth;
                if (flowLayoutPanel7 != null)
                    flowLayoutPanel7.Width = fullWidth;
                if (flowLayoutPanel1 != null)
                    flowLayoutPanel1.Width = fullWidth;
                if (flowLayoutPanel10 != null)
                    flowLayoutPanel10.Width = fullWidth;
                if (flowLayoutPanel18 != null)
                    flowLayoutPanel18.Width = fullWidth;

                int gapsBetweenPanels1 = panelMargin * 2;
                int gapsBetweenPanels2 = panelMargin * 1;

                int availableWidth1 = flowLayoutPanel1.ClientSize.Width - gapsBetweenPanels1;
                int availableWidth2 = flowLayoutPanel10.ClientSize.Width - gapsBetweenPanels2;

                if (availableWidth1 <= 0) return;

                int panelWidth1 = availableWidth1 / 3;
                int panelHeight1 = flowLayoutPanel1.ClientSize.Height - (panelMargin * 2);

                int panelWidth2 = availableWidth2 / 2;
                int panelHeight2 = flowLayoutPanel10.ClientSize.Height - (panelMargin * 2);

                int panelWidth3 = availableWidth2 / 2;
                int panelHeight3 = flowLayoutPanel18.ClientSize.Height - (panelMargin * 2);

                ApplyStatsPanelSizes(
                    new Control[] { StreakCounterPanel, FinishedCounter, DroppedCounter },
                    panelWidth1, panelHeight1, panelMargin);

                ApplyTwoPanelSizesEven(
                    new Control[] { DateDisplayPanel, CurrentTaskDisplay },
                    panelWidth2, panelHeight2, panelMargin);

                ApplyTwoPanelSizesEven(
                    new Control[] { TimeDisplayPanel, NextTaskDisplay },
                    panelWidth3, panelHeight3, panelMargin);

                SetLabelWidths();

                // Integrated Current Task header layout
                UpdateCurrentTaskHeaderLayout();

                UpdateDynamicFontSizes(statsHeight, currentTaskHeight, timeHeight);
            }
            finally
            {
                if (flowLayoutPanel1 != null)
                    flowLayoutPanel1.ResumeLayout();
                if (flowLayoutPanel18 != null)
                    flowLayoutPanel18.ResumeLayout();
                if (flowLayoutPanel10 != null)
                    flowLayoutPanel10.ResumeLayout();
            }
        }

        private void UpdateCurrentTaskHeaderLayout()
        {
            if (flowLayoutPanel14 == null || label11 == null || pictureBox3 == null) return;

            if (flowLayoutPanel14.InvokeRequired)
            {
                flowLayoutPanel14.Invoke(new Action(UpdateCurrentTaskHeaderLayout));
                return;
            }

            flowLayoutPanel14.SuspendLayout();

            int buttonSize = 25;
            int totalButtonsWidth = (buttonSize * 2) + 15; // buttons + margins

            // Update label width to fill available space
            label11.Width = Math.Max(50, flowLayoutPanel14.Width - totalButtonsWidth - 30);
            label11.Height = flowLayoutPanel14.Height;

            // Update trash button position and size
            pictureBox3.Size = new Size(buttonSize, buttonSize);
            pictureBox3.Margin = new Padding(0, (flowLayoutPanel14.Height - buttonSize) / 2, 5, 0);

            // Update NextTaskBtn position and size
            if (NextTaskBtn != null && !NextTaskBtn.IsDisposed)
            {
                NextTaskBtn.Size = new Size(buttonSize, buttonSize);
                NextTaskBtn.Margin = new Padding(0, (flowLayoutPanel14.Height - buttonSize) / 2, 10, 0);
            }

            // Ensure correct control order
            flowLayoutPanel14.Controls.SetChildIndex(label11, 0);
            flowLayoutPanel14.Controls.SetChildIndex(pictureBox3, 1);
            flowLayoutPanel14.Controls.SetChildIndex(NextTaskBtn, 2);

            flowLayoutPanel14.ResumeLayout(false);
            flowLayoutPanel14.PerformLayout();
        }

        private void ApplyTwoPanelSizesEven(Control[] panels, int width, int height, int margin)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                var panel = panels[i];
                if (panel == null || panel.IsDisposed) continue;

                panel.Width = width;
                panel.Height = height;
                panel.Margin = i == panels.Length - 1
                    ? new Padding(margin / 2, margin / 2, 0, margin / 2)
                    : new Padding(margin / 2, margin / 2, margin / 2, margin / 2);
            }
        }

        private void ApplyStatsPanelSizes(Control[] panels, int width, int height, int margin)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                var panel = panels[i];
                if (panel == null || panel.IsDisposed) continue;

                panel.Width = width;
                panel.Height = height;
                panel.Margin = i == panels.Length - 1
                    ? new Padding(margin / 2, margin / 2, 0, margin / 2)
                    : new Padding(margin / 2, margin / 2, margin / 2, margin / 2);
            }
        }

        private void SetLabelWidths()
        {
            SetLabelSize(label4, flowLayoutPanel6, widthOffset: 30, heightOffset: 10);
            SetLabelSize(NamePlaceholder, flowLayoutPanel7, widthOffset: 30, heightOffset: 10);
            SetLabelSize(label1, flowLayoutPanel2, widthOffset: 20, heightOffset: 0);
            SetLabelSize(label5, flowLayoutPanel5, widthOffset: 20, heightOffset: 0);
            SetLabelSize(label7, flowLayoutPanel9, widthOffset: 20, heightOffset: 0);
            SetLabelSize(MonthText, flowLayoutPanel12, widthOffset: 20, heightOffset: 0);
            SetLabelSize(DayText, flowLayoutPanel11, widthOffset: 20, heightOffset: 0);
            SetLabelSize(YearText, flowLayoutPanel15, widthOffset: 20, heightOffset: 0);
            SetLabelSize(NameTaskText, flowLayoutPanel13, widthOffset: 20, heightOffset: 0);
            SetLabelSize(DecriptionText, flowLayoutPanel16, widthOffset: 20, heightOffset: 0);
            SetLabelSize(Timetext, flowLayoutPanel21, widthOffset: 20, heightOffset: 0);
            SetLabelSize(label10, flowLayoutPanel23, widthOffset: 20, heightOffset: 0);
            SetLabelSize(NameNextTaskText, flowLayoutPanel22, widthOffset: 20, heightOffset: 0);

            // Update Current Task header label size
            if (label11 != null && flowLayoutPanel14 != null && !label11.IsDisposed && !flowLayoutPanel14.IsDisposed)
            {
                int buttonSize = 25;
                int totalButtonsWidth = (buttonSize * 2) + 15;
                label11.Width = Math.Max(50, flowLayoutPanel14.Width - totalButtonsWidth - 30);
                label11.Height = flowLayoutPanel14.Height;
            }

            if (label2 != null && flowLayoutPanel3 != null && pictureBox1 != null)
            {
                label2.Width = flowLayoutPanel3.ClientSize.Width - pictureBox1.Width - 40;
                label2.Height = flowLayoutPanel3.ClientSize.Height - 20;
            }
            if (label3 != null && flowLayoutPanel4 != null)
            {
                label3.Width = flowLayoutPanel4.ClientSize.Width - 20;
                label3.Height = flowLayoutPanel4.ClientSize.Height - 20;
            }
            if (label6 != null && flowLayoutPanel8 != null)
            {
                label6.Width = flowLayoutPanel8.ClientSize.Width - 20;
                label6.Height = flowLayoutPanel8.ClientSize.Height - 20;
            }
        }

        private static void SetLabelSize(Control label, Control container,
                                         int widthOffset, int heightOffset)
        {
            if (label == null || label.IsDisposed || container == null || container.IsDisposed) return;
            label.Width = container.ClientSize.Width - widthOffset;
            label.Height = container.ClientSize.Height - heightOffset;
        }

        private void UpdateDynamicFontSizes(int statsHeight, int taskHeight, int timeHeight)
        {
            float streakSize = Math.Max(12, Math.Min(28, statsHeight / 2.5f));
            float finishedSize = Math.Max(12, Math.Min(28, statsHeight / 2.5f));
            float droppedSize = Math.Max(12, Math.Min(28, statsHeight / 2.5f));
            float daySize = Math.Max(16, Math.Min(36, taskHeight / 2.5f));
            float timeSize = Math.Max(16, Math.Min(48, timeHeight / 2.0f));

            SetCachedFont(label2, ref _lastStreakFontSize, streakSize, "Segoe UI", FontStyle.Bold);
            SetCachedFont(label3, ref _lastFinishedFontSize, finishedSize, "Segoe UI", FontStyle.Bold);
            SetCachedFont(label6, ref _lastDroppedFontSize, droppedSize, "Segoe UI", FontStyle.Bold);
            SetCachedFont(DayText, ref _lastDayFontSize, daySize, "Segoe UI", FontStyle.Bold);
            SetCachedFont(Timetext, ref _lastTimeFontSize, timeSize, "Segoe UI", FontStyle.Bold);
        }

        private static void SetCachedFont(Control control, ref float cachedSize,
                                          float newSize, string family, FontStyle style)
        {
            if (control == null || control.IsDisposed) return;
            if (Math.Abs(newSize - cachedSize) <= 0.5f) return;

            control.Font?.Dispose();
            control.Font = new Font(family, newSize, style);
            cachedSize = newSize;
        }

        // ── Clock ─────────────────────────────────────────────────────────────
        private void UpdateCurrentTime()
        {
            if (Timetext != null && !Timetext.IsDisposed)
                Timetext.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            UpdateCurrentTime();
        }

        // ── Task management ───────────────────────────────────────────────────
        public void UpdateCurrentAndNextTasks()
        {
            if (currentUser == null) return;

            try
            {
                // Combine morning and afternoon tasks – no time‑of‑day filtering
                var allTasks = currentUser.MorningTasks.Concat(currentUser.AfternoonTasks).ToList();

                if (allTasks.Count > 0 && NameTaskText != null && !NameTaskText.IsDisposed)
                {
                    Task_BX currentTask = allTasks[0];
                    NameTaskText.Text = currentTask.Name;
                    if (DecriptionText != null && !DecriptionText.IsDisposed)
                        DecriptionText.Text = currentTask.Description ?? "";
                    SetStars(currentTask.Priority);

                    // NEW: Update the CurrentTaskDisplay panel color to match the task's card color
                    UpdateCurrentTaskDisplayColor(currentTask.CardColor);
                }
                else
                {
                    if (NameTaskText != null && !NameTaskText.IsDisposed)
                        NameTaskText.Text = "No tasks";
                    if (DecriptionText != null && !DecriptionText.IsDisposed)
                        DecriptionText.Text = "";
                    SetStars(0);

                    // NEW: Reset to default color when no tasks
                    ResetCurrentTaskDisplayColor();
                }

                if (NameNextTaskText != null && !NameNextTaskText.IsDisposed)
                    NameNextTaskText.Text = allTasks.Count > 1 ? allTasks[1].Name : "No tasks";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateCurrentAndNextTasks: {ex.Message}");
            }
        }

        // NEW: Method to update CurrentTaskDisplay color based on task's card color
        private void UpdateCurrentTaskDisplayColor(Color taskColor)
        {
            if (CurrentTaskDisplay != null && !CurrentTaskDisplay.IsDisposed)
            {
                CurrentTaskDisplay.BackColor = taskColor;

                // Optional: Adjust text colors for better contrast based on background
                // You can calculate luminance and adjust text colors accordingly
                if (NameTaskText != null && !NameTaskText.IsDisposed)
                    NameTaskText.ForeColor = GetContrastColor(taskColor);
                if (DecriptionText != null && !DecriptionText.IsDisposed)
                    DecriptionText.ForeColor = GetContrastColor(taskColor);
            }
        }

        // NEW: Reset to default color
        private void ResetCurrentTaskDisplayColor()
        {
            if (CurrentTaskDisplay != null && !CurrentTaskDisplay.IsDisposed)
                CurrentTaskDisplay.BackColor = ColorTranslator.FromHtml("#B38DFF");

            // Reset text colors to default
            if (NameTaskText != null && !NameTaskText.IsDisposed)
                NameTaskText.ForeColor = SystemColors.ControlText;
            if (DecriptionText != null && !DecriptionText.IsDisposed)
                DecriptionText.ForeColor = SystemColors.ControlText;
        }

        // NEW: Helper method to determine contrasting text color (black or white)
        private Color GetContrastColor(Color backgroundColor)
        {
            // Calculate luminance - formula: (0.299*R + 0.587*G + 0.114*B)
            double luminance = (0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B) / 255;

            // Return white for dark backgrounds, black for light backgrounds
            return luminance > 0.5 ? Color.Black : Color.White;
        }

        private void SetStars(int priority)
        {
            if (priority < 0) priority = 0;
            if (priority > 5) priority = 5;

            PictureBox[] starsArray = { star1, star2, star3, star4, star5 };
            for (int i = 0; i < starsArray.Length; i++)
            {
                if (starsArray[i] != null && !starsArray[i].IsDisposed)
                {
                    if (i < priority)
                        starsArray[i].Image = Properties.Resources.Rating;
                    else
                        starsArray[i].Image = CreateDimmedStar(Properties.Resources.Rating);
                }
            }
        }

        private Image CreateDimmedStar(Image original)
        {
            if (original == null) return null;

            Bitmap dimmed = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(dimmed))
            {
                float[][] matrix = {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.4f, 0},
                    new float[] {0, 0, 0, 0, 1}
                };
                using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                {
                    attributes.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix(matrix));
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                        0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return dimmed;
        }

        // ── Event handlers ────────────────────────────────────────────────────
        private void NamePlaceholder_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void DayText_Click(object sender, EventArgs e) { }
        private void MonthText_Click(object sender, EventArgs e) { }
        private void YearText_Click(object sender, EventArgs e) { }
        private void NameTaskText_Click(object sender, EventArgs e) { }
        private void DecriptionText_Click(object sender, EventArgs e) { }
        private void Timetext_Click(object sender, EventArgs e) { }
        private void NameNextTaskText_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void DecriptionText_Click_1(object sender, EventArgs e) { }
        private void star5_Click(object sender, EventArgs e) { }
        private void star1_Click(object sender, EventArgs e) { }
        private void star2_Click(object sender, EventArgs e) { }
        private void star3_Click(object sender, EventArgs e) { }
        private void star4_Click(object sender, EventArgs e) { }
        private void NameNextTaskText_Click_1(object sender, EventArgs e) { }

        private Task_BX GetCurrentTask()
        {
            if (currentUser == null) return null;
            var allTasks = currentUser.MorningTasks.Concat(currentUser.AfternoonTasks).ToList();
            if (allTasks.Count == 0) return null;
            return allTasks[0];
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Get the current task
            Task_BX currentTask = GetCurrentTask();
            if (currentTask == null) return;

            DialogResult result = MessageBox.Show($"Drop task '{currentTask.Name}'?", "Drop Task",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            // Increment dropped counter
            AppState.TotalDroppedTasks++;
            AppState.ConsecutiveFinishes = 0;
            AppState.CurrentStreak = 0;

            // Remove task
            currentUser?.RemoveTask(currentTask);

            // Refresh dashboard
            RefreshAll();
        }

        public void RefreshStats()
        {
            if (label2 != null && !label2.IsDisposed)
                label2.Text = AppState.CurrentStreak.ToString();
            if (label3 != null && !label3.IsDisposed)
                label3.Text = AppState.TotalFinishedTasks.ToString();
            if (label6 != null && !label6.IsDisposed)
                label6.Text = AppState.TotalDroppedTasks.ToString();
        }

        public void RefreshAll()
        {
            if (AppState.CurrentUser != null)
            {
                currentUser = AppState.CurrentUser;
                NamePlaceholder.Text = currentUser.Username;
                UpdateCurrentAndNextTasks();
                RefreshStats();
            }
        }

        private void NextTaskBtn_Click(object sender, EventArgs e)
        {
            // Get the current task (first in combined list)
            Task_BX currentTask = GetCurrentTask();
            if (currentTask == null) return;

            // Confirm with user
            DialogResult result = MessageBox.Show($"Finish task '{currentTask.Name}'?", "Complete Task",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            // Increment finished counter
            AppState.TotalFinishedTasks++;
            AppState.ConsecutiveFinishes++;
            if (AppState.ConsecutiveFinishes % 2 == 0)
            {
                AppState.CurrentStreak++;
            }

            // Remove task from user
            currentUser?.RemoveTask(currentTask);

            // Refresh dashboard
            RefreshAll();
        }

        public void ForceRefreshDashboard()
        {
            if (this.IsDisposed) return;

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(ForceRefreshDashboard));
                return;
            }

            currentUser = AppState.CurrentUser;
            if (currentUser != null)
            {
                NamePlaceholder.Text = currentUser.Username;

                var allTasks = currentUser.MorningTasks.Concat(currentUser.AfternoonTasks).ToList();

                if (allTasks.Count > 0)
                {
                    Task_BX currentTask = allTasks[0];
                    NameTaskText.Text = currentTask.Name;
                    DecriptionText.Text = currentTask.Description ?? "";
                    SetStars(currentTask.Priority);

                    // Force color update
                    if (CurrentTaskDisplay != null && !CurrentTaskDisplay.IsDisposed)
                    {
                        CurrentTaskDisplay.BackColor = currentTask.CardColor;
                    }
                }
                else
                {
                    NameTaskText.Text = "No tasks";
                    DecriptionText.Text = "";
                    SetStars(0);

                    if (CurrentTaskDisplay != null && !CurrentTaskDisplay.IsDisposed)
                    {
                        CurrentTaskDisplay.BackColor = ColorTranslator.FromHtml("#B38DFF");
                    }
                }

                RefreshStats();
            }
        }

        // Override OnVisibleChanged to refresh when dashboard becomes visible
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            // Guard against firing during async init — colors aren't ready yet
            if (this.Visible && !this.IsDisposed && !isInitializing)
            {
                // Delay to ensure control is ready
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 50;
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                    timer.Dispose();
                    ForceRefreshDashboard();
                };
                timer.Start();
            }
        }
    }
}