using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Inflow
{
    public partial class SignUpForm_FX : Form
    {
        private GradientPanel_CMP panel2;


        public SignUpForm_FX()
        {


            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            panel2.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#01FBCE");
            panel2.ColorTop = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            this.MaximumSize = this.MinimumSize = this.Size;

            // Ensure the label uses GDI+ (compatible) text rendering so PrivateFontCollection fonts are used
            label1.UseCompatibleTextRendering = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            usernameTextbox.PlaceholderText = "Username";
            passwordText.PlaceholderText = "Password";
            confirmPassText.PlaceholderText = "Confirm Password";
            passwordText.PasswordChar = '*';
            confirmPassText.PasswordChar = '*';
            usernameTextbox.TextAlign = HorizontalAlignment.Center;
            passwordText.TextAlign = HorizontalAlignment.Center;
            confirmPassText.TextAlign = HorizontalAlignment.Center;

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignUpForm_FX));
            panel1 = new Panel();
            panel2 = new GradientPanel_CMP();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            Next = new Label();
            nextButton = new PictureBox();
            flowLayoutPanel3 = new FlowLayoutPanel();
            usernameTextbox = new RoundedTextBox_CMP();
            passwordText = new RoundedTextBox_CMP();
            confirmPassText = new RoundedTextBox_CMP();
            usernameErrorLabel = new Label();
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
            label1.Font = new Font("Inter", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(135, 157);
            label1.Name = "label1";
            label1.Size = new Size(435, 97);
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
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.BackColor = Color.Transparent;
            flowLayoutPanel3.Controls.Add(usernameTextbox);
            flowLayoutPanel3.Controls.Add(passwordText);
            flowLayoutPanel3.Controls.Add(confirmPassText);
            flowLayoutPanel3.Controls.Add(usernameErrorLabel);
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
            usernameTextbox.Font = new Font("Inter Light", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            usernameTextbox.ForeColor = SystemColors.WindowText;
            usernameTextbox.Location = new Point(10, 10);
            usernameTextbox.Margin = new Padding(10);
            usernameTextbox.Name = "usernameTextbox";
            usernameTextbox.Padding = new Padding(10, 8, 10, 8);
            usernameTextbox.Size = new Size(536, 40);
            usernameTextbox.TabIndex = 1;
            usernameTextbox.TextChanged += usernameTextbox_TextChanged;
            // 
            // passwordText
            // 
            passwordText.Anchor = AnchorStyles.None;
            passwordText.BackColor = SystemColors.ControlLight;
            passwordText.Font = new Font("Inter Light", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordText.ForeColor = SystemColors.WindowText;
            passwordText.Location = new Point(10, 70);
            passwordText.Margin = new Padding(10);
            passwordText.Name = "passwordText";
            passwordText.Padding = new Padding(12, 8, 12, 8);
            passwordText.Size = new Size(536, 40);
            passwordText.TabIndex = 1;
            passwordText.TextChanged += passwordText_TextChanged;
            // 
            // confirmPassText
            // 
            confirmPassText.Anchor = AnchorStyles.None;
            confirmPassText.BackColor = SystemColors.ControlLight;
            confirmPassText.Font = new Font("Inter Light", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            confirmPassText.ForeColor = SystemColors.WindowText;
            confirmPassText.Location = new Point(10, 130);
            confirmPassText.Margin = new Padding(10);
            confirmPassText.Name = "confirmPassText";
            confirmPassText.Padding = new Padding(10, 8, 10, 8);
            confirmPassText.Size = new Size(536, 40);
            confirmPassText.TabIndex = 1;
            confirmPassText.TextChanged += confirmPassText_TextChanged;
            // 
            // usernameErrorLabel
            // 
            usernameErrorLabel.AutoSize = true;
            usernameErrorLabel.Font = new Font("Inter Light", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            usernameErrorLabel.ForeColor = Color.Red;
            usernameErrorLabel.Location = new Point(10, 180);
            usernameErrorLabel.Margin = new Padding(10, 0, 10, 10);
            usernameErrorLabel.Name = "usernameErrorLabel";
            usernameErrorLabel.Size = new Size(0, 20);
            usernameErrorLabel.TabIndex = 2;
            usernameErrorLabel.Visible = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Inter", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(159, 254);
            label2.Name = "label2";
            label2.Size = new Size(392, 24);
            label2.TabIndex = 3;
            label2.Text = "Please sign in with the following details:";
            // 
            // SignUpForm_FX
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
            Name = "SignUpForm_FX";
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
            nextButton.Enabled = false;
            Next.Enabled = false;
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private System.ComponentModel.IContainer components;
        private Label label1;
        private Panel panel3;
        private PictureBox pictureBox1;
        private Label label2;
        private RoundedTextBox_CMP passwordText;
        private RoundedTextBox_CMP usernameTextbox;
        private FlowLayoutPanel flowLayoutPanel3;
        private RoundedTextBox_CMP confirmPassText;
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

        private void Next_Click(object sender, EventArgs e)
        {
            if (!IsFormValid())
            {
                MessageBox.Show("Please enter a valid username or password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GettingStartedForm_FX newForm = new GettingStartedForm_FX();
            newForm.Show();
            newForm.TopMost = true;
            this.Close();
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
