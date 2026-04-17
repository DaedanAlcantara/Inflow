namespace Inflow
{
    public partial class Form1 : Form
    {
        private int borderSize = 0;

        public Form1()
        {
            InitializeComponent();
            this.Padding = new System.Windows.Forms.Padding(borderSize);
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#FF37E8");
            int x = (pictureBox1.ClientSize.Width - pictureBox1.Width) / 2;
            int y = (pictureBox1.ClientSize.Height - pictureBox1.Height) / 2;
            pictureBox1.Location = new Point(x, y);
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;

        }
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
