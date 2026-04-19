using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Inflow
{
    public partial class SplashScreen : Form
    {
        private GradientPanel panel2;


        public SplashScreen()
        {


            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            panel2.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#01FBCE");
            panel2.ColorTop = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            this.MaximumSize = this.MinimumSize = this.Size;

            // Ensure the label uses GDI+ (compatible) text rendering so PrivateFontCollection fonts are used
            label1.UseCompatibleTextRendering = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            panel1 = new Panel();
            panel2 = new GradientPanel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            Next = new Label();
            nextButton = new PictureBox();
            flowLayoutPanel3 = new FlowLayoutPanel();
            usernameTextbox = new RoundedTextBox();
            passwordText = new RoundedTextBox();
            confirmPassText = new RoundedTextBox();
            label2 = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nextButton).BeginInit();
            flowLayoutPanel3.SuspendLayout();
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
            label1.Font = new Font("Microsoft Sans Serif", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(123, 149);
            label1.Name = "label1";
            label1.Size = new Size(401, 91);
            label1.TabIndex = 2;
            label1.Text = "Welcome!";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(flowLayoutPanel3);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(342, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(683, 738);
            panel3.TabIndex = 3;
            panel3.Paint += panel3_Paint;
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
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(usernameTextbox);
            flowLayoutPanel3.Controls.Add(passwordText);
            flowLayoutPanel3.Controls.Add(confirmPassText);
            flowLayoutPanel3.ForeColor = SystemColors.ActiveCaptionText;
            flowLayoutPanel3.Location = new Point(65, 314);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(559, 172);
            flowLayoutPanel3.TabIndex = 6;
            flowLayoutPanel3.Paint += flowLayoutPanel3_Paint;
            // 
            // usernameTextbox
            // 
            usernameTextbox.Anchor = AnchorStyles.None;
            usernameTextbox.BackColor = SystemColors.ControlLight;
            usernameTextbox.BorderStyle = BorderStyle.None;
            usernameTextbox.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            usernameTextbox.ForeColor = SystemColors.WindowText;
            usernameTextbox.InnerPadding = new Padding(12, 20, 12, 20);
            usernameTextbox.Location = new Point(10, 10);
            usernameTextbox.Margin = new Padding(10);
            usernameTextbox.Name = "usernameTextbox";
            usernameTextbox.PlaceholderText = "Username";
            usernameTextbox.Size = new Size(536, 31);
            usernameTextbox.TabIndex = 1;
            usernameTextbox.TextAlign = HorizontalAlignment.Center;
            usernameTextbox.TextChanged += usernameTextbox_TextChanged;
            // 
            // passwordText
            // 
            passwordText.Anchor = AnchorStyles.None;
            passwordText.BackColor = SystemColors.ControlLight;
            passwordText.BorderStyle = BorderStyle.None;
            passwordText.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordText.ForeColor = SystemColors.WindowText;
            passwordText.InnerPadding = new Padding(12, 20, 12, 20);
            passwordText.Location = new Point(10, 61);
            passwordText.Margin = new Padding(10);
            passwordText.Name = "passwordText";
            passwordText.PasswordChar = '*';
            passwordText.PlaceholderText = "Password";
            passwordText.Size = new Size(536, 31);
            passwordText.TabIndex = 1;
            passwordText.TextAlign = HorizontalAlignment.Center;
            passwordText.TextChanged += passwordText_TextChanged;
            // 
            // confirmPassText
            // 
            confirmPassText.Anchor = AnchorStyles.None;
            confirmPassText.BackColor = SystemColors.ControlLight;
            confirmPassText.BorderStyle = BorderStyle.None;
            confirmPassText.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            confirmPassText.ForeColor = SystemColors.WindowText;
            confirmPassText.InnerPadding = new Padding(12, 20, 12, 20);
            confirmPassText.Location = new Point(10, 112);
            confirmPassText.Margin = new Padding(10);
            confirmPassText.Name = "confirmPassText";
            confirmPassText.PasswordChar = '*';
            confirmPassText.PlaceholderText = "Confirm Password";
            confirmPassText.Size = new Size(536, 31);
            confirmPassText.TabIndex = 1;
            confirmPassText.TextAlign = HorizontalAlignment.Center;
            confirmPassText.TextChanged += confirmPassText_TextChanged;
            //
            // username error back-end
            //
            usernameErrorLabel = new Label();
            usernameErrorLabel.AutoSize = true;
            usernameErrorLabel.ForeColor = Color.Red;
            usernameErrorLabel.Font = new Font("Microsoft Sans Serif", 9F);
            usernameErrorLabel.Text = "";
            usernameErrorLabel.Visible = false;
            usernameErrorLabel.Margin = new Padding(10, -10, 10, 10); // negative top pulls it closer to the textbox
            flowLayoutPanel3.Controls.Add(usernameErrorLabel);
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(147, 255);
            label2.Name = "label2";
            label2.Size = new Size(351, 25);
            label2.TabIndex = 3;
            label2.Text = "Please sign in with the following details:";
            // 
            // SplashScreen
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
            Name = "SplashScreen";
            SizeGripStyle = SizeGripStyle.Hide;
            Load += SplashScreen_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nextButton).EndInit();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            ResumeLayout(false);

        }


        private void SplashScreen_Load(object sender, EventArgs e)
        {
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private System.ComponentModel.IContainer components;
        private Label label1;
        private Panel panel3;
        private PictureBox pictureBox1;
        private Label label2;
        private RoundedTextBox passwordText;
        private RoundedTextBox usernameTextbox;
        private FlowLayoutPanel flowLayoutPanel3;
        private RoundedTextBox confirmPassText;
        private Panel panel4;
        private Label Next;
        private PictureBox nextButton;
        private Panel panel1;
        private Label usernameErrorLabel;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void usernameTextbox_TextChanged(object sender, EventArgs e)
        {
            usernameTextbox.BackColor = string.IsNullOrEmpty(usernameTextbox.Text) ? SystemColors.ControlLight : Color.LightBlue;
            string error = ValidateUsername(usernameTextbox.Text);
            usernameErrorLabel.Text = error ?? "";
            usernameErrorLabel.Visible = (error != null);
            UpdateNextButtonState();
        }

        private void panel4_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (!IsFormValid())
            {
                MessageBox.Show("Please enter a valid username or password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show($"Welcome, {usernameTextbox.Text}!", "Successfully logged in", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void passwordText_TextChanged(object sender, EventArgs e)
        {
            UpdateNextButtonState();
        }

        private void confirmPassText_TextChanged(object sender, EventArgs e)
        {
            UpdateNextButtonState();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private string ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return "Username is required.";
            if (username.Length < 3)
                return "Username must be at least 3 characters.";
            if (username.Length > 20)
                return "Username cannot exceed 20 characters.";
            if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
                return "Username can only contain letters, digits, and underscores.";
            string[] existingUsers = { "admin", "user1", "test" };
            if (existingUsers.Contains(username, StringComparer.OrdinalIgnoreCase))
                return "Username already taken.";
            return null;
        }

        private bool IsFormValid()
        {
            if (!string.IsNullOrEmpty(ValidateUsername(usernameTextbox.Text)))
                return false;
            if (string.IsNullOrWhiteSpace(passwordText.Text))
                return false;
            if (passwordText.Text != confirmPassText.Text)
                return false;
            return true;
        }

        private void UpdateNextButtonState()
        {
            bool valid = IsFormValid();
            nextButton.Enabled = valid;
            Next.Enabled = valid;
        }
    }
}
