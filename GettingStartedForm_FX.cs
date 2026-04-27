using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Inflow
{
    public partial class GettingStartedForm_FX : Form
    {
        private GradientPanel_CMP panel2;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string Username { get; set; }

        public GettingStartedForm_FX(string username)
        {
            Username = username;
            InitializeComponent();
            panel2.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#01FBCE");
            panel2.ColorTop = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximumSize = this.MinimumSize = this.Size;
            textBox10.PlaceholderText = textBox14.PlaceholderText = textBox6.PlaceholderText = textBox2.PlaceholderText = "x";
            textBox11.PlaceholderText = textBox15.PlaceholderText = textBox7.PlaceholderText = textBox3.PlaceholderText = "x";
            textBox12.PlaceholderText = textBox13.PlaceholderText = textBox5.PlaceholderText = textBox4.PlaceholderText = "x";
            textBox9.PlaceholderText = textBox16.PlaceholderText = textBox8.PlaceholderText = MorningStartHour1.PlaceholderText = "x";

            var timeTextBoxes = new RoundedTextBox_CMP[]
            {
                MorningStartHour1, textBox2, textBox3, textBox4,
                textBox5, textBox6, textBox7, textBox8,
                textBox9, textBox10, textBox11, textBox12,
                textBox13, textBox14, textBox15, textBox16
            };
            foreach (var box in timeTextBoxes)
            {
                box.MaxLength = 1;
                box.NumericOnly = true;
            }
        }

        private Label label1;
        private Panel panel3;
        private PictureBox pictureBox1;
        private Label label2;
        private Panel panel4;
        private Label Next;
        private PictureBox nextButton;
        private Panel panel1;
        private Label label3;

        private bool TryGetTwoDigitValue(Control tensBox, Control unitsBox, out int value, out string error)
        {
            value = 0;
            error = null;

            if (!int.TryParse(tensBox.Text, out int tens) || tens < 0 || tens > 5)
            {
                error = $"Invalid tens digit '{tensBox.Text}' (must be 0-5)";
                return false;
            }
            if (!int.TryParse(unitsBox.Text, out int units) || units < 0 || units > 9)
            {
                error = $"Invalid units digit '{unitsBox.Text}' (must be 0-9)";
                return false;
            }

            value = tens * 10 + units;
            return true;
        }

        private bool TryParseTime(Control hourTens, Control hourUnits, Control minuteTens, Control minuteUnits,
                                  RadioButton amRadio, RadioButton pmRadio,
                                  out TimeSpan time, out string error)
        {
            time = TimeSpan.Zero;
            error = null;

            if (!TryGetTwoDigitValue(hourTens, hourUnits, out int hour, out error))
                return false;
            if (!TryGetTwoDigitValue(minuteTens, minuteUnits, out int minute, out error))
                return false;

            if (hour < 0 || hour > 12)
            {
                error = $"Hour must be between 0 and 12 (got {hour})";
                return false;
            }
            if (minute < 0 || minute > 59)
            {
                error = $"Minute must be between 0 and 59 (got {minute})";
                return false;
            }
            if (!amRadio.Checked && !pmRadio.Checked)
            {
                error = "Please select AM or PM.";
                return false;
            }

            // Convert 0 to 12 (12-hour representation)
            if (hour == 0) hour = 12;

            if (pmRadio.Checked && hour != 12) hour += 12;
            if (amRadio.Checked && hour == 12) hour = 0;

            time = new TimeSpan(hour, minute, 0);
            return true;
        }

        // Morning start
        private bool TryGetMorningStart(out TimeSpan time, out string error)
        {
            return TryParseTime(MorningStartHour1, textBox2, textBox3, textBox4,
                                radioButton1, radioButton2, out time, out error);
        }

        // Morning end (textBox5 & textBox6 = hour; textBox7 & textBox8 = minute)
        private bool TryGetMorningEnd(out TimeSpan time, out string error)
        {
            return TryParseTime(textBox5, textBox6, textBox7, textBox8,
                                radioButton3, radioButton4, out time, out error);
        }

        // Afternoon start (textBox9 & textBox10 = hour; textBox11 & textBox12 = minute)
        private bool TryGetAfternoonStart(out TimeSpan time, out string error)
        {
            return TryParseTime(textBox9, textBox10, textBox11, textBox12,
                                radioButton5, radioButton6, out time, out error);
        }

        // Afternoon end (textBox13 & textBox14 = hour; textBox15 & textBox16 = minute)
        private bool TryGetAfternoonEnd(out TimeSpan time, out string error)
        {
            return TryParseTime(textBox13, textBox14, textBox15, textBox16,
                                radioButton7, radioButton8, out time, out error);
        }

        // ========== NEXT BUTTON LOGIC ==========
        private void nextButton_Click(object sender, EventArgs e)
        {
            string error1 = null, error2 = null, error3 = null, error4 = null;
            if (!TryGetMorningStart(out TimeSpan morningStart, out error1) ||
                !TryGetMorningEnd(out TimeSpan morningEnd, out error2) ||
                !TryGetAfternoonStart(out TimeSpan afternoonStart, out error3) ||
                !TryGetAfternoonEnd(out TimeSpan afternoonEnd, out error4))
            {
                string error = error1 ?? error2 ?? error3 ?? error4;
                MessageBox.Show($"Invalid input:\n{error}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Additional validations
            if (morningStart >= morningEnd)
            {
                MessageBox.Show("Morning start time must be before morning end time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (afternoonStart >= afternoonEnd)
            {
                MessageBox.Show("Afternoon start time must be before afternoon end time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (morningEnd > afternoonStart && afternoonStart != TimeSpan.Zero)
            {
                MessageBox.Show("Morning end time should not overlap afternoon start.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create the schedule object
            UserSchedule_BX schedule = new UserSchedule_BX(Username, morningStart, morningEnd, afternoonStart, afternoonEnd);
            User_BX newUser = new User_BX(Username, schedule);
            AppState.CurrentUser = newUser;// OBJECT CREATION FOR USERRRRRR

            // Debug output, not permanent
            MessageBox.Show($"User: {schedule.Username}\n" +
                            $"Morning: {schedule.MorningStart:hh\\:mm} – {schedule.MorningEnd:hh\\:mm}\n" +
                            $"Afternoon: {schedule.AfternoonStart:hh\\:mm} – {schedule.AfternoonEnd:hh\\:mm}",
                            "Schedule Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MainWindowMother_FX mainWindow = new MainWindowMother_FX();
            this.Close();
            mainWindow.Show();
            mainWindow.TopMost = true;
        }
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(GettingStartedForm_FX));
            panel1 = new Panel();
            panel2 = new GradientPanel_CMP();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panel3 = new Panel();
            RadioPanel4 = new FlowLayoutPanel();
            radioButton7 = new RadioButton();
            radioButton8 = new RadioButton();
            RadioPanel3 = new FlowLayoutPanel();
            radioButton5 = new RadioButton();
            radioButton6 = new RadioButton();
            RadioPanel2 = new FlowLayoutPanel();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            TimePicker4 = new FlowLayoutPanel();
            textBox13 = new RoundedTextBox_CMP();
            textBox14 = new RoundedTextBox_CMP();
            label9 = new Label();
            textBox15 = new RoundedTextBox_CMP();
            textBox16 = new RoundedTextBox_CMP();
            TimePicker3 = new FlowLayoutPanel();
            textBox9 = new RoundedTextBox_CMP();
            textBox10 = new RoundedTextBox_CMP();
            label8 = new Label();
            textBox11 = new RoundedTextBox_CMP();
            textBox12 = new RoundedTextBox_CMP();
            TimePicker2 = new FlowLayoutPanel();
            textBox5 = new RoundedTextBox_CMP();
            textBox6 = new RoundedTextBox_CMP();
            label7 = new Label();
            textBox7 = new RoundedTextBox_CMP();
            textBox8 = new RoundedTextBox_CMP();
            label6 = new Label();
            label5 = new Label();
            label3 = new Label();
            RadioPanel = new FlowLayoutPanel();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            TimePicker1 = new FlowLayoutPanel();
            MorningStartHour1 = new RoundedTextBox_CMP();
            textBox2 = new RoundedTextBox_CMP();
            label4 = new Label();
            textBox3 = new RoundedTextBox_CMP();
            textBox4 = new RoundedTextBox_CMP();
            panel4 = new Panel();
            Next = new Label();
            nextButton = new PictureBox();
            label2 = new Label();
            panel2.SuspendLayout();
            ((ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            RadioPanel4.SuspendLayout();
            RadioPanel3.SuspendLayout();
            RadioPanel2.SuspendLayout();
            TimePicker4.SuspendLayout();
            TimePicker3.SuspendLayout();
            TimePicker2.SuspendLayout();
            RadioPanel.SuspendLayout();
            TimePicker1.SuspendLayout();
            panel4.SuspendLayout();
            ((ISupportInitialize)nextButton).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 125);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlDark;
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(344, 738);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.LogoMark;
            pictureBox1.Location = new Point(75, 237);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(191, 191);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Inter", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(195, 49);
            label1.Name = "label1";
            label1.Size = new Size(297, 45);
            label1.TabIndex = 2;
            label1.Text = "Getting Started";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(RadioPanel4);
            panel3.Controls.Add(RadioPanel3);
            panel3.Controls.Add(RadioPanel2);
            panel3.Controls.Add(TimePicker4);
            panel3.Controls.Add(TimePicker3);
            panel3.Controls.Add(TimePicker2);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(RadioPanel);
            panel3.Controls.Add(TimePicker1);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(342, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(683, 738);
            panel3.TabIndex = 3;
            panel3.Paint += panel3_Paint;
            // 
            // RadioPanel4
            // 
            RadioPanel4.Anchor = AnchorStyles.Top;
            RadioPanel4.AutoSize = true;
            RadioPanel4.BackColor = Color.Transparent;
            RadioPanel4.Controls.Add(radioButton7);
            RadioPanel4.Controls.Add(radioButton8);
            RadioPanel4.Location = new Point(294, 616);
            RadioPanel4.Name = "RadioPanel4";
            RadioPanel4.Size = new Size(116, 30);
            RadioPanel4.TabIndex = 17;
            // 
            // radioButton7
            // 
            radioButton7.Anchor = AnchorStyles.None;
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(3, 3);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(53, 24);
            radioButton7.TabIndex = 7;
            radioButton7.TabStop = true;
            radioButton7.Text = "AM";
            radioButton7.TextAlign = ContentAlignment.MiddleCenter;
            radioButton7.UseVisualStyleBackColor = true;
            radioButton7.CheckedChanged += radioButton7_CheckedChanged;
            // 
            // radioButton8
            // 
            radioButton8.Anchor = AnchorStyles.None;
            radioButton8.AutoSize = true;
            radioButton8.Location = new Point(62, 3);
            radioButton8.Name = "radioButton8";
            radioButton8.Size = new Size(51, 24);
            radioButton8.TabIndex = 8;
            radioButton8.TabStop = true;
            radioButton8.Text = "PM";
            radioButton8.TextAlign = ContentAlignment.MiddleCenter;
            radioButton8.UseVisualStyleBackColor = true;
            radioButton8.CheckedChanged += radioButton8_CheckedChanged;
            // 
            // RadioPanel3
            // 
            RadioPanel3.Anchor = AnchorStyles.Top;
            RadioPanel3.AutoSize = true;
            RadioPanel3.BackColor = Color.Transparent;
            RadioPanel3.Controls.Add(radioButton5);
            RadioPanel3.Controls.Add(radioButton6);
            RadioPanel3.Location = new Point(291, 476);
            RadioPanel3.Name = "RadioPanel3";
            RadioPanel3.Size = new Size(116, 30);
            RadioPanel3.TabIndex = 16;
            // 
            // radioButton5
            // 
            radioButton5.Anchor = AnchorStyles.None;
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(3, 3);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(53, 24);
            radioButton5.TabIndex = 7;
            radioButton5.TabStop = true;
            radioButton5.Text = "AM";
            radioButton5.TextAlign = ContentAlignment.MiddleCenter;
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            // 
            // radioButton6
            // 
            radioButton6.Anchor = AnchorStyles.None;
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(62, 3);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(51, 24);
            radioButton6.TabIndex = 8;
            radioButton6.TabStop = true;
            radioButton6.Text = "PM";
            radioButton6.TextAlign = ContentAlignment.MiddleCenter;
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += radioButton6_CheckedChanged;
            // 
            // RadioPanel2
            // 
            RadioPanel2.Anchor = AnchorStyles.Top;
            RadioPanel2.AutoSize = true;
            RadioPanel2.BackColor = Color.Transparent;
            RadioPanel2.Controls.Add(radioButton3);
            RadioPanel2.Controls.Add(radioButton4);
            RadioPanel2.Location = new Point(291, 336);
            RadioPanel2.Name = "RadioPanel2";
            RadioPanel2.Size = new Size(116, 30);
            RadioPanel2.TabIndex = 15;
            // 
            // radioButton3
            // 
            radioButton3.Anchor = AnchorStyles.None;
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(3, 3);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(53, 24);
            radioButton3.TabIndex = 7;
            radioButton3.TabStop = true;
            radioButton3.Text = "AM";
            radioButton3.TextAlign = ContentAlignment.MiddleCenter;
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.Anchor = AnchorStyles.None;
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(62, 3);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(51, 24);
            radioButton4.TabIndex = 8;
            radioButton4.TabStop = true;
            radioButton4.Text = "PM";
            radioButton4.TextAlign = ContentAlignment.MiddleCenter;
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // TimePicker4
            // 
            TimePicker4.BackColor = Color.Transparent;
            TimePicker4.Controls.Add(textBox13);
            TimePicker4.Controls.Add(textBox14);
            TimePicker4.Controls.Add(label9);
            TimePicker4.Controls.Add(textBox15);
            TimePicker4.Controls.Add(textBox16);
            TimePicker4.Location = new Point(249, 553);
            TimePicker4.Name = "TimePicker4";
            TimePicker4.Size = new Size(201, 49);
            TimePicker4.TabIndex = 14;
            // 
            // textBox13
            // 
            textBox13.Anchor = AnchorStyles.None;
            textBox13.BackColor = SystemColors.ControlLight;
            textBox13.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox13.Location = new Point(3, 3);
            textBox13.MaxLength = 32767;
            textBox13.Name = "textBox13";
            textBox13.NumericOnly = false;
            textBox13.Padding = new Padding(10, 8, 10, 8);
            textBox13.Size = new Size(36, 46);
            textBox13.TabIndex = 0;
            textBox13.Load += textBox13_Load;
            // 
            // textBox14
            // 
            textBox14.Anchor = AnchorStyles.None;
            textBox14.BackColor = SystemColors.ControlLight;
            textBox14.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox14.Location = new Point(45, 3);
            textBox14.MaxLength = 32767;
            textBox14.Name = "textBox14";
            textBox14.NumericOnly = false;
            textBox14.Padding = new Padding(10, 8, 10, 8);
            textBox14.Size = new Size(36, 46);
            textBox14.TabIndex = 1;
            textBox14.Load += textBox14_Load;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.None;
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(87, 8);
            label9.Name = "label9";
            label9.Size = new Size(23, 36);
            label9.TabIndex = 2;
            label9.Text = ":";
            // 
            // textBox15
            // 
            textBox15.Anchor = AnchorStyles.None;
            textBox15.BackColor = SystemColors.ControlLight;
            textBox15.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox15.Location = new Point(116, 3);
            textBox15.MaxLength = 32767;
            textBox15.Name = "textBox15";
            textBox15.NumericOnly = false;
            textBox15.Padding = new Padding(10, 8, 10, 8);
            textBox15.Size = new Size(36, 46);
            textBox15.TabIndex = 3;
            textBox15.Load += textBox15_Load;
            // 
            // textBox16
            // 
            textBox16.Anchor = AnchorStyles.None;
            textBox16.BackColor = SystemColors.ControlLight;
            textBox16.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox16.Location = new Point(158, 3);
            textBox16.MaxLength = 32767;
            textBox16.Name = "textBox16";
            textBox16.NumericOnly = false;
            textBox16.Padding = new Padding(10, 8, 10, 8);
            textBox16.Size = new Size(36, 46);
            textBox16.TabIndex = 4;
            textBox16.Load += textBox16_Load;
            // 
            // TimePicker3
            // 
            TimePicker3.BackColor = Color.Transparent;
            TimePicker3.Controls.Add(textBox9);
            TimePicker3.Controls.Add(textBox10);
            TimePicker3.Controls.Add(label8);
            TimePicker3.Controls.Add(textBox11);
            TimePicker3.Controls.Add(textBox12);
            TimePicker3.Location = new Point(249, 413);
            TimePicker3.Name = "TimePicker3";
            TimePicker3.Size = new Size(201, 49);
            TimePicker3.TabIndex = 13;
            // 
            // textBox9
            // 
            textBox9.Anchor = AnchorStyles.None;
            textBox9.BackColor = SystemColors.ControlLight;
            textBox9.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox9.Location = new Point(3, 3);
            textBox9.MaxLength = 32767;
            textBox9.Name = "textBox9";
            textBox9.NumericOnly = false;
            textBox9.Padding = new Padding(10, 8, 10, 8);
            textBox9.Size = new Size(36, 46);
            textBox9.TabIndex = 0;
            textBox9.Load += textBox9_Load_1;
            // 
            // textBox10
            // 
            textBox10.Anchor = AnchorStyles.None;
            textBox10.BackColor = SystemColors.ControlLight;
            textBox10.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox10.Location = new Point(45, 3);
            textBox10.MaxLength = 32767;
            textBox10.Name = "textBox10";
            textBox10.NumericOnly = false;
            textBox10.Padding = new Padding(10, 8, 10, 8);
            textBox10.Size = new Size(36, 46);
            textBox10.TabIndex = 1;
            textBox10.Load += textBox10_Load;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(87, 8);
            label8.Name = "label8";
            label8.Size = new Size(23, 36);
            label8.TabIndex = 2;
            label8.Text = ":";
            // 
            // textBox11
            // 
            textBox11.Anchor = AnchorStyles.None;
            textBox11.BackColor = SystemColors.ControlLight;
            textBox11.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox11.Location = new Point(116, 3);
            textBox11.MaxLength = 32767;
            textBox11.Name = "textBox11";
            textBox11.NumericOnly = false;
            textBox11.Padding = new Padding(10, 8, 10, 8);
            textBox11.Size = new Size(36, 46);
            textBox11.TabIndex = 3;
            textBox11.Load += textBox11_Load;
            // 
            // textBox12
            // 
            textBox12.Anchor = AnchorStyles.None;
            textBox12.BackColor = SystemColors.ControlLight;
            textBox12.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox12.Location = new Point(158, 3);
            textBox12.MaxLength = 32767;
            textBox12.Name = "textBox12";
            textBox12.NumericOnly = false;
            textBox12.Padding = new Padding(10, 8, 10, 8);
            textBox12.Size = new Size(36, 46);
            textBox12.TabIndex = 4;
            textBox12.Load += textBox12_Load;
            // 
            // TimePicker2
            // 
            TimePicker2.BackColor = Color.Transparent;
            TimePicker2.Controls.Add(textBox5);
            TimePicker2.Controls.Add(textBox6);
            TimePicker2.Controls.Add(label7);
            TimePicker2.Controls.Add(textBox7);
            TimePicker2.Controls.Add(textBox8);
            TimePicker2.Location = new Point(249, 273);
            TimePicker2.Name = "TimePicker2";
            TimePicker2.Size = new Size(201, 49);
            TimePicker2.TabIndex = 12;
            // 
            // textBox5
            // 
            textBox5.Anchor = AnchorStyles.None;
            textBox5.BackColor = SystemColors.ControlLight;
            textBox5.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox5.Location = new Point(3, 3);
            textBox5.MaxLength = 32767;
            textBox5.Name = "textBox5";
            textBox5.NumericOnly = false;
            textBox5.Padding = new Padding(10, 8, 10, 8);
            textBox5.Size = new Size(36, 46);
            textBox5.TabIndex = 0;
            textBox5.Load += textBox5_Load;
            // 
            // textBox6
            // 
            textBox6.Anchor = AnchorStyles.None;
            textBox6.BackColor = SystemColors.ControlLight;
            textBox6.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox6.Location = new Point(45, 3);
            textBox6.MaxLength = 32767;
            textBox6.Name = "textBox6";
            textBox6.NumericOnly = false;
            textBox6.Padding = new Padding(10, 8, 10, 8);
            textBox6.Size = new Size(36, 46);
            textBox6.TabIndex = 1;
            textBox6.Load += textBox6_Load;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(87, 8);
            label7.Name = "label7";
            label7.Size = new Size(23, 36);
            label7.TabIndex = 2;
            label7.Text = ":";
            // 
            // textBox7
            // 
            textBox7.Anchor = AnchorStyles.None;
            textBox7.BackColor = SystemColors.ControlLight;
            textBox7.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox7.Location = new Point(116, 3);
            textBox7.MaxLength = 32767;
            textBox7.Name = "textBox7";
            textBox7.NumericOnly = false;
            textBox7.Padding = new Padding(10, 8, 10, 8);
            textBox7.Size = new Size(36, 46);
            textBox7.TabIndex = 3;
            textBox7.Load += textBox7_Load;
            // 
            // textBox8
            // 
            textBox8.Anchor = AnchorStyles.None;
            textBox8.BackColor = SystemColors.ControlLight;
            textBox8.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox8.Location = new Point(158, 3);
            textBox8.MaxLength = 32767;
            textBox8.Name = "textBox8";
            textBox8.NumericOnly = false;
            textBox8.Padding = new Padding(10, 8, 10, 8);
            textBox8.Size = new Size(36, 46);
            textBox8.TabIndex = 4;
            textBox8.Load += textBox8_Load;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Inter", 9F);
            label6.Location = new Point(166, 524);
            label6.Margin = new Padding(3, 10, 3, 10);
            label6.Name = "label6";
            label6.Size = new Size(351, 19);
            label6.TabIndex = 11;
            label6.Text = "What time do you end your afternoon schedule?";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Inter", 9F);
            label5.Location = new Point(166, 384);
            label5.Margin = new Padding(3, 10, 3, 10);
            label5.Name = "label5";
            label5.Size = new Size(356, 19);
            label5.TabIndex = 10;
            label5.Text = "What time do you start your afternoon schedule?";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Inter", 9F);
            label3.Location = new Point(174, 244);
            label3.Margin = new Padding(3, 10, 3, 10);
            label3.Name = "label3";
            label3.Size = new Size(341, 19);
            label3.TabIndex = 9;
            label3.Text = "What time do you end your morning schedule?";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RadioPanel
            // 
            RadioPanel.Anchor = AnchorStyles.Top;
            RadioPanel.AutoSize = true;
            RadioPanel.BackColor = Color.Transparent;
            RadioPanel.Controls.Add(radioButton1);
            RadioPanel.Controls.Add(radioButton2);
            RadioPanel.Location = new Point(291, 196);
            RadioPanel.Name = "RadioPanel";
            RadioPanel.Size = new Size(116, 30);
            RadioPanel.TabIndex = 5;
            // 
            // radioButton1
            // 
            radioButton1.Anchor = AnchorStyles.None;
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(3, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(53, 24);
            radioButton1.TabIndex = 7;
            radioButton1.TabStop = true;
            radioButton1.Text = "AM";
            radioButton1.TextAlign = ContentAlignment.MiddleCenter;
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.Anchor = AnchorStyles.None;
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(62, 3);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(51, 24);
            radioButton2.TabIndex = 8;
            radioButton2.TabStop = true;
            radioButton2.Text = "PM";
            radioButton2.TextAlign = ContentAlignment.MiddleCenter;
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // TimePicker1
            // 
            TimePicker1.BackColor = Color.Transparent;
            TimePicker1.Controls.Add(MorningStartHour1);
            TimePicker1.Controls.Add(textBox2);
            TimePicker1.Controls.Add(label4);
            TimePicker1.Controls.Add(textBox3);
            TimePicker1.Controls.Add(textBox4);
            TimePicker1.Location = new Point(249, 133);
            TimePicker1.Name = "TimePicker1";
            TimePicker1.Size = new Size(201, 49);
            TimePicker1.TabIndex = 8;
            // 
            // MorningStartHour1
            // 
            MorningStartHour1.Anchor = AnchorStyles.None;
            MorningStartHour1.BackColor = SystemColors.ControlLight;
            MorningStartHour1.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MorningStartHour1.Location = new Point(3, 3);
            MorningStartHour1.MaxLength = 32767;
            MorningStartHour1.Name = "MorningStartHour1";
            MorningStartHour1.NumericOnly = false;
            MorningStartHour1.Padding = new Padding(10, 8, 10, 8);
            MorningStartHour1.Size = new Size(36, 46);
            MorningStartHour1.TabIndex = 0;
            MorningStartHour1.Load += textBox1_Load;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.BackColor = SystemColors.ControlLight;
            textBox2.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(45, 3);
            textBox2.MaxLength = 32767;
            textBox2.Name = "textBox2";
            textBox2.NumericOnly = false;
            textBox2.Padding = new Padding(10, 8, 10, 8);
            textBox2.Size = new Size(36, 46);
            textBox2.TabIndex = 1;
            textBox2.Load += textBox2_Load;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(87, 8);
            label4.Name = "label4";
            label4.Size = new Size(23, 36);
            label4.TabIndex = 2;
            label4.Text = ":";
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.None;
            textBox3.BackColor = SystemColors.ControlLight;
            textBox3.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(116, 3);
            textBox3.MaxLength = 32767;
            textBox3.Name = "textBox3";
            textBox3.NumericOnly = false;
            textBox3.Padding = new Padding(10, 8, 10, 8);
            textBox3.Size = new Size(36, 46);
            textBox3.TabIndex = 3;
            textBox3.Load += textBox3_Load;
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.None;
            textBox4.BackColor = SystemColors.ControlLight;
            textBox4.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox4.Location = new Point(158, 3);
            textBox4.MaxLength = 32767;
            textBox4.Name = "textBox4";
            textBox4.NumericOnly = false;
            textBox4.Padding = new Padding(10, 8, 10, 8);
            textBox4.Size = new Size(36, 46);
            textBox4.TabIndex = 4;
            textBox4.Load += textBox4_Load;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel4.BackColor = Color.Transparent;
            panel4.Controls.Add(Next);
            panel4.Controls.Add(nextButton);
            panel4.Location = new Point(523, 620);
            panel4.Name = "panel4";
            panel4.Size = new Size(101, 53);
            panel4.TabIndex = 7;
            panel4.Click += panel4_Click;
            panel4.Paint += panel4_Paint;
            // 
            // Next
            // 
            Next.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Next.AutoSize = true;
            Next.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Next.Location = new Point(10, 14);
            Next.Name = "Next";
            Next.Size = new Size(52, 25);
            Next.TabIndex = 1;
            Next.Text = "Next";
            Next.TextAlign = ContentAlignment.MiddleCenter;
            Next.Click += Next_Click;
            // 
            // nextButton
            // 
            nextButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nextButton.Image = Properties.Resources.NextButton;
            nextButton.Location = new Point(70, 6);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(22, 41);
            nextButton.SizeMode = PictureBoxSizeMode.AutoSize;
            nextButton.TabIndex = 0;
            nextButton.TabStop = false;
            nextButton.Click += nextButton_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Inter", 9F);
            label2.Location = new Point(171, 104);
            label2.Margin = new Padding(3, 10, 3, 10);
            label2.Name = "label2";
            label2.Size = new Size(346, 19);
            label2.TabIndex = 3;
            label2.Text = "What time do you start your morning schedule?";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GettingStartedForm_FX
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(1025, 738);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GettingStartedForm_FX";
            SizeGripStyle = SizeGripStyle.Hide;
            Load += GettingStartedForm_FX_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            RadioPanel4.ResumeLayout(false);
            RadioPanel4.PerformLayout();
            RadioPanel3.ResumeLayout(false);
            RadioPanel3.PerformLayout();
            RadioPanel2.ResumeLayout(false);
            RadioPanel2.PerformLayout();
            TimePicker4.ResumeLayout(false);
            TimePicker4.PerformLayout();
            TimePicker3.ResumeLayout(false);
            TimePicker3.PerformLayout();
            TimePicker2.ResumeLayout(false);
            TimePicker2.PerformLayout();
            RadioPanel.ResumeLayout(false);
            RadioPanel.PerformLayout();
            TimePicker1.ResumeLayout(false);
            TimePicker1.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((ISupportInitialize)nextButton).EndInit();
            ResumeLayout(false);
        }

        private void GettingStartedForm_FX_Load(object sender, EventArgs e)
        {

        }
        private void passwordText_TextChanged(object sender, EventArgs e)
        {

        }
        private void usernameTextbox_TextChanged(object sender, EventArgs e)
        {
        }
        private void confirmPassText_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel4_Click(object sender, EventArgs e)
        {
        }
        private void Next_Click(object sender, EventArgs e)
        {
        }
        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
        }

        // Morning Start Buttons

        private void textBox1_Load(object sender, EventArgs e)
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
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Morning End Buttons

        private void textBox5_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_Load(object sender, EventArgs e)
        {

        }

        private void textBox8_Load(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Afternoon Start Buttons

        private void textBox9_Load_1(object? sender, EventArgs e)
        {

        }

        private void textBox10_Load(object sender, EventArgs e)
        {

        }

        private void textBox11_Load(object sender, EventArgs e)
        {

        }

        private void textBox12_Load(object sender, EventArgs e)
        {

        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Afternoon End Buttons

        private void textBox13_Load(object sender, EventArgs e)
        {

        }

        private void textBox14_Load(object sender, EventArgs e)
        {

        }

        private void textBox15_Load(object sender, EventArgs e)
        {

        }

        private void textBox16_Load(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}
