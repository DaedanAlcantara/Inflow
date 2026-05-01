using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Inflow
{
    public partial class Nitro_FX : UserControl
    {
        // ── Variables ───────────────────────────────────────────────────────
        private User_BX currentUser;
        private List<Task_BX> masterQueue = new List<Task_BX>();
        private int currentTaskIndex = 0;

        // Stats Tracking
        private int currentStreak = 0;
        private int finishedCount = 0;
        private int droppedCount = 0;

        private System.Windows.Forms.Timer sessionTimer;
        private TimeSpan userTaskDuration = TimeSpan.Zero;

        public Nitro_FX()
        {
            InitializeComponent();
            SetupTimer();
            AttachEvents();
        }

        private void Nitro_FX_Load(object sender, EventArgs e)
        {
            currentUser = AppState.CurrentUser;
            LoadTasksToQueue();

            if (masterQueue.Count > 0)
            {
                DisplayCurrentTask();
                if (!sessionTimer.Enabled) sessionTimer.Start();
            }
            else
            {
                FinishSession();
            }

            CenterAllControls();
        }
        // ── Timer Logic ─────────────────────────────────────────────────────
        private void SetupTimer()
        {
            if (sessionTimer != null)
            {
                sessionTimer.Stop();
                sessionTimer.Tick -= Timer_Tick_Logic; 
                sessionTimer.Dispose();
            }

            sessionTimer.Tick += Timer_Tick_Logic;
        }
        private void Timer_Tick_Logic(object sender, EventArgs e)
        {
            AppState.NitroElapsedSeconds++;
            TimeSpan elapsed = TimeSpan.FromSeconds(AppState.NitroElapsedSeconds);

            if (TimePlaceholderText != null)
                TimePlaceholderText.Text = elapsed.ToString(@"mm\:ss");

            if (currentTaskIndex < masterQueue.Count)
            {
                var task = masterQueue[currentTaskIndex];

                // Console.WriteLine($"Elapsed: {AppState.NitroElapsedSeconds}, Limit: {userTaskDuration.TotalSeconds}");

                double limitInSeconds = userTaskDuration.TotalSeconds;

                // TRIGGER CHECK
                if (limitInSeconds > 0 && AppState.NitroElapsedSeconds >= (int)limitInSeconds)
                {
                    TriggerAutoDrop(task.Name);
                }
            }
        }
        public void SetSessionDuration(TimeSpan duration)
        {
            this.userTaskDuration = duration;
        }
        // ── Event Handlers ──────────────────────────────────────────────────
        private void AttachEvents()
        {
            if (NextTaskButton != null)
            {
                NextTaskButton.Click -= HandleComplete;
                NextTaskButton.Click += HandleComplete;
            }

            if (StopButton != null)
            {
                StopButton.Click -= HandleStop;
                StopButton.Click += HandleStop;
            }

            var dropBtn = this.Controls.Find("btnDropTask", true).FirstOrDefault() as Button;
            if (dropBtn != null)
            {
                dropBtn.Click -= HandleDrop;
                dropBtn.Click += HandleDrop;
            }
        }
        private void HandleComplete(object sender, EventArgs e) => AdvanceTask(isFinished: true);

        private void HandleDrop(object sender, EventArgs e) => AdvanceTask(isFinished: false);

        private void HandleStop(object sender, EventArgs e)
        {
            sessionTimer?.Stop();
            this.Visible = false;
        }

        private void TriggerAutoDrop(string taskName)
        {
            sessionTimer.Stop();
            MessageBox.Show($"Time is up for '{taskName}'!", "Nitro Auto-Drop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            HandleDrop(null, null);
            if (currentTaskIndex < masterQueue.Count) sessionTimer.Start();
        }
        private void AdvanceTask(bool isFinished)
        {
            if (currentTaskIndex >= masterQueue.Count) return;

            if (isFinished)
            {
                finishedCount++;
                currentStreak++;
            }
            else
            {
                droppedCount++;
                currentStreak = 0;
            }
            currentUser?.RemoveTask(masterQueue[currentTaskIndex]);

            currentTaskIndex++;
            AppState.NitroElapsedSeconds = 0; 

            DisplayCurrentTask();
        }
        // ── UI Helpers ──────────────────────────────────────────────────────
        private void LoadTasksToQueue()
        {
            masterQueue.Clear();
            if (currentUser == null) return;
            if (currentUser.MorningTasks != null) masterQueue.AddRange(currentUser.MorningTasks);
            if (currentUser.AfternoonTasks != null) masterQueue.AddRange(currentUser.AfternoonTasks);
        }

        private void DisplayCurrentTask()
        {
            if (currentTaskIndex < masterQueue.Count)
            {
                var task = masterQueue[currentTaskIndex];
                if (TaskNamePlaceholder != null) TaskNamePlaceholder.Text = task.Name;
                if (DescriptionPlaceholder != null) DescriptionPlaceholder.Text = task.Description;

                var streakLabel = this.Controls.Find("StreakCounterLabel", true).FirstOrDefault() as Label;
                if (streakLabel != null) streakLabel.Text = currentStreak.ToString();

                UpdatePriorityStars(task.Priority);
                UpdateQueuePreview();
                CenterAllControls();
            }
            else
            {
                FinishSession();
            }
        }
        private void FinishSession()
        {
            sessionTimer?.Stop();
            if (TaskNamePlaceholder != null) TaskNamePlaceholder.Text = "TASK CLEARED";
            if (DescriptionPlaceholder != null) DescriptionPlaceholder.Text = "You finished this task!!.";
            MessageBox.Show($"Session Finished!\nCompleted: {finishedCount}\nDropped: {droppedCount}", "Nitro Result");
        }

        private void UpdatePriorityStars(int priority)
        {
            if (flowLayoutPanel10 == null) return;
            for (int i = 0; i < flowLayoutPanel10.Controls.Count; i++)
                flowLayoutPanel10.Controls[i].Visible = (i < priority);
        }

        private void UpdateQueuePreview()
        {
            SetPreviewText(NextTaskPlaceholder, 1);
            SetPreviewText(NextTask2Placeholder, 2);
            SetPreviewText(NextTask3Placeholder, 3);
        }

        private void SetPreviewText(Label lbl, int offset)
        {
            if (lbl != null)
                lbl.Text = (currentTaskIndex + offset < masterQueue.Count)
                    ? masterQueue[currentTaskIndex + offset].Name : "---";
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterAllControls();
        }

        private void CenterAllControls()
        {
            // Resize all panels to fit width
            ResizePanelsToWidth();

            // Center the main task panels
            CenterPanel(CurrentTaskDisplayPanel);
            CenterPanel(NextTaskDisplayPanel);
            CenterPanel(NextTask2DisplayPanel);
            CenterPanel(NextTask3DisplayPanel);

            // Center the buttons
            CenterButtons();

            // Center contents inside panels
            CenterInnerContents();
            // Center placeholdertexts inside their panels
            CenterPlaceholderText();
        }

        private void CenterPlaceholderText()
        {
            if (TimePlaceholderText != null && panel1 != null)
            {
                TimePlaceholderText.Left = (panel1.Width - TimePlaceholderText.Width) / 2;
                TimePlaceholderText.Top = (panel1.Height - TimePlaceholderText.Height) / 2;
            }
            if (TaskNamePlaceholder != null && flowLayoutPanel3 != null)
            {
                TaskNamePlaceholder.Left = (flowLayoutPanel3.Width - TaskNamePlaceholder.Width) / 2;
                TaskNamePlaceholder.Top = (flowLayoutPanel3.Height - TaskNamePlaceholder.Height) / 2;
            }
            if (DescriptionPlaceholder != null && flowLayoutPanel4 != null)
            {
                DescriptionPlaceholder.Left = (flowLayoutPanel4.Width - DescriptionPlaceholder.Width) / 2;
                DescriptionPlaceholder.Top = (flowLayoutPanel4.Height - DescriptionPlaceholder.Height) / 2;
            }
            if (flowLayoutPanel10 != null && flowLayoutPanel5 != null)
            {
                flowLayoutPanel10.Left = (flowLayoutPanel5.Width - flowLayoutPanel10.Width) / 2;
                flowLayoutPanel10.Top = (flowLayoutPanel5.Height - flowLayoutPanel10.Height) / 2;
            }

            // Center next task placeholders
            if (NextTaskPlaceholder != null && NextTaskDisplayPanel != null)
            {
                NextTaskPlaceholder.Left = (NextTaskDisplayPanel.Width - NextTaskPlaceholder.Width) / 2;
                NextTaskPlaceholder.Top = (NextTaskDisplayPanel.Height - NextTaskPlaceholder.Height) / 2;
            }
            if (NextTask2Placeholder != null && NextTask2DisplayPanel != null)
            {
                NextTask2Placeholder.Left = (NextTask2DisplayPanel.Width - NextTask2Placeholder.Width) / 2;
                NextTask2Placeholder.Top = (NextTask2DisplayPanel.Height - NextTask2Placeholder.Height) / 2;
            }
            if (NextTask3Placeholder != null && NextTask3DisplayPanel != null)
            {
                NextTask3Placeholder.Left = (NextTask3DisplayPanel.Width - NextTask3Placeholder.Width) / 2;
                NextTask3Placeholder.Top = (NextTask3DisplayPanel.Height - NextTask3Placeholder.Height) / 2;
            }
        }

        private void ResizePanelsToWidth()
        {
            int maxWidth = this.ClientSize.Width - 40; // 40 for margins

            // Resize flowLayoutPanel1 to full width
            if (flowLayoutPanel1 != null)
            {
                flowLayoutPanel1.Width = this.ClientSize.Width;
                flowLayoutPanel1.Height = 85;

                // Resize panel1 to fill flowLayoutPanel1 width
                if (panel1 != null)
                {
                    panel1.Width = flowLayoutPanel1.Width - 6; // Account for padding
                    panel1.Height = 75;

                    // Resize TimePlaceholderText to fill panel1
                    if (TimePlaceholderText != null)
                    {
                        TimePlaceholderText.AutoSize = false;
                        TimePlaceholderText.Width = panel1.Width - 10;
                        TimePlaceholderText.Height = panel1.Height - 10;
                        TimePlaceholderText.TextAlign = ContentAlignment.MiddleCenter;
                    }
                }
            }

            // Resize CurrentTaskDisplayPanel
            if (CurrentTaskDisplayPanel != null)
            {
                CurrentTaskDisplayPanel.Width = maxWidth;
            }

            // Resize Next task panels
            if (NextTaskDisplayPanel != null)
            {
                NextTaskDisplayPanel.Width = maxWidth - 50;
                NextTaskDisplayPanel.Height = 50;

                // Resize NextTaskPlaceholder
                if (NextTaskPlaceholder != null)
                {
                    NextTaskPlaceholder.AutoSize = false;
                    NextTaskPlaceholder.Width = NextTaskDisplayPanel.Width - 20;
                    NextTaskPlaceholder.Height = NextTaskDisplayPanel.Height - 10;
                    NextTaskPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
                }

                // Resize flowLayoutPanel6
                if (flowLayoutPanel6 != null)
                {
                    flowLayoutPanel6.Width = NextTaskDisplayPanel.Width;
                    flowLayoutPanel6.Height = NextTaskDisplayPanel.Height;
                }
            }

            if (NextTask2DisplayPanel != null)
            {
                NextTask2DisplayPanel.Width = maxWidth - 100;
                NextTask2DisplayPanel.Height = 50;

                // Resize NextTask2Placeholder
                if (NextTask2Placeholder != null)
                {
                    NextTask2Placeholder.AutoSize = false;
                    NextTask2Placeholder.Width = NextTask2DisplayPanel.Width - 20;
                    NextTask2Placeholder.Height = NextTask2DisplayPanel.Height - 10;
                    NextTask2Placeholder.TextAlign = ContentAlignment.MiddleCenter;
                }

                // Resize flowLayoutPanel7
                if (flowLayoutPanel7 != null)
                {
                    flowLayoutPanel7.Width = NextTask2DisplayPanel.Width;
                    flowLayoutPanel7.Height = NextTask2DisplayPanel.Height;
                }
            }

            if (NextTask3DisplayPanel != null)
            {
                NextTask3DisplayPanel.Width = maxWidth - 150;
                NextTask3DisplayPanel.Height = 50;

                // Resize NextTask3Placeholder
                if (NextTask3Placeholder != null)
                {
                    NextTask3Placeholder.AutoSize = false;
                    NextTask3Placeholder.Width = NextTask3DisplayPanel.Width - 20;
                    NextTask3Placeholder.Height = NextTask3DisplayPanel.Height - 10;
                    NextTask3Placeholder.TextAlign = ContentAlignment.MiddleCenter;
                }

                // Resize flowLayoutPanel8
                if (flowLayoutPanel8 != null)
                {
                    flowLayoutPanel8.Width = NextTask3DisplayPanel.Width;
                    flowLayoutPanel8.Height = NextTask3DisplayPanel.Height;
                }
            }

            // Resize inner flow layout panels of CurrentTaskDisplayPanel
            if (flowLayoutPanel5 != null && CurrentTaskDisplayPanel != null)
            {
                flowLayoutPanel5.Width = CurrentTaskDisplayPanel.Width;
            }

            if (flowLayoutPanel4 != null && CurrentTaskDisplayPanel != null)
            {
                flowLayoutPanel4.Width = CurrentTaskDisplayPanel.Width;
                if (DescriptionPlaceholder != null)
                {
                    DescriptionPlaceholder.Width = flowLayoutPanel4.Width - 40;
                }
            }

            if (flowLayoutPanel3 != null && CurrentTaskDisplayPanel != null)
            {
                flowLayoutPanel3.Width = CurrentTaskDisplayPanel.Width;
                if (TaskNamePlaceholder != null)
                {
                    TaskNamePlaceholder.Width = flowLayoutPanel3.Width - 40;
                }
            }

            if (flowLayoutPanel2 != null && CurrentTaskDisplayPanel != null)
            {
                flowLayoutPanel2.Width = CurrentTaskDisplayPanel.Width;
            }
        }

        private void CenterPanel(Control panel)
        {
            if (panel != null)
            {
                panel.Left = (this.ClientSize.Width - panel.Width) / 2;
            }
        }

        private void CenterButtons()
        {
            if (panel2 == null || StopButton == null || NextTaskButton == null)
                return;

            int gap = 55;
            int totalWidth = StopButton.Width + gap + NextTaskButton.Width;
            int startX = (panel2.Width - totalWidth) / 2;
            int verticalCenter = (panel2.Height - StopButton.Height) / 2;

            StopButton.Left = startX;
            StopButton.Top = verticalCenter;

            NextTaskButton.Left = startX + StopButton.Width + gap;
            NextTaskButton.Top = verticalCenter;
        }

        private void CenterInnerContents()
        {
            // Center stars
            if (flowLayoutPanel5 != null && flowLayoutPanel10 != null)
            {
                flowLayoutPanel10.Left = (flowLayoutPanel5.Width - flowLayoutPanel10.Width) / 2;
                flowLayoutPanel10.Top = (flowLayoutPanel5.Height - flowLayoutPanel10.Height) / 2;
            }

            // Center panel1 within flowLayoutPanel1
            if (flowLayoutPanel1 != null && panel1 != null)
            {
                panel1.Left = (flowLayoutPanel1.Width - panel1.Width) / 2;
                panel1.Top = (flowLayoutPanel1.Height - panel1.Height) / 2;
            }
        }

        internal void SetUser()
        {
            if (currentUser == null)
            {
                currentUser = AppState.CurrentUser;
            }
            else
            {
                throw new InvalidOperationException("User has already been set for this Nitro_FX instance.");
            }
        }
    }
}