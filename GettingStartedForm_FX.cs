using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Inflow
{
    public partial class GettingStartedForm_FX : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Username { get; set; }

        // Textboxes
        private RoundedTextBox_CMP MorningStartHour1, textBox2, textBox3, textBox4;
        private RoundedTextBox_CMP textBox5, textBox6, textBox7, textBox8;
        private RoundedTextBox_CMP textBox9, textBox10, textBox11, textBox12;
        private RoundedTextBox_CMP textBox13, textBox14, textBox15, textBox16;

        public GettingStartedForm_FX(string username)
        {
            InitializeComponent();
            Username = username;
            BuildUI();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximumSize = this.MinimumSize = this.Size;
        }
        private void InitializeComponent()
        {
            // Initalize all child textboxes in each time row to avoid designer issues

        }

        private void BuildUI()
        {
            this.ClientSize = new Size(1025, 738);
            this.BackColor = Color.White;
            this.ControlBox = false;

            // Left gradient panel
            GradientPanel_CMP panel2 = new GradientPanel_CMP
            {
                Dock = DockStyle.Left,
                Size = new Size(344, 738),
                ColorTop = ColorTranslator.FromHtml("#0E24F0"),
                ColorBottom = ColorTranslator.FromHtml("#01FBCE")
            };
            PictureBox pictureBox1 = new PictureBox
            {
                Image = Properties.Resources.LogoMark,
                SizeMode = PictureBoxSizeMode.AutoSize,
                BackColor = Color.Transparent,
                Location = new Point(75, 237)
            };
            panel2.Controls.Add(pictureBox1);

            // Right panel
            Panel panel3 = new Panel
            {
                Dock = DockStyle.Right,
                BackColor = Color.White,
                Size = new Size(683, 738)
            };

            // Title
            Label label1 = new Label
            {
                Text = "Getting Started",
                Font = new Font("Inter", 22.2F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(197, 68)
            };

            // Labels
            Label label2 = new Label
            {
                Text = "What time do you start your morning schedule?",
                Font = new Font("Inter", 9F),
                AutoSize = true,
                Location = new Point(171, 133)
            };
            Label label3 = new Label
            {
                Text = "What time do you end your morning schedule?",
                Font = new Font("Inter", 9F),
                AutoSize = true,
                Location = new Point(174, 244)
            };
            Label label5 = new Label
            {
                Text = "What time do you start your afternoon schedule?",
                Font = new Font("Inter", 9F),
                AutoSize = true,
                Location = new Point(166, 363)
            };
            Label label6 = new Label
            {
                Text = "What time do you end your afternoon schedule?",
                Font = new Font("Inter", 9F),
                AutoSize = true,
                Location = new Point(166, 492)
            };

            // Helper to add a time row with formatting and center alignment
            RoundedTextBox_CMP[] AddTimeRow(int x, int y)
            {
                var hhTens = new RoundedTextBox_CMP { Size = new Size(36, 46), Location = new Point(x, y) };
                var hhUnits = new RoundedTextBox_CMP { Size = new Size(36, 46), Location = new Point(x + 42, y) };
                var colon = new Label { Text = ":", Font = new Font("Inter Light", 18F), AutoSize = false, Size = new Size(20, 36), TextAlign = ContentAlignment.MiddleCenter, Location = new Point(x + 84, y+15) };
                var mmTens = new RoundedTextBox_CMP { Size = new Size(36, 46), Location = new Point(x + 113, y) };
                var mmUnits = new RoundedTextBox_CMP { Size = new Size(36, 46), Location = new Point(x + 155, y) };

                // Apply formatting and center alignment to all textboxes in this row
                var textBoxes = new[] { hhTens, hhUnits, mmTens, mmUnits };
                foreach (var box in textBoxes)
                {
                    box.Anchor = AnchorStyles.None;
                    box.BackColor = SystemColors.ControlLight;
                    box.Font = new Font("Inter Light", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    box.ForeColor = SystemColors.WindowText;
                    box.TextAlign = HorizontalAlignment.Center;  // Center text horizontally
                    box.Margin = new Padding(0, 0, 0, 15);  // Add vertical padding to center text vertically
                    box.Text = "";  // Clear any default text
                }

                panel3.Controls.AddRange(new Control[] { hhTens, hhUnits, colon, mmTens, mmUnits });
                return new[] { hhTens, hhUnits, mmTens, mmUnits };
            }

            // Morning start
            var ms = AddTimeRow(249, 162);
            MorningStartHour1 = ms[0]; textBox2 = ms[1]; textBox3 = ms[2]; textBox4 = ms[3];
            // Morning end
            var me = AddTimeRow(249, 273);
            textBox5 = me[0]; textBox6 = me[1]; textBox7 = me[2]; textBox8 = me[3];
            // Afternoon start
            var ast = AddTimeRow(249, 392);
            textBox9 = ast[0]; textBox10 = ast[1]; textBox11 = ast[2]; textBox12 = ast[3];
            // Afternoon end
            var aet = AddTimeRow(249, 521);
            textBox13 = aet[0]; textBox14 = aet[1]; textBox15 = aet[2]; textBox16 = aet[3];

            // Set numeric restrictions and placeholders
            var allBoxes = new[] { MorningStartHour1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8,
                                   textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16 };
            foreach (var box in allBoxes)
            {
                box.MaxLength = 1;
                box.NumericOnly = true;
                box.PlaceholderText = "x";
            }

            // Next button panel
            Panel panel4 = new Panel
            {
                Size = new Size(101, 53),
                Location = new Point(523, 620),
                BackColor = Color.Transparent
            };
            Label NextLabel = new Label
            {
                Text = "Next",
                Font = new Font("Microsoft Sans Serif", 12F),
                AutoSize = true,
                Location = new Point(10, 14)
            };
            PictureBox nextPic = new PictureBox
            {
                Image = Properties.Resources.NextButton,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(70, 6)
            };
            panel4.Controls.Add(NextLabel);
            panel4.Controls.Add(nextPic);
            nextPic.Click += (s, e) => NextButtonClick();
            NextLabel.Click += (s, e) => NextButtonClick();

            panel3.Controls.Add(label1);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(panel4);

            this.Controls.Add(panel2);
            this.Controls.Add(panel3);
        }

        // Parsing helpers
        private bool TryGetTwoDigitValue(Control tens, Control units, out int value, out string error)
        {
            value = 0; error = null;
            if (!int.TryParse(tens.Text, out int t) || t < 0 || t > 5) { error = $"Invalid tens digit '{tens.Text}' (0-5)"; return false; }
            if (!int.TryParse(units.Text, out int u) || u < 0 || u > 9) { error = $"Invalid units digit '{units.Text}' (0-9)"; return false; }
            value = t * 10 + u;
            return true;
        }

        private bool TryParseTime(Control hourTens, Control hourUnits, Control minTens, Control minUnits, bool isMorning, out TimeSpan time, out string error)
        {
            time = TimeSpan.Zero; error = null;
            if (!TryGetTwoDigitValue(hourTens, hourUnits, out int hour, out error)) return false;
            if (!TryGetTwoDigitValue(minTens, minUnits, out int minute, out error)) return false;
            if (hour < 0 || hour > 12) { error = $"Hour must be 0-12 (got {hour})"; return false; }
            if (minute < 0 || minute > 59) { error = $"Minute must be 0-59 (got {minute})"; return false; }
            if (hour == 0) hour = 12;
            if (!isMorning && hour != 12) hour += 12;
            if (isMorning && hour == 12) hour = 0;
            time = new TimeSpan(hour, minute, 0);
            return true;
        }

        private bool TryGetMorningStart(out TimeSpan t, out string e) => TryParseTime(MorningStartHour1, textBox2, textBox3, textBox4, true, out t, out e);
        private bool TryGetMorningEnd(out TimeSpan t, out string e) => TryParseTime(textBox5, textBox6, textBox7, textBox8, true, out t, out e);
        private bool TryGetAfternoonStart(out TimeSpan t, out string e) => TryParseTime(textBox9, textBox10, textBox11, textBox12, false, out t, out e);
        private bool TryGetAfternoonEnd(out TimeSpan t, out string e) => TryParseTime(textBox13, textBox14, textBox15, textBox16, false, out t, out e);

        private void NextButtonClick()
        {
            string e1 = null, e2 = null, e3 = null, e4 = null;
            if (!TryGetMorningStart(out TimeSpan ms, out e1) ||
                !TryGetMorningEnd(out TimeSpan me, out e2) ||
                !TryGetAfternoonStart(out TimeSpan ast, out e3) ||
                !TryGetAfternoonEnd(out TimeSpan aet, out e4))
            {
                MessageBox.Show($"Invalid input:\n{e1 ?? e2 ?? e3 ?? e4}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ms >= me) { MessageBox.Show("Morning end must be after morning start."); return; }
            if (ast >= aet) { MessageBox.Show("Afternoon end must be after afternoon start."); return; }
            if (me > ast && ast != TimeSpan.Zero) { MessageBox.Show("Morning end should not overlap afternoon start."); return; }

            var schedule = new UserSchedule_BX(Username, ms, me, ast, aet);
            var user = new User_BX(Username, schedule);
            AppState.CurrentUser = user;

            MessageBox.Show($"Schedule saved!\nMorning: {ms:hh\\:mm}–{me:hh\\:mm} AM \nAfternoon: {ast:hh\\:mm}–{aet:hh\\:mm} PM", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var main = new MainWindowMother_FX();
            this.Close();
            main.Show();
            main.TopMost = true;
        }
    }
}