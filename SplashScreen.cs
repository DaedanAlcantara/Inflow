using System.Runtime.InteropServices;

namespace Inflow
{
    public partial class SplashScreen : MotherWindow
    {
        private GradientPanel panel2;


        public SplashScreen()
        {
            InitializeComponent();
            panel2.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#01FBCE");
            panel2.ColorTop = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            this.FormBorderStyle = FormBorderStyle.None;

        }

        private void InitializeComponent()
        {
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
            panel2.Size = new Size(344, 647);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // SplashScreen
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(796, 647);
            Controls.Add(panel2);
            Controls.Add(panel1);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "SplashScreen";
            ShowIcon = false;
            ShowInTaskbar = false;
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

        private Panel panel1;
    }
}
