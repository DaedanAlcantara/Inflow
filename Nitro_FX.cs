using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Inflow
{
    public partial class Nitro_FX : UserControl
    {
        // ── Variables ───────────────────────────────────────────────────────
        private User_BX currentUser;
        private List<Task_BX> taskSequence = new List<Task_BX>();
        private int currentTaskIndex = 0;

        // Stats Tracking
        private int finishedCount = 0;
        private int droppedCount = 0;
        private bool isPausedByNavigation = false;
        private int pausedRemainingSeconds = 0;

        private System.Windows.Forms.Timer taskTimer;
        private bool isPaused = false;



        public Nitro_FX()
        {
            InitializeComponent();
            SetupTimer();
        }

        private void Nitro_FX_Load(object sender, EventArgs e)
        {
            currentUser = AppState.CurrentUser;

            LoadTasksToQueue();

            if (taskSequence != null && taskSequence.Count > 0)
            {
                currentTaskIndex = 0;
                DisplayCurrentTask();
            }
            else
            {
                FinishTask();
            }

            SetUser();               // load tasks if needed
            CenterAllControls();

            // Delay to ensure layout is fully settled before colors are painted
            System.Windows.Forms.Timer colorTimer = new System.Windows.Forms.Timer { Interval = 100 };
            colorTimer.Tick += (s, ev) =>
            {
                colorTimer.Stop();
                colorTimer.Dispose();
                if (!this.IsDisposed && currentTaskIndex < taskSequence.Count)
                    ApplyCurrentTaskColor(taskSequence[currentTaskIndex].CardColor);
            };
            colorTimer.Start();
        }

        private void SetupTimer()
        {
            if (taskTimer == null)
            {
                taskTimer = new System.Windows.Forms.Timer();
                taskTimer.Interval = 1000;
            }
            taskTimer.Tick -= Timer_Tick_Logic;
            taskTimer.Tick += Timer_Tick_Logic;
        }

        private void Timer_Tick_Logic(object sender, EventArgs e)
        {
            if (AppState.NitroElapsedSeconds <= 0)
            {
                taskTimer.Stop();
                MessageBox.Show(this, "Time is up! Task dropped.", "Inflow Nitro",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AdvanceTask(isFinished: false);
                return;
            }

            AppState.NitroElapsedSeconds--;
            UpdateTimerDisplay();
        }

        private void UpdateTimerDisplay()
        {
            if (TimePlaceholderText != null)
            {
                TimeSpan remaining = TimeSpan.FromSeconds(AppState.NitroElapsedSeconds);
                TimePlaceholderText.Text = remaining.ToString(@"hh\:mm\:ss");
            }
            if (panel1 != null)
            {
                if (AppState.NitroElapsedSeconds <= 10)
                {
                    panel1.BackColor = Color.Red;
                }

            }
        }

        // ── Core Task Management ─────────────────────────────────────────────
        private void DisplayCurrentTask()
        {
            if (currentTaskIndex < taskSequence.Count)
            {
                if (flowLayoutPanel10 != null)
                {
                    flowLayoutPanel10.Visible = true;
                }
                var task = taskSequence[currentTaskIndex];

                if (TaskNamePlaceholder != null) TaskNamePlaceholder.Text = task.Name;
                if (DescriptionPlaceholder != null) DescriptionPlaceholder.Text = task.Description;
                ApplyCurrentTaskColor(task.CardColor);
                isPaused = false;
                isPausedByNavigation = false;
                taskTimer.Stop();

                int taskSeconds = (int)task.Duration.TotalSeconds;
                AppState.NitroElapsedSeconds = taskSeconds;

                if (TimePlaceholderText != null)
                {
                    TimePlaceholderText.ForeColor = Color.White;
                    UpdateTimerDisplay();
                }

                taskTimer.Start();

                UpdatePriorityStars(task.Priority);
                UpdateQueuePreview();
                CenterAllControls();
            }
            else
            {
                FinishTask();
            }
        }


        private void ApplyCurrentTaskColor(Color taskColor)
        {
            // Only change the current task panel
            if (CurrentTaskDisplayPanel != null && !CurrentTaskDisplayPanel.IsDisposed)
                CurrentTaskDisplayPanel.BackColor = taskColor;

            if (NextTaskDisplayPanel != null && !NextTaskDisplayPanel.IsDisposed)
                NextTaskDisplayPanel.BackColor = Color.DarkGray;
            if (NextTask2DisplayPanel != null && !NextTask2DisplayPanel.IsDisposed)
                NextTask2DisplayPanel.BackColor = Color.LightGray;
            if (NextTask3DisplayPanel != null && !NextTask3DisplayPanel.IsDisposed)
                NextTask3DisplayPanel.BackColor = Color.Gainsboro;
        }

        // Blend a color toward white by the given factor (0 = original, 1 = white)
        private Color LightenColor(Color color, float factor)
        {
            int r = color.R + (int)((255 - color.R) * factor);
            int g = color.G + (int)((255 - color.G) * factor);
            int b = color.B + (int)((255 - color.B) * factor);
            return Color.FromArgb(Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
        }

        private void AdvanceTask(bool isFinished)
        {
            if (currentTaskIndex >= taskSequence.Count) return;

            taskTimer.Stop();

            if (isFinished)
            {
                finishedCount++;
                AppState.TotalFinishedTasks++;

                AppState.ConsecutiveFinishes++;
                if (AppState.ConsecutiveFinishes % 2 == 0)
                {
                    AppState.CurrentStreak++;
                }
            }
            else
            {
                droppedCount++;
                AppState.TotalDroppedTasks++;
                AppState.ConsecutiveFinishes = 0;
                AppState.CurrentStreak = 0;
            }

            // Refresh Dashboard counters immediately
            var mainForm = System.Windows.Forms.Application.OpenForms.OfType<MainWindowMother_FX>().FirstOrDefault();
            mainForm?.RefreshCurrentContent();

            currentUser?.RemoveTask(taskSequence[currentTaskIndex]);

            currentTaskIndex++;
            DisplayCurrentTask();
        }

        private void FinishTask(bool silent = false)
        {
            taskTimer?.Stop();
            if (TaskNamePlaceholder != null) TaskNamePlaceholder.Text = "TASK CLEARED";
            if (DescriptionPlaceholder != null) DescriptionPlaceholder.Text = "All assigned tasks are now finished";
            if (TaskNamePlaceholder != null)
            {
                if (flowLayoutPanel10 != null)
                    flowLayoutPanel10.Visible = false;
            }
            if (TimePlaceholderText != null)
            {
                TimePlaceholderText.Text = "00:00:00";
                TimePlaceholderText.ForeColor = Color.White;
                panel1.BackColor = Color.Green;
            }



            var mainForm = System.Windows.Forms.Application.OpenForms.OfType<MainWindowMother_FX>().FirstOrDefault();
            if (mainForm != null)
            {
                mainForm.DeactivateNitro();
            }
        }

        // ── Controls Logic (PictureBoxes) ───────────────────────────────────
        private void pictureBox1_Click(object sender, EventArgs e) // Drop
        {
            taskTimer.Stop();
            DialogResult result = MessageBox.Show("Are you sure you want to drop this task?",
                "Confirm Drop", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                AdvanceTask(isFinished: false);
            }
            else
            {
                if (!isPaused) taskTimer.Start();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e) // Next (Finish)
        {
            taskTimer.Stop();
            DialogResult result = MessageBox.Show("Mark this task as finished and move to the next?",
                "Complete Task", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AdvanceTask(isFinished: true);
            }
            else
            {
                if (!isPaused) taskTimer.Start();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e) // Pause/Resume
        {
            if (!isPaused)
            {
                taskTimer.Stop();
                isPaused = true;
                isPausedByNavigation = false;

                if (TimePlaceholderText != null)
                {
                    TimePlaceholderText.Text = "PAUSED";
                    TimePlaceholderText.ForeColor = Color.White;
                    panel1.BackColor = Color.Orange;
                }

                MessageBox.Show("The task has been PAUSED.\nClick OK when you are ready to resume.",
                    "Task Paused", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResumeTask();
                if (panel1 != null) panel1.BackColor = Color.Gray;
            }
            else
            {
                ResumeTask();
            }
        }

        private void ResumeTask()
        {
            isPaused = false;
            isPausedByNavigation = false;
            if (TimePlaceholderText != null) TimePlaceholderText.ForeColor = Color.White;
            UpdateTimerDisplay();
            taskTimer.Start();
        }

        private void LoadTasksToQueue()
        {
            taskSequence.Clear();
            if (currentUser == null) return;
            if (currentUser.MorningTasks != null) taskSequence.AddRange(currentUser.MorningTasks);
            if (currentUser.AfternoonTasks != null) taskSequence.AddRange(currentUser.AfternoonTasks);
        }

        private void UpdatePriorityStars(int priority)
        {
            if (flowLayoutPanel10 == null) return;
            for (int i = 0; i < flowLayoutPanel10.Controls.Count; i++)
                flowLayoutPanel10.Controls[i].Visible = (i < priority);
        }

        private void UpdateQueuePreview()
        {
            if (NextTaskPlaceholder != null)
                NextTaskPlaceholder.Text = (currentTaskIndex + 1 < taskSequence.Count) ? taskSequence[currentTaskIndex + 1].Name : "---";
            if (NextTask2Placeholder != null)
                NextTask2Placeholder.Text = (currentTaskIndex + 2 < taskSequence.Count) ? taskSequence[currentTaskIndex + 2].Name : "---";
            if (NextTask3Placeholder != null)
                NextTask3Placeholder.Text = (currentTaskIndex + 3 < taskSequence.Count) ? taskSequence[currentTaskIndex + 3].Name : "---";
        }

        // ── Layout Methods (unchanged) ───────────────────────────────────────
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterAllControls();
        }

        private void CenterAllControls()
        {
            ResizePanelsToWidth();
            CenterPanel(CurrentTaskDisplayPanel);
            CenterPanel(NextTaskDisplayPanel);
            CenterPanel(NextTask2DisplayPanel);
            CenterPanel(NextTask3DisplayPanel);
            CenterButtons();
            CenterInnerContents();
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
            int maxWidth = this.ClientSize.Width - 40;
            if (flowLayoutPanel1 != null)
            {
                flowLayoutPanel1.Width = this.ClientSize.Width;
                flowLayoutPanel1.Height = 85;
                if (panel1 != null)
                {
                    panel1.Width = flowLayoutPanel1.Width - 6;
                    panel1.Height = 75;
                    if (TimePlaceholderText != null)
                    {
                        TimePlaceholderText.AutoSize = false;
                        TimePlaceholderText.Width = panel1.Width - 10;
                        TimePlaceholderText.Height = panel1.Height - 10;
                        TimePlaceholderText.TextAlign = ContentAlignment.MiddleCenter;
                    }
                }
            }
            if (CurrentTaskDisplayPanel != null) CurrentTaskDisplayPanel.Width = maxWidth;
            if (NextTaskDisplayPanel != null)
            {
                NextTaskDisplayPanel.Width = maxWidth - 50;
                NextTaskDisplayPanel.Height = 50;
                if (NextTaskPlaceholder != null)
                {
                    NextTaskPlaceholder.AutoSize = false;
                    NextTaskPlaceholder.Width = NextTaskDisplayPanel.Width - 20;
                    NextTaskPlaceholder.Height = NextTaskDisplayPanel.Height - 10;
                    NextTaskPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
                }
                if (flowLayoutPanel6 != null) { flowLayoutPanel6.Width = NextTaskDisplayPanel.Width; flowLayoutPanel6.Height = NextTaskDisplayPanel.Height; }
            }
            if (NextTask2DisplayPanel != null)
            {
                NextTask2DisplayPanel.Width = maxWidth - 100;
                NextTask2DisplayPanel.Height = 50;
                if (NextTask2Placeholder != null)
                {
                    NextTask2Placeholder.AutoSize = false;
                    NextTask2Placeholder.Width = NextTask2DisplayPanel.Width - 20;
                    NextTask2Placeholder.Height = NextTask2DisplayPanel.Height - 10;
                    NextTask2Placeholder.TextAlign = ContentAlignment.MiddleCenter;
                }
                if (flowLayoutPanel7 != null) { flowLayoutPanel7.Width = NextTask2DisplayPanel.Width; flowLayoutPanel7.Height = NextTask2DisplayPanel.Height; }
            }
            if (NextTask3DisplayPanel != null)
            {
                NextTask3DisplayPanel.Width = maxWidth - 150;
                NextTask3DisplayPanel.Height = 50;
                if (NextTask3Placeholder != null)
                {
                    NextTask3Placeholder.AutoSize = false;
                    NextTask3Placeholder.Width = NextTask3DisplayPanel.Width - 20;
                    NextTask3Placeholder.Height = NextTask3DisplayPanel.Height - 10;
                    NextTask3Placeholder.TextAlign = ContentAlignment.MiddleCenter;
                }
                if (flowLayoutPanel8 != null) { flowLayoutPanel8.Width = NextTask3DisplayPanel.Width; flowLayoutPanel8.Height = NextTask3DisplayPanel.Height; }
            }

            if (flowLayoutPanel5 != null && CurrentTaskDisplayPanel != null) flowLayoutPanel5.Width = CurrentTaskDisplayPanel.Width;
            if (flowLayoutPanel4 != null && CurrentTaskDisplayPanel != null)
            {
                flowLayoutPanel4.Width = CurrentTaskDisplayPanel.Width;
                if (DescriptionPlaceholder != null) DescriptionPlaceholder.Width = flowLayoutPanel4.Width - 40;
            }
            if (flowLayoutPanel3 != null && CurrentTaskDisplayPanel != null)
            {
                flowLayoutPanel3.Width = CurrentTaskDisplayPanel.Width;
                if (TaskNamePlaceholder != null) TaskNamePlaceholder.Width = flowLayoutPanel3.Width - 40;
            }
            if (flowLayoutPanel2 != null && CurrentTaskDisplayPanel != null) flowLayoutPanel2.Width = CurrentTaskDisplayPanel.Width;
        }

        private void CenterPanel(Control panel)
        {
            if (panel != null) panel.Left = (this.ClientSize.Width - panel.Width) / 2;
        }

        private void CenterButtons()
        {
            if (panel2 == null || StopButton == null || NextTaskButton == null) return;
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
            if (flowLayoutPanel5 != null && flowLayoutPanel10 != null)
            {
                flowLayoutPanel10.Left = (flowLayoutPanel5.Width - flowLayoutPanel10.Width) / 2;
                flowLayoutPanel10.Top = (flowLayoutPanel5.Height - flowLayoutPanel10.Height) / 2;
            }
            if (flowLayoutPanel1 != null && panel1 != null)
            {
                panel1.Left = (flowLayoutPanel1.Width - panel1.Width) / 2;
                panel1.Top = (flowLayoutPanel1.Height - panel1.Height) / 2;
            }
        }

        // ── User management and navigation handling ───────────────────────────
        internal void SetUser()
        {
            if (currentUser == null) currentUser = AppState.CurrentUser;

            // Only reload tasks if no task is currently in progress (timer not running, no active task index)
            bool isTaskActive = (taskTimer != null && taskTimer.Enabled) ||
                                (currentTaskIndex < taskSequence.Count && AppState.NitroElapsedSeconds > 0);
            if (!isTaskActive)
            {
                LoadTasksToQueue();
                if (taskSequence.Count > 0)
                {
                    currentTaskIndex = 0;
                    DisplayCurrentTask();
                }
                else
                {
                    FinishTask();
                }
            }
        }

        public void ReloadTasks()
        {
            if (currentUser == null) currentUser = AppState.CurrentUser;
            if (currentUser == null) return;

            // Stop any running timer
            taskTimer?.Stop();
            isPaused = false;
            isPausedByNavigation = false;

            // Clear and reload the task sequence
            taskSequence.Clear();
            taskSequence.AddRange(currentUser.MorningTasks);
            taskSequence.AddRange(currentUser.AfternoonTasks);
            currentTaskIndex = 0;

            if (taskSequence.Count > 0)
            {
                DisplayCurrentTask();
            }
            else
            {
                FinishTask(silent: true);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.DesignMode) return;

            if (this.Visible)
            {
                // Check if there are any tasks in the user object
                bool hasTasks = currentUser != null &&
                                (currentUser.MorningTasks.Any() || currentUser.AfternoonTasks.Any());

                // Check if the local view is in a finished state (no tasks in queue or showing "TASK CLEARED")
                bool isFinishedState = taskSequence.Count == 0 ||
                                        (TaskNamePlaceholder != null && TaskNamePlaceholder.Text == "TASK CLEARED");

                // If there are tasks but Nitro is showing the finished screen, reload
                if (hasTasks && isFinishedState)
                {
                    ReloadTasks();
                    return;
                }

                // Otherwise, handle normal resume (timer pause/resume)
                if (isPausedByNavigation)
                {
                    isPausedByNavigation = false;
                    isPaused = false;
                    AppState.NitroElapsedSeconds = pausedRemainingSeconds;
                    UpdateTimerDisplay();
                    taskTimer.Start();
                }
                else if (taskTimer != null && !taskTimer.Enabled && !isPaused &&
                         currentTaskIndex < taskSequence.Count && AppState.NitroElapsedSeconds > 0)
                {
                    taskTimer.Start();
                }
            }
            else
            {
                // Navigating away – pause timer if running
                if (taskTimer != null && taskTimer.Enabled)
                {
                    taskTimer.Stop();
                    isPausedByNavigation = true;
                    pausedRemainingSeconds = AppState.NitroElapsedSeconds;
                }
            }
        }
    }
}