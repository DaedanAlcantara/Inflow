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
            flowLayoutPanel3 = new FlowLayoutPanel();
            textBox1 = new RoundedTextBox();
            textBox2 = new RoundedTextBox();
            textBox3 = new RoundedTextBox();
            label2 = new Label();
            pictureBox2 = new PictureBox();
            Next = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
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
            label1.Location = new Point(123, 149);
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
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel4.BackColor = Color.Transparent;
            panel4.Controls.Add(Next);
            panel4.Controls.Add(pictureBox2);
            panel4.Location = new Point(523, 620);
            panel4.Name = "panel4";
            panel4.Size = new Size(101, 53);
            panel4.TabIndex = 7;
            panel4.Click += panel4_Click;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(textBox1);
            flowLayoutPanel3.Controls.Add(textBox2);
            flowLayoutPanel3.Controls.Add(textBox3);
            flowLayoutPanel3.ForeColor = SystemColors.ActiveCaptionText;
            flowLayoutPanel3.Location = new Point(65, 314);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(559, 172);
            flowLayoutPanel3.TabIndex = 6;
            flowLayoutPanel3.Paint += flowLayoutPanel3_Paint;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.BackColor = SystemColors.ControlLight;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Inter Light", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.ForeColor = SystemColors.WindowText;
            textBox1.InnerPadding = new Padding(12, 20, 12, 20);
            textBox1.Location = new Point(10, 10);
            textBox1.Margin = new Padding(10);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Username";
            textBox1.Size = new Size(536, 33);
            textBox1.TabIndex = 1;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.BackColor = SystemColors.ControlLight;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Inter Light", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.ForeColor = SystemColors.WindowText;
            textBox2.InnerPadding = new Padding(12, 20, 12, 20);
            textBox2.Location = new Point(10, 63);
            textBox2.Margin = new Padding(10);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.PlaceholderText = "Password";
            textBox2.Size = new Size(536, 33);
            textBox2.TabIndex = 1;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.None;
            textBox3.BackColor = SystemColors.ControlLight;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Inter Light", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.ForeColor = SystemColors.WindowText;
            textBox3.InnerPadding = new Padding(12, 20, 12, 20);
            textBox3.Location = new Point(10, 116);
            textBox3.Margin = new Padding(10);
            textBox3.Name = "textBox3";
            textBox3.PasswordChar = '*';
            textBox3.PlaceholderText = "Confirm Password";
            textBox3.Size = new Size(536, 33);
            textBox3.TabIndex = 1;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Inter Light", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(147, 255);
            label2.Name = "label2";
            label2.Size = new Size(387, 24);
            label2.TabIndex = 3;
            label2.Text = "Please sign in with the following details:";
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pictureBox2.Image = Properties.Resources.NextButton;
            pictureBox2.Location = new Point(70, 6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(22, 41);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            
            // 
            // Next
            // 
            Next.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Next.AutoSize = true;
            Next.Font = new Font("Inter Light", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Next.Location = new Point(10, 14);
            Next.Name = "Next";
            Next.Size = new Size(54, 24);
            Next.TabIndex = 1;
            Next.Text = "Next";
            Next.TextAlign = ContentAlignment.MiddleCenter;
            
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
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
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
        private RoundedTextBox textBox2;
        private RoundedTextBox textBox1;
        private FlowLayoutPanel flowLayoutPanel3;
        private RoundedTextBox textBox3;
        private Panel panel4;
        private Label Next;
        private PictureBox pictureBox2;
        private Panel panel1;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = string.IsNullOrEmpty(textBox1.Text) ? SystemColors.ControlLight : Color.LightBlue;
        }

        private void panel4_Click(object sender, EventArgs e)
        {

        }
    }
}
