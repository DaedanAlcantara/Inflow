namespace Inflow
{
    public partial class Form1 : Form
    {
        private int borderSize = 0;

        public Form1()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;
            this.Padding = new System.Windows.Forms.Padding(borderSize);
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#FF37E8");

            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;


        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
            
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#FF37E8");
        }
     
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
