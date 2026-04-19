using System.Runtime.InteropServices;

namespace Inflow
{
    public partial class SplashScreen : MotherWindow
    {
        private GradientPanel panel2;


        public SplashScreen()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            panel2.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#01FBCE");
            panel2.ColorTop = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            this.MaximumSize = this.MinimumSize = this.Size;

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            panel1 = new Panel();
            panel2 = new GradientPanel();
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
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(344, 712);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // SplashScreen
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(999, 712);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SplashScreen";
            SizeGripStyle = SizeGripStyle.Hide;
            Load += SplashScreen_Load;
            ResumeLayout(false);

        }


        private void SplashScreen_Load(object sender, EventArgs e)
        {
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private System.ComponentModel.IContainer components;
        private Panel panel1;

        
        
    }
}
