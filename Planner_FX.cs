using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Inflow
{
    public partial class Planner_FX : UserControl
    {
        private User_BX currentUser;

        public Planner_FX()
        {
            InitializeComponent();

            // Enable double buffering for smoother rendering
            this.DoubleBuffered = true;
            EnableDoubleBufferingForAllControls();

            // Set default sizes
            this.Size = new Size(632, 567);

            // Initialize task display
            InitializeTaskDisplay();

            // Setup button events
            SetupButtonEvents();

            // Ensure AutoSort button is visible
            if (AutoSortBTN != null)
            {
                AutoSortBTN.Visible = true;
                AutoSortBTN.BringToFront();
            }

            // Subscribe to resize event
            this.Resize += Planner_FX_Resize;

            var durationBoxes = new RoundedTextBox_CMP[] { MorningStartHour1, textBox2, textBox3, textBox4 };

            foreach (var box in durationBoxes)
            {
                box.MaxLength = 1;
                box.NumericOnly = true;
            }

            textBox3.KeyPress += RestrictMinuteTens;
        }

        private void RestrictMinuteTens(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (backspace, delete, arrow keys, tab, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Only allow digits 0-5
            if (!char.IsDigit(e.KeyChar) || int.Parse(e.KeyChar.ToString()) > 5)
                e.Handled = true;
        }


        private void InitializeComponent()
        {
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel1 = new Panel();
            taskListPanel = new FlowLayoutPanel();
            AutoSortBTN = new Panel();
            pictureBox1 = new PictureBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            label1 = new Label();
            usernameTextbox = new RoundedTextBox_CMP();
            flowLayoutPanel4 = new FlowLayoutPanel();
            label2 = new Label();
            roundedTextBox_cmp1 = new RoundedTextBox_CMP();
            flowLayoutPanel5 = new FlowLayoutPanel();
            flowLayoutPanel6 = new FlowLayoutPanel();
            label3 = new Label();
            AMButton = new RadioButton();
            PMButton = new RadioButton();
            flowLayoutPanel7 = new FlowLayoutPanel();
            label4 = new Label();
            TimePicker1 = new FlowLayoutPanel();
            MorningStartHour1 = new RoundedTextBox_CMP();
            textBox2 = new RoundedTextBox_CMP();
            label5 = new Label();
            textBox3 = new RoundedTextBox_CMP();
            textBox4 = new RoundedTextBox_CMP();
            flowLayoutPanel8 = new FlowLayoutPanel();
            label6 = new Label();
            flowLayoutPanel9 = new FlowLayoutPanel();
            star1 = new PictureBox();
            star2 = new PictureBox();
            star3 = new PictureBox();
            star4 = new PictureBox();
            star5 = new PictureBox();
            flowLayoutPanel10 = new FlowLayoutPanel();
            button1 = new Button();
            button2 = new Button();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            taskListPanel.SuspendLayout();
            AutoSortBTN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            TimePicker1.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            flowLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)star1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)star2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)star3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)star4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)star5).BeginInit();
            flowLayoutPanel10.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.Transparent;
            flowLayoutPanel1.Controls.Add(panel1);
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.Size = new Size(332, 567);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.AppWorkspace;
            panel1.Controls.Add(taskListPanel);
            panel1.Location = new Point(13, 13);
            panel1.Name = "panel1";
            panel1.Size = new Size(303, 539);
            panel1.TabIndex = 0;
            // 
            // taskListPanel
            // 
            taskListPanel.Controls.Add(AutoSortBTN);
            taskListPanel.Location = new Point(0, 3);
            taskListPanel.Name = "taskListPanel";
            taskListPanel.Size = new Size(303, 536);
            taskListPanel.TabIndex = 0;
            // 
            // AutoSortBTN
            // 
            AutoSortBTN.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AutoSortBTN.BackColor = Color.White;
            AutoSortBTN.Controls.Add(pictureBox1);
            AutoSortBTN.Location = new Point(3, 3);
            AutoSortBTN.Name = "AutoSortBTN";
            AutoSortBTN.Size = new Size(30, 30);
            AutoSortBTN.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.AutoSort;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(25, 25);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.Transparent;
            flowLayoutPanel2.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel4);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel5);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel10);
            flowLayoutPanel2.Dock = DockStyle.Right;
            flowLayoutPanel2.Location = new Point(332, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(10);
            flowLayoutPanel2.Size = new Size(300, 567);
            flowLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel3.Controls.Add(label1);
            flowLayoutPanel3.Controls.Add(usernameTextbox);
            flowLayoutPanel3.Location = new Point(13, 13);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Padding = new Padding(5);
            flowLayoutPanel3.Size = new Size(269, 73);
            flowLayoutPanel3.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            label1.Location = new Point(8, 5);
            label1.Name = "label1";
            label1.Size = new Size(103, 20);
            label1.TabIndex = 3;
            label1.Text = "Task Name";
            // 
            // usernameTextbox
            // 
            usernameTextbox.Anchor = AnchorStyles.None;
            usernameTextbox.BackColor = SystemColors.ControlLight;
            usernameTextbox.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            usernameTextbox.ForeColor = SystemColors.WindowText;
            usernameTextbox.Location = new Point(15, 35);
            usernameTextbox.Margin = new Padding(10);
            usernameTextbox.MaxLength = 32767;
            usernameTextbox.Name = "usernameTextbox";
            usernameTextbox.NumericOnly = false;
            usernameTextbox.Padding = new Padding(10, 8, 10, 8);
            usernameTextbox.Size = new Size(237, 31);
            usernameTextbox.TabIndex = 2;
            usernameTextbox.Load += usernameTextbox_Load;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel4.Controls.Add(label2);
            flowLayoutPanel4.Controls.Add(roundedTextBox_cmp1);
            flowLayoutPanel4.Location = new Point(13, 92);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Padding = new Padding(5);
            flowLayoutPanel4.Size = new Size(269, 142);
            flowLayoutPanel4.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            label2.Location = new Point(8, 5);
            label2.Name = "label2";
            label2.Size = new Size(152, 20);
            label2.TabIndex = 3;
            label2.Text = "Task Description";
            // 
            // roundedTextBox_cmp1
            // 
            roundedTextBox_cmp1.Anchor = AnchorStyles.None;
            roundedTextBox_cmp1.BackColor = SystemColors.ControlLight;
            roundedTextBox_cmp1.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedTextBox_cmp1.ForeColor = SystemColors.WindowText;
            roundedTextBox_cmp1.Location = new Point(15, 35);
            roundedTextBox_cmp1.Margin = new Padding(10);
            roundedTextBox_cmp1.MaxLength = 32767;
            roundedTextBox_cmp1.Name = "roundedTextBox_cmp1";
            roundedTextBox_cmp1.NumericOnly = false;
            roundedTextBox_cmp1.Padding = new Padding(10, 8, 10, 8);
            roundedTextBox_cmp1.Size = new Size(237, 100);
            roundedTextBox_cmp1.TabIndex = 2;
            roundedTextBox_cmp1.Load += roundedTextBox_cmp1_Load;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Controls.Add(flowLayoutPanel6);
            flowLayoutPanel5.Controls.Add(flowLayoutPanel7);
            flowLayoutPanel5.Controls.Add(flowLayoutPanel8);
            flowLayoutPanel5.Location = new Point(13, 240);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(269, 253);
            flowLayoutPanel5.TabIndex = 2;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Controls.Add(label3);
            flowLayoutPanel6.Controls.Add(AMButton);
            flowLayoutPanel6.Controls.Add(PMButton);
            flowLayoutPanel6.Location = new Point(3, 3);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(133, 103);
            flowLayoutPanel6.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 4;
            label3.Text = "Time";
            // 
            // AMButton
            // 
            AMButton.AutoSize = true;
            AMButton.Font = new Font("Microsoft Sans Serif", 10.2F);
            AMButton.Location = new Point(3, 23);
            AMButton.Name = "AMButton";
            AMButton.Size = new Size(90, 24);
            AMButton.TabIndex = 5;
            AMButton.TabStop = true;
            AMButton.Text = "Morning";
            AMButton.UseVisualStyleBackColor = true;
            AMButton.CheckedChanged += AMButton_CheckedChanged;
            // 
            // PMButton
            // 
            PMButton.AutoSize = true;
            PMButton.Font = new Font("Microsoft Sans Serif", 10.2F);
            PMButton.Location = new Point(3, 53);
            PMButton.Name = "PMButton";
            PMButton.Size = new Size(102, 24);
            PMButton.TabIndex = 6;
            PMButton.TabStop = true;
            PMButton.Text = "Afternoon";
            PMButton.UseVisualStyleBackColor = true;
            PMButton.CheckedChanged += PMButton_CheckedChanged;
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.Controls.Add(label4);
            flowLayoutPanel7.Controls.Add(TimePicker1);
            flowLayoutPanel7.Location = new Point(3, 112);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Size = new Size(213, 79);
            flowLayoutPanel7.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(81, 20);
            label4.TabIndex = 4;
            label4.Text = "Duration";
            // 
            // TimePicker1
            // 
            TimePicker1.BackColor = Color.Transparent;
            TimePicker1.Controls.Add(MorningStartHour1);
            TimePicker1.Controls.Add(textBox2);
            TimePicker1.Controls.Add(label5);
            TimePicker1.Controls.Add(textBox3);
            TimePicker1.Controls.Add(textBox4);
            TimePicker1.Location = new Point(3, 23);
            TimePicker1.Name = "TimePicker1";
            TimePicker1.Size = new Size(201, 49);
            TimePicker1.TabIndex = 9;
            // 
            // MorningStartHour1
            // 
            MorningStartHour1.Anchor = AnchorStyles.None;
            MorningStartHour1.BackColor = SystemColors.ControlLight;
            MorningStartHour1.Font = new Font("Segoe UI", 13.8F);
            MorningStartHour1.Location = new Point(3, 3);
            MorningStartHour1.MaxLength = 32767;
            MorningStartHour1.Name = "MorningStartHour1";
            MorningStartHour1.NumericOnly = false;
            MorningStartHour1.Padding = new Padding(10, 8, 10, 8);
            MorningStartHour1.Size = new Size(37, 46);
            MorningStartHour1.TabIndex = 0;
            MorningStartHour1.Load += MorningStartHour1_Load;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.BackColor = SystemColors.ControlLight;
            textBox2.Font = new Font("Segoe UI", 13.8F);
            textBox2.Location = new Point(46, 3);
            textBox2.MaxLength = 32767;
            textBox2.Name = "textBox2";
            textBox2.NumericOnly = false;
            textBox2.Padding = new Padding(10, 8, 10, 8);
            textBox2.Size = new Size(37, 46);
            textBox2.TabIndex = 1;
            textBox2.Load += textBox2_Load;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(89, 8);
            label5.Name = "label5";
            label5.Size = new Size(23, 36);
            label5.TabIndex = 2;
            label5.Text = ":";
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.None;
            textBox3.BackColor = SystemColors.ControlLight;
            textBox3.Font = new Font("Segoe UI", 13.8F);
            textBox3.Location = new Point(118, 3);
            textBox3.MaxLength = 32767;
            textBox3.Name = "textBox3";
            textBox3.NumericOnly = false;
            textBox3.Padding = new Padding(10, 8, 10, 8);
            textBox3.Size = new Size(37, 46);
            textBox3.TabIndex = 3;
            textBox3.Load += textBox3_Load;
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.None;
            textBox4.BackColor = SystemColors.ControlLight;
            textBox4.Font = new Font("Segoe UI", 13.8F);
            textBox4.Location = new Point(161, 3);
            textBox4.MaxLength = 32767;
            textBox4.Name = "textBox4";
            textBox4.NumericOnly = false;
            textBox4.Padding = new Padding(10, 8, 10, 8);
            textBox4.Size = new Size(37, 46);
            textBox4.TabIndex = 4;
            textBox4.Load += textBox4_Load;
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.Controls.Add(label6);
            flowLayoutPanel8.Controls.Add(flowLayoutPanel9);
            flowLayoutPanel8.Location = new Point(3, 197);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            flowLayoutPanel8.Size = new Size(213, 59);
            flowLayoutPanel8.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            label6.Location = new Point(3, 0);
            label6.Name = "label6";
            label6.Size = new Size(130, 20);
            label6.TabIndex = 4;
            label6.Text = "Priority Rating";
            // 
            // flowLayoutPanel9
            // 
            flowLayoutPanel9.Controls.Add(star1);
            flowLayoutPanel9.Controls.Add(star2);
            flowLayoutPanel9.Controls.Add(star3);
            flowLayoutPanel9.Controls.Add(star4);
            flowLayoutPanel9.Controls.Add(star5);
            flowLayoutPanel9.Location = new Point(3, 23);
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            flowLayoutPanel9.Size = new Size(210, 33);
            flowLayoutPanel9.TabIndex = 5;
            // 
            // star1
            // 
            star1.Image = Properties.Resources.Rating;
            star1.Location = new Point(3, 3);
            star1.Name = "star1";
            star1.Size = new Size(30, 30);
            star1.SizeMode = PictureBoxSizeMode.StretchImage;
            star1.TabIndex = 5;
            star1.TabStop = false;
            star1.Click += star1_Click;
            // 
            // star2
            // 
            star2.Image = Properties.Resources.Rating;
            star2.Location = new Point(39, 3);
            star2.Name = "star2";
            star2.Size = new Size(30, 30);
            star2.SizeMode = PictureBoxSizeMode.StretchImage;
            star2.TabIndex = 6;
            star2.TabStop = false;
            star2.Click += star2_Click;
            // 
            // star3
            // 
            star3.Image = Properties.Resources.Rating;
            star3.Location = new Point(75, 3);
            star3.Name = "star3";
            star3.Size = new Size(30, 30);
            star3.SizeMode = PictureBoxSizeMode.StretchImage;
            star3.TabIndex = 7;
            star3.TabStop = false;
            star3.Click += star3_Click;
            // 
            // star4
            // 
            star4.Image = Properties.Resources.Rating;
            star4.Location = new Point(111, 3);
            star4.Name = "star4";
            star4.Size = new Size(30, 30);
            star4.SizeMode = PictureBoxSizeMode.StretchImage;
            star4.TabIndex = 8;
            star4.TabStop = false;
            star4.Click += star4_Click;
            // 
            // star5
            // 
            star5.Image = Properties.Resources.Rating;
            star5.Location = new Point(147, 3);
            star5.Name = "star5";
            star5.Size = new Size(30, 30);
            star5.SizeMode = PictureBoxSizeMode.StretchImage;
            star5.TabIndex = 9;
            star5.TabStop = false;
            star5.Click += star5_Click;
            // 
            // flowLayoutPanel10
            // 
            flowLayoutPanel10.Controls.Add(button1);
            flowLayoutPanel10.Controls.Add(button2);
            flowLayoutPanel10.Location = new Point(13, 499);
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            flowLayoutPanel10.Size = new Size(270, 68);
            flowLayoutPanel10.TabIndex = 3;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(213, 28);
            button1.TabIndex = 0;
            button1.Text = "Create Task";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            button2.Location = new Point(3, 37);
            button2.Name = "button2";
            button2.Size = new Size(213, 28);
            button2.TabIndex = 1;
            button2.Text = "Reset Planner";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Planner_FX
            // 
            BackColor = Color.Transparent;
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Margin = new Padding(0);
            Name = "Planner_FX";
            Size = new Size(632, 567);
            Resize += Planner_FX_Resize;
            flowLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            taskListPanel.ResumeLayout(false);
            AutoSortBTN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            TimePicker1.ResumeLayout(false);
            TimePicker1.PerformLayout();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            flowLayoutPanel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)star1).EndInit();
            ((System.ComponentModel.ISupportInitialize)star2).EndInit();
            ((System.ComponentModel.ISupportInitialize)star3).EndInit();
            ((System.ComponentModel.ISupportInitialize)star4).EndInit();
            ((System.ComponentModel.ISupportInitialize)star5).EndInit();
            flowLayoutPanel10.ResumeLayout(false);
            ResumeLayout(false);

        }

        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel1;
        private FlowLayoutPanel flowLayoutPanel3;
        private Label label1;
        private RoundedTextBox_CMP usernameTextbox;
        private FlowLayoutPanel flowLayoutPanel4;
        private Label label2;
        private RoundedTextBox_CMP roundedTextBox_cmp1;
        private FlowLayoutPanel flowLayoutPanel5;
        private FlowLayoutPanel flowLayoutPanel6;
        private Label label3;
        private RadioButton AMButton;
        private RadioButton PMButton;
        private FlowLayoutPanel flowLayoutPanel7;
        private Label label4;
        private FlowLayoutPanel TimePicker1;
        public RoundedTextBox_CMP MorningStartHour1;
        public RoundedTextBox_CMP textBox2;
        private Label label5;
        private RoundedTextBox_CMP textBox3;
        private RoundedTextBox_CMP textBox4;
        private FlowLayoutPanel flowLayoutPanel8;
        private Label label6;
        private FlowLayoutPanel flowLayoutPanel9;
        private PictureBox star5;
        private PictureBox star1;
        private PictureBox star2;
        private PictureBox star3;
        private PictureBox star4;
        private FlowLayoutPanel flowLayoutPanel10;
        private Button button1;
        private Button button2;
        private FlowLayoutPanel taskListPanel;
        private FlowLayoutPanel flowLayoutPanel2;
        private Panel AutoSortBTN;
        private PictureBox pictureBox1;
        private int taskCounter = 0;

        // getting duration
        private bool TryParseDuration(out TimeSpan duration, out string error)
        {
            duration = TimeSpan.Zero;
            error = null;

            // Combine hour digits (tens + units)
            string hourText = MorningStartHour1.Text.Trim() + textBox2.Text.Trim();
            if (!int.TryParse(hourText, out int hours) || hours < 0 || hours > 23)
            {
                error = "Hours must be between 0 and 23 (use two digits, e.g., 04).";
                return false;
            }

            // Combine minute digits (tens + units)
            string minuteText = textBox3.Text.Trim() + textBox4.Text.Trim();
            if (!int.TryParse(minuteText, out int minutes) || minutes < 0 || minutes > 59)
            {
                error = "Minutes must be between 0 and 59 (use two digits, e.g., 30).";
                return false;
            }

            if (hours == 0 && minutes == 0)
            {
                error = "Duration cannot be zero.";
                return false;
            }

            duration = new TimeSpan(hours, minutes, 0);
            return true;
        }

        private TaskCard_CMP CreateTaskCardFromTask(Task_BX task)
        {
            string timePeriod = (task.TimePreference == TimePreference_BX.Morning) ? "Morning" : "Afternoon";
            // Format duration as HH:MM
            string duration = $"{task.Duration.Hours:D2}:{task.Duration.Minutes:D2}";
            var card = new TaskCard_CMP(task.Name, task.Description, timePeriod, duration, task.Priority);
            card.Tag = task;  // store reference for deletion
            card.DeleteClicked += (s, e) => DeleteTask((TaskCard_CMP)s);
            return card;
        }

        // task display with sorting
        public void RefreshTaskDisplay()
        {
            if (taskListPanel == null || currentUser == null) return;

            taskListPanel.SuspendLayout();
            taskListPanel.Controls.Clear();

            var morningList = currentUser.MorningTasks.ToList();
            var afternoonList = currentUser.AfternoonTasks.ToList();

            if (morningList.Count > 0)
            {
                Label morningHeader = new Label
                {
                    Text = "☀️ Morning Tasks",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(255, 140, 0),
                    Margin = new Padding(3, 10, 3, 5),
                    AutoSize = true
                };
                taskListPanel.Controls.Add(morningHeader);
                foreach (var task in morningList)
                    taskListPanel.Controls.Add(CreateTaskCardFromTask(task));
            }

            if (afternoonList.Count > 0)
            {
                Label afternoonHeader = new Label
                {
                    Text = "🌙 Afternoon Tasks",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(0, 120, 215),
                    Margin = new Padding(3, 15, 3, 5),
                    AutoSize = true
                };
                taskListPanel.Controls.Add(afternoonHeader);
                foreach (var task in afternoonList)
                    taskListPanel.Controls.Add(CreateTaskCardFromTask(task));
            }

            if (morningList.Count == 0 && afternoonList.Count == 0)
            {
                Label noTasksLabel = new Label
                {
                    Text = "No tasks yet. Create your first task!",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 50,
                    Margin = new Padding(3, 20, 3, 0)
                };
                taskListPanel.Controls.Add(noTasksLabel);
            }

            taskListPanel.ResumeLayout(false);
            taskListPanel.PerformLayout();
        }

        private void DeleteTask(TaskCard_CMP taskCard)
        {
            if (currentUser == null) return;

            Task_BX task = taskCard.Tag as Task_BX;
            if (task == null) return;

            DialogResult result = MessageBox.Show($"Delete task '{task.Name}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            currentUser.RemoveTask(task);
            RefreshTaskDisplay();
        }

        private void ResetAllTasks()
        {
            if (currentUser == null) return;

            DialogResult result = MessageBox.Show("Delete ALL tasks (both morning and afternoon)?", "Confirm Reset",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            // Clear both lists (using ToList() to avoid modification during iteration)
            foreach (var task in currentUser.MorningTasks.ToList())
                currentUser.RemoveTask(task);
            foreach (var task in currentUser.AfternoonTasks.ToList())
                currentUser.RemoveTask(task);

            RefreshTaskDisplay();
            taskCounter = 0;
        }

        // CREATING TASK BACKEND
        private void CreateTask()
        {
            if (currentUser == null)
            {
                MessageBox.Show("User not loaded. Please restart the application.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // get task name to validate
            string taskName = GetTaskName();
            if (string.IsNullOrWhiteSpace(taskName))
            {
                MessageBox.Show("Please enter a task name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string description = GetTaskDescription();

            // Priority (1–5)
            int priority = selectedPriority;
            if (priority < 1)
            {
                MessageBox.Show("Please select a priority (1-5 stars).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // morning or afternoon 
            TimePreference_BX timePref;
            if (AMButton.Checked)
                timePref = TimePreference_BX.Morning;
            else if (PMButton.Checked)
                timePref = TimePreference_BX.Afternoon;
            else
            {
                MessageBox.Show("Please select Morning or Afternoon.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryParseDuration(out TimeSpan duration, out string durationError))
            {
                MessageBox.Show($"Invalid duration: {durationError}", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Task_BX newTask = new Task_BX(taskName, priority, duration, timePref, description);

            // Add to user (automatically sorted and placed in morning/afternoon list)
            currentUser.AddTask(newTask);

            // Refresh the UI from the model
            RefreshTaskDisplay();

            // Clear input form
            ClearTaskForm();

            taskCounter++;
        }

        private void InitializeTaskDisplay()
        {
            panel1.Controls.Clear();

            TableLayoutPanel containerPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                RowStyles = {
            new RowStyle(SizeType.Percent, 100F),
            new RowStyle(SizeType.Absolute, 40F)
        },
                BackColor = Color.White,
                Padding = new Padding(0)
            };

            Label headerLabel = new Label
            {
                Text = "My Tasks",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            // Use the existing taskListPanel from designer, don't create a new one
            if (taskListPanel == null)
            {
                taskListPanel = new FlowLayoutPanel();
            }

            taskListPanel.Dock = DockStyle.Fill;
            taskListPanel.FlowDirection = FlowDirection.TopDown;
            taskListPanel.WrapContents = false;
            taskListPanel.AutoScroll = true;
            taskListPanel.Padding = new Padding(5);
            taskListPanel.BackColor = Color.White;
            taskListPanel.Controls.Clear(); // Clear existing controls (except AutoSortBTN if needed)

            Panel bottomPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Height = 40
            };

            // Use existing AutoSortBTN or create new
            if (AutoSortBTN == null)
            {
                AutoSortBTN = new Panel();
            }

            AutoSortBTN.Size = new Size(35, 35);
            AutoSortBTN.BackColor = Color.FromArgb(240, 240, 240);
            AutoSortBTN.Cursor = Cursors.Hand;
            AutoSortBTN.Visible = true;

            // Remove old paint handlers to avoid duplicates
            AutoSortBTN.Paint -= AutoSortBTN_Paint;
            AutoSortBTN.Paint += AutoSortBTN_Paint;

            if (pictureBox1 == null)
            {
                pictureBox1 = new PictureBox();
            }

            pictureBox1.Image = Properties.Resources.AutoSort;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Padding = new Padding(5);

            AutoSortBTN.Controls.Clear();
            AutoSortBTN.Controls.Add(pictureBox1);

            // Position button
            AutoSortBTN.Location = new Point(bottomPanel.Width - AutoSortBTN.Width - 10,
                                              (bottomPanel.Height - AutoSortBTN.Height) / 2);

            bottomPanel.Resize += (s, e) =>
            {
                AutoSortBTN.Location = new Point(bottomPanel.Width - AutoSortBTN.Width - 10,
                                                  (bottomPanel.Height - AutoSortBTN.Height) / 2);
            };

            // Remove old click handlers to avoid duplicates
            AutoSortBTN.Click -= (s, e) => SortTasksByPriority();
            pictureBox1.Click -= (s, e) => SortTasksByPriority();

            AutoSortBTN.Click += (s, e) => SortTasksByPriority();
            pictureBox1.Click += (s, e) => SortTasksByPriority();

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(AutoSortBTN, "Sort tasks by priority (highest first)");

            bottomPanel.Controls.Clear();
            bottomPanel.Controls.Add(AutoSortBTN);

            containerPanel.Controls.Add(taskListPanel, 0, 0);
            containerPanel.Controls.Add(bottomPanel, 0, 1);

            panel1.Controls.Add(headerLabel);
            panel1.Controls.Add(containerPanel);

            containerPanel.BringToFront();
        }

        private void AutoSortBTN_Paint(object sender, PaintEventArgs e)
        {
            var rect = new Rectangle(0, 0, AutoSortBTN.Width - 1, AutoSortBTN.Height - 1);
            using (var pen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        // USES SortedTaskList_BX to sort tasks by priority and refresh display
        private void SortTasksByPriority()
        {
            if (currentUser == null) return;

            // Convert unsorted lists to SortedTaskList_BX and sort
            currentUser.SortTasks();

            // Refresh the display to show the sorted order
            RefreshTaskDisplay();

            var mainForm = this.FindForm() as MainWindowMother_FX;
            mainForm?.RefreshCurrentContent();
        }

        // Helper methods to get form values
        private string GetSelectedTimePeriod()
        {
            if (AMButton.Checked)
                return "Morning";
            else if (PMButton.Checked)
                return "Afternoon";
            else
                return "Not Set";
        }

        private string GetDuration()
        {
            string hourTens = MorningStartHour1?.Text ?? "";
            string hourUnits = textBox2?.Text ?? "";
            string minuteTens = textBox3?.Text ?? "";
            string minuteUnits = textBox4?.Text ?? "";

            if (!string.IsNullOrEmpty(hourTens) && !string.IsNullOrEmpty(hourUnits) &&
                !string.IsNullOrEmpty(minuteTens) && !string.IsNullOrEmpty(minuteUnits))
            {
                return $"{hourTens}{hourUnits}:{minuteTens}{minuteUnits}";
            }
            return "Not set";
        }

        private int GetSelectedPriority()
        {
            return selectedPriority;
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

        private void Planner_FX_Resize(object sender, EventArgs e)
        {
            ResizeContent();
        }

        public void ResizeContent()
        {
            if (flowLayoutPanel1 == null || this.IsDisposed) return;

            try
            {
                this.SuspendLayout();
                flowLayoutPanel1.SuspendLayout();
                flowLayoutPanel2.SuspendLayout();

                // Calculate split width (50/50)
                int totalWidth = this.ClientSize.Width;
                int halfWidth = totalWidth / 2;

                // Set widths of main panels
                flowLayoutPanel1.Width = halfWidth;
                flowLayoutPanel2.Width = halfWidth;
                flowLayoutPanel2.Location = new Point(halfWidth, 0);
                flowLayoutPanel2.Left = halfWidth;

                // Set heights to fill
                int availableHeight = this.ClientSize.Height;
                flowLayoutPanel1.Height = availableHeight;
                flowLayoutPanel2.Height = availableHeight;

                // Update internal panel sizes in flowLayoutPanel2
                int internalWidth = flowLayoutPanel2.ClientSize.Width - flowLayoutPanel2.Padding.Horizontal - 10;

                if (flowLayoutPanel3 != null)
                    flowLayoutPanel3.Width = internalWidth;
                if (flowLayoutPanel4 != null)
                    flowLayoutPanel4.Width = internalWidth;
                if (flowLayoutPanel5 != null)
                    flowLayoutPanel5.Width = internalWidth;
                if (flowLayoutPanel10 != null)
                    flowLayoutPanel10.Width = internalWidth;

                // Update textbox widths
                if (usernameTextbox != null)
                    usernameTextbox.Width = flowLayoutPanel3.ClientSize.Width - 20;
                if (roundedTextBox_cmp1 != null)
                    roundedTextBox_cmp1.Width = flowLayoutPanel4.ClientSize.Width - 20;

                // Update panel1 (task list container)
                if (panel1 != null)
                {
                    panel1.Width = flowLayoutPanel1.ClientSize.Width - flowLayoutPanel1.Padding.Horizontal;
                    panel1.Height = flowLayoutPanel1.ClientSize.Height - flowLayoutPanel1.Padding.Vertical;
                    panel1.Location = new Point(
                        flowLayoutPanel1.Padding.Left,
                        flowLayoutPanel1.Padding.Top
                    );

                    // Update task list panel if it exists
                    if (taskListPanel != null)
                    {
                        taskListPanel.Width = panel1.ClientSize.Width - 10;
                        taskListPanel.Height = panel1.ClientSize.Height - 40; // Account for header
                    }
                }
            }
            finally
            {
                flowLayoutPanel2.ResumeLayout(false);
                flowLayoutPanel1.ResumeLayout(false);
                this.ResumeLayout(false);
            }
        }

        internal void SetUser()
        {
            currentUser = AppState.CurrentUser;
            if (currentUser != null)
                RefreshTaskDisplay();
        }

        // Public methods to get task data
        public string GetTaskName() => usernameTextbox?.Text ?? string.Empty;
        public string GetTaskDescription() => roundedTextBox_cmp1?.Text ?? string.Empty;

        public void ClearTaskForm()
        {
            if (usernameTextbox != null)
                usernameTextbox.Text = string.Empty;
            if (roundedTextBox_cmp1 != null)
                roundedTextBox_cmp1.Text = string.Empty;

            // Reset time picker fields
            if (MorningStartHour1 != null)
                MorningStartHour1.Text = string.Empty;
            if (textBox2 != null)
                textBox2.Text = string.Empty;
            if (textBox3 != null)
                textBox3.Text = string.Empty;
            if (textBox4 != null)
                textBox4.Text = string.Empty;

            // Reset radio buttons
            AMButton.Checked = false;
            PMButton.Checked = false;

            // Reset star rating to none
            selectedPriority = 0;
            var stars = new PictureBox[] { star1, star2, star3, star4, star5 };
            UpdateStarDisplay(stars, selectedPriority);
        }
        private void SetupButtonEvents()
        {
            // Create Task button
            button1.Click += (s, e) => CreateTask();

            // Reset Planner button
            button2.Click += (s, e) => ResetAllTasks();

            // Optional: Add star rating selection
            SetupStarRatingSelection();
        }

        private void SetupStarRatingSelection()
        {
            var stars = new PictureBox[] { star1, star2, star3, star4, star5 };

            // Initialize all stars
            for (int i = 0; i < stars.Length; i++)
            {
                int rating = i + 1;
                stars[i].Cursor = Cursors.Hand;
                stars[i].Tag = rating;
                stars[i].SizeMode = PictureBoxSizeMode.StretchImage;

                // Remove old event handlers to avoid duplicates
                stars[i].Click -= (s, e) => { };
                stars[i].MouseEnter -= (s, e) => { };

                // Add click event
                stars[i].Click += (s, e) =>
                {
                    selectedPriority = rating;
                    UpdateStarDisplay(stars, selectedPriority);
                };

                // Add hover effect
                stars[i].MouseEnter += (s, e) =>
                {
                    HighlightStars(stars, rating);
                };
            }

            // Reset stars when mouse leaves the star area
            if (flowLayoutPanel9 != null)
            {
                flowLayoutPanel9.MouseLeave -= (s, e) => { };
                flowLayoutPanel9.MouseLeave += (s, e) =>
                {
                    UpdateStarDisplay(stars, selectedPriority);
                };
            }

            // Set default priority 
            selectedPriority = 0;
            UpdateStarDisplay(stars, selectedPriority);
        }


        private void HighlightStars(PictureBox[] stars, int upToRating)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Image = (i < upToRating)
                    ? Properties.Resources.Rating
                    : ImageHelper.CreateDimmedStar(Properties.Resources.Rating);
                stars[i].BackColor = Color.Transparent;
            }
        }

        private void UpdateStarDisplay(PictureBox[] stars, int rating)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Image = (i < rating)
                    ? Properties.Resources.Rating
                    : ImageHelper.CreateDimmedStar(Properties.Resources.Rating);
                stars[i].BackColor = Color.Transparent;
            }
        }

        // Update GetSelectedPriority to use the selected rating
        // Add this field:
        private int selectedPriority = 0;

        private void usernameTextbox_Load(object sender, EventArgs e)
        {

        }

        private void roundedTextBox_cmp1_Load(object sender, EventArgs e)
        {

        }

        private void AMButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PMButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MorningStartHour1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_Load(object sender, EventArgs e)
        {

        }

        private void star1_Click(object sender, EventArgs e)
        {

        }

        private void star2_Click(object sender, EventArgs e)
        {

        }

        private void star3_Click(object sender, EventArgs e)
        {

        }

        private void star4_Click(object sender, EventArgs e)
        {

        }

        private void star5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SortTasksByPriority();
        }

        // Then in SetupStarRatingSelection, update the click event:
        // selectedPriority = rating;


    }
}