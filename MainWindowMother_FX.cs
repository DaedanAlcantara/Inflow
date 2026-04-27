using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Inflow
{
    public class MainWindowMother_FX : GrandmaWindow_FX
    {
        private Size normalFormSize;
        private int borderSize = 0;
        protected Panel panel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private PictureBox pictureBox1;
        private FlowLayoutPanel flowLayoutPanel4;
        private PictureBox pictureBox3;
        private Label label3;
        private FlowLayoutPanel flowLayoutPanel3;
        private PictureBox pictureBox2;
        private Label label2;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel5;
        private PictureBox pictureBox4;
        private bool isRestoringFromMinimized = false;
        private bool isInitializing = true;

        private System.Windows.Forms.Timer animationTimer;
        private int targetWidth;
        private bool isAnimating = false;
        private bool isSidebarCollapsed = false;
        private const int EXPANDED_WIDTH = 250;
        private const int COLLAPSED_WIDTH = 100;
        private const int ANIMATION_STEP = 15;
        private const int ANIMATION_INTERVAL = 5;

        public MainWindowMother_FX()
        {
            EnableDoubleBuffering();
            InitializeComponent();
            InitializeAnimation();

            if (this.Icon == null)
            {
                Application.Exit();
            }
            flowLayoutPanel5.SuspendLayout();
            this.ShowIcon = true;
            this.Padding = new Padding(borderSize);
            this.Resize += Form1_Resize;
            this.ResizeEnd += MainWindowMother_FX_ResizeEnd;

            pictureBox4.Location = new Point((Sidebar.Width - pictureBox4.Width) / 2, (flowLayoutPanel5.Height - pictureBox4.Height) / 2);

            Sidebar.BackColor = System.Drawing.ColorTranslator.FromHtml("#0E24F0");

            this.Size = new Size(1200, 800);
            this.MinimumSize = new Size(800, 600);

            // Use Shown event instead of Load for proper sizing
            this.Shown += MainWindowMother_FX_Shown;
        }

        private void MainWindowMother_FX_Shown(object? sender, EventArgs e)
        {
            isInitializing = false;

            // Store the initial form size
            if (this.WindowState == FormWindowState.Normal)
            {
                normalFormSize = this.Size;
            }

            ShowDashboard();
        }

        public Panel ContentPanel
        {
            get { return panel1; }
        }

        private void EnableDoubleBuffering()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.DoubleBuffer |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        private void InitializeAnimation()
        {
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = ANIMATION_INTERVAL;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        protected virtual void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowMother_FX));
            TitleBar = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnMinimize = new PictureBox();
            btnClose = new PictureBox();
            Sidebar = new Panel();
            flowLayoutPanel5 = new FlowLayoutPanel();
            pictureBox4 = new PictureBox();
            flowLayoutPanel4 = new FlowLayoutPanel();
            pictureBox3 = new PictureBox();
            label3 = new Label();
            flowLayoutPanel3 = new FlowLayoutPanel();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panel1 = new Panel();
            TitleBar.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnMinimize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            Sidebar.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // TitleBar
            // 
            TitleBar.BackColor = SystemColors.ControlDarkDark;
            TitleBar.Controls.Add(flowLayoutPanel1);
            TitleBar.Dock = DockStyle.Top;
            TitleBar.Location = new Point(0, 0);
            TitleBar.Name = "TitleBar";
            TitleBar.Size = new Size(1942, 41);
            TitleBar.TabIndex = 0;
            TitleBar.MouseDown += TitleBar_MouseDown;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnMinimize);
            flowLayoutPanel1.Controls.Add(btnClose);
            flowLayoutPanel1.Dock = DockStyle.Right;
            flowLayoutPanel1.Location = new Point(1862, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(80, 41);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // btnMinimize
            // 
            btnMinimize.Image = Properties.Resources.Minimize;
            btnMinimize.Location = new Point(3, 15);
            btnMinimize.Margin = new Padding(3, 15, 15, 10);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(15, 15);
            btnMinimize.SizeMode = PictureBoxSizeMode.StretchImage;
            btnMinimize.TabIndex = 0;
            btnMinimize.TabStop = false;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnClose
            // 
            btnClose.Image = Properties.Resources.Close;
            btnClose.Location = new Point(36, 15);
            btnClose.Margin = new Padding(3, 15, 15, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(15, 15);
            btnClose.SizeMode = PictureBoxSizeMode.StretchImage;
            btnClose.TabIndex = 2;
            btnClose.TabStop = false;
            btnClose.Click += btnClose_Click;
            // 
            // Sidebar
            // 
            Sidebar.BackColor = SystemColors.ActiveCaption;
            Sidebar.Controls.Add(flowLayoutPanel5);
            Sidebar.Controls.Add(flowLayoutPanel4);
            Sidebar.Controls.Add(flowLayoutPanel3);
            Sidebar.Controls.Add(flowLayoutPanel2);
            Sidebar.Dock = DockStyle.Left;
            Sidebar.Location = new Point(0, 41);
            Sidebar.Name = "Sidebar";
            Sidebar.Size = new Size(250, 1061);
            Sidebar.TabIndex = 1;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.BackColor = Color.Transparent;
            flowLayoutPanel5.Controls.Add(pictureBox4);
            flowLayoutPanel5.Dock = DockStyle.Top;
            flowLayoutPanel5.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel5.Location = new Point(0, 0);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(250, 120);
            flowLayoutPanel5.TabIndex = 3;
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.None;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(0, 0);
            pictureBox4.Margin = new Padding(0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(56, 56);
            pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox4.TabIndex = 0;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.BackColor = Color.Transparent;
            flowLayoutPanel4.Controls.Add(pictureBox3);
            flowLayoutPanel4.Controls.Add(label3);
            flowLayoutPanel4.Location = new Point(0, 288);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Padding = new Padding(30, 0, 0, 0);
            flowLayoutPanel4.Size = new Size(250, 61);
            flowLayoutPanel4.TabIndex = 2;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(35, 10);
            pictureBox3.Margin = new Padding(5, 10, 10, 5);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(32, 40);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Inter", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(80, 20);
            label3.Margin = new Padding(3, 20, 3, 15);
            label3.Name = "label3";
            label3.Size = new Size(57, 24);
            label3.TabIndex = 1;
            label3.Tag = "Nitro";
            label3.Text = "Nitro";
            label3.Click += label3_Click;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.BackColor = Color.Transparent;
            flowLayoutPanel3.Controls.Add(pictureBox2);
            flowLayoutPanel3.Controls.Add(label2);
            flowLayoutPanel3.Location = new Point(0, 221);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Padding = new Padding(30, 0, 0, 0);
            flowLayoutPanel3.Size = new Size(250, 61);
            flowLayoutPanel3.TabIndex = 1;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(30, 10);
            pictureBox2.Margin = new Padding(0, 10, 10, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(40, 40);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Inter", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(83, 20);
            label2.Margin = new Padding(3, 20, 3, 15);
            label2.Name = "label2";
            label2.Size = new Size(83, 24);
            label2.TabIndex = 1;
            label2.Tag = "Planner";
            label2.Text = "Planner";
            label2.Click += label2_Click;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.Transparent;
            flowLayoutPanel2.Controls.Add(pictureBox1);
            flowLayoutPanel2.Controls.Add(label1);
            flowLayoutPanel2.Location = new Point(0, 154);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(30, 0, 0, 0);
            flowLayoutPanel2.Size = new Size(250, 61);
            flowLayoutPanel2.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Dashboard;
            pictureBox1.Location = new Point(30, 10);
            pictureBox1.Margin = new Padding(0, 10, 10, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Inter", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(83, 20);
            label1.Margin = new Padding(3, 20, 3, 15);
            label1.Name = "label1";
            label1.Size = new Size(113, 24);
            label1.TabIndex = 1;
            label1.Tag = "Dashboard";
            label1.Text = "Dashboard";
            label1.Click += label1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Dock = DockStyle.Fill;
            panel1.ForeColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(250, 41);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10);
            panel1.Size = new Size(1692, 1061);
            panel1.TabIndex = 2;
            // 
            // MainWindowMother_FX
            // 
            ClientSize = new Size(1942, 1102);
            Controls.Add(panel1);
            Controls.Add(Sidebar);
            Controls.Add(TitleBar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "MainWindowMother_FX";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "Inflow";
            TitleBar.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnMinimize).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            Sidebar.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private Panel TitleBar;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox btnMinimize;
        private PictureBox btnClose;
        private Panel Sidebar;

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 0, 0, 0);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            // Save the current normal size before minimizing
            if (this.WindowState == FormWindowState.Normal)
            {
                if (this.Size.Width > this.MinimumSize.Width && this.Size.Height > this.MinimumSize.Height)
                {
                    normalFormSize = this.Size;
                }
            }

            // Minimize the window
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();

            // Handle restore from minimized state
            if (this.WindowState == FormWindowState.Normal && isRestoringFromMinimized)
            {
                isRestoringFromMinimized = false;
                this.BeginInvoke(new Action(() =>
                {
                    if (normalFormSize.Width > this.MinimumSize.Width && normalFormSize.Height > this.MinimumSize.Height)
                    {
                        this.Size = normalFormSize;
                    }
                }));
            }

            // Save normal size when resizing in normal state (not during initialization)
            if (!isInitializing && this.WindowState == FormWindowState.Normal)
            {
                if (this.Size.Width > this.MinimumSize.Width && this.Size.Height > this.MinimumSize.Height)
                {
                    normalFormSize = this.Size;
                }
            }
        }

        private void MainWindowMother_FX_ResizeEnd(object sender, EventArgs e)
        {
            if (!isInitializing && this.WindowState == FormWindowState.Normal)
            {
                if (this.Size.Width > this.MinimumSize.Width && this.Size.Height > this.MinimumSize.Height)
                {
                    normalFormSize = this.Size;
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (isAnimating) return;

            targetWidth = isSidebarCollapsed ? EXPANDED_WIDTH : COLLAPSED_WIDTH;

            if (targetWidth == EXPANDED_WIDTH)
            {
                UpdateLabelsVisibility(true);
            }

            isAnimating = true;
            animationTimer.Start();

            // Adjust minimum form size when sidebar changes
            int newMinWidth = 400 + (isSidebarCollapsed ? COLLAPSED_WIDTH : EXPANDED_WIDTH);
            this.MinimumSize = new Size(newMinWidth, 400);
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int newWidth;

            if (Sidebar.Width < targetWidth)
            {
                newWidth = Math.Min(Sidebar.Width + ANIMATION_STEP, targetWidth);
                Sidebar.Width = newWidth;

                if (Sidebar.Width >= targetWidth)
                {
                    CompleteAnimation();
                }
                else
                {
                    UpdatePictureBoxPosition(newWidth);
                    bool shouldShowText = newWidth > (EXPANDED_WIDTH + COLLAPSED_WIDTH) / 2;
                    if (shouldShowText != !isSidebarCollapsed)
                    {
                        UpdateLabelsVisibility(shouldShowText);
                    }

                    // Update dashboard during animation
                    if (panel1.Controls.Count > 0 && panel1.Controls[0] is Dashboard_FX dashboard)
                    {
                        dashboard.ResizeContent();
                    }
                }
            }
            else if (Sidebar.Width > targetWidth)
            {
                newWidth = Math.Max(Sidebar.Width - ANIMATION_STEP, targetWidth);
                Sidebar.Width = newWidth;

                if (Sidebar.Width <= targetWidth)
                {
                    CompleteAnimation();
                }
                else
                {
                    UpdatePictureBoxPosition(newWidth);
                    bool shouldShowText = newWidth > (EXPANDED_WIDTH + COLLAPSED_WIDTH) / 2;
                    if (shouldShowText != !isSidebarCollapsed)
                    {
                        UpdateLabelsVisibility(shouldShowText);
                    }

                    // Update dashboard during animation
                    if (panel1.Controls.Count > 0 && panel1.Controls[0] is Dashboard_FX dashboard)
                    {
                        dashboard.ResizeContent();
                    }
                }
            }
        }

        private void CompleteAnimation()
        {
            animationTimer.Stop();
            isAnimating = false;
            isSidebarCollapsed = !isSidebarCollapsed;

            UpdatePictureBoxPosition(Sidebar.Width);
            UpdateLabelsVisibility(!isSidebarCollapsed);

            this.PerformLayout();

            // Notify dashboard of size change
            if (panel1.Controls.Count > 0 && panel1.Controls[0] is Dashboard_FX dashboard)
            {
                dashboard.ResizeContent();
                dashboard.PerformLayout();
            }
        }

        private void UpdatePictureBoxPosition(int currentWidth)
        {
            pictureBox4.Location = new Point(
                (currentWidth - pictureBox4.Width) / 2,
                (flowLayoutPanel5.Height - pictureBox4.Height) / 2
            );
        }

        private void UpdateLabelsVisibility(bool showText)
        {
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();

            if (showText)
            {
                foreach (Label label in flowLayoutPanel2.Controls.OfType<Label>())
                {
                    label.Text = " " + label.Tag?.ToString();
                    label.ImageAlign = ContentAlignment.MiddleLeft;
                    label.Padding = new Padding(10, 0, 0, 0);
                }

                foreach (Label label in flowLayoutPanel3.Controls.OfType<Label>())
                {
                    label.Text = " " + label.Tag?.ToString();
                    label.ImageAlign = ContentAlignment.MiddleLeft;
                    label.Padding = new Padding(10, 0, 0, 0);
                }

                foreach (Label label in flowLayoutPanel4.Controls.OfType<Label>())
                {
                    label.Text = " " + label.Tag?.ToString();
                    label.ImageAlign = ContentAlignment.MiddleLeft;
                    label.Padding = new Padding(10, 0, 0, 0);
                }
            }
            else
            {
                foreach (Label label in flowLayoutPanel2.Controls.OfType<Label>())
                {
                    label.Text = "";
                    label.ImageAlign = ContentAlignment.MiddleCenter;
                    label.Padding = new Padding(0);
                }

                foreach (Label label in flowLayoutPanel3.Controls.OfType<Label>())
                {
                    label.Text = "";
                    label.ImageAlign = ContentAlignment.MiddleCenter;
                    label.Padding = new Padding(0);
                }

                foreach (Label label in flowLayoutPanel4.Controls.OfType<Label>())
                {
                    label.Text = "";
                    label.ImageAlign = ContentAlignment.MiddleCenter;
                    label.Padding = new Padding(0);
                }
            }

            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel4.ResumeLayout(false);

            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel4.PerformLayout();
        }

        private void CollapseMenu(bool collapse)
        {
            if (collapse)
            {
                Sidebar.Width = COLLAPSED_WIDTH;
                UpdateLabelsVisibility(false);
            }
            else
            {
                Sidebar.Width = EXPANDED_WIDTH;
                UpdateLabelsVisibility(true);
            }

            UpdatePictureBoxPosition(Sidebar.Width);
            isSidebarCollapsed = collapse;
        }

        private void ShowDashboard()
        {
            panel1.Controls.Clear();

            var dashboard = new Dashboard_FX();
            dashboard.Dock = DockStyle.Fill;
            if (AppState.CurrentUser != null)
            {
                dashboard.SetUser();
            }

            panel1.Controls.Add(dashboard);
            dashboard.PerformLayout();

            if (!isInitializing && this.WindowState == FormWindowState.Normal)
            {
                if (normalFormSize.Width > this.MinimumSize.Width)
                {
                    this.Size = normalFormSize;
                }
            }
        }

        // This method is kept for compatibility but is not used externally.
        // The dashboard and planner read AppState.CurrentUser directly.
        internal void SetUser()
        {
            if (AppState.CurrentUser != null && panel1.Controls.Count > 0 && panel1.Controls[0] is Dashboard_FX dashboard)
            {
                dashboard.SetUser();
            }
        }

        private void ShowPlanner()
        {
            panel1.Controls.Clear();

            var planner = new Planner_FX();
            planner.Dock = DockStyle.Fill;
            if (AppState.CurrentUser != null)
            {
                planner.SetUser();
            }

            panel1.Controls.Add(planner);
            planner.PerformLayout();

            if (!isInitializing && this.WindowState == FormWindowState.Normal)
            {
                if (normalFormSize.Width > this.MinimumSize.Width)
                {
                    this.Size = normalFormSize;
                }
            }
        }

        private void ShowNitro()
        {
            panel1.Controls.Clear();

            var nitro = new Label
            var nitro = new Nitro_FX();
            nitro.Dock = DockStyle.Fill;
            if (currentUser != null)
            {
                nitro.SetUser(currentUser);
            }

            panel1.Controls.Add(nitro);

            panel1.PerformLayout();

            if (!isInitializing && this.WindowState == FormWindowState.Normal)
            {
                if (normalFormSize.Width > this.MinimumSize.Width)
                {
                    this.Size = normalFormSize;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ShowPlanner();
        }
        public void RefreshCurrentContent()
        {
            if (panel1.Controls.Count > 0 && panel1.Controls[0] is Dashboard_FX dashboard)
            {
                dashboard.SetUser();   // re‑reads AppState.CurrentUser and updates tasks
                dashboard.UpdateCurrentAndNextTasks();
            }
            else if (panel1.Controls.Count > 0 && panel1.Controls[0] is Planner_FX planner)
            {
                planner.SetUser();     // re‑reads AppState.CurrentUser and refreshes task list
                planner.RefreshTaskDisplay();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            ShowNitro();
        }
    }
}