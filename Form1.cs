using System.Runtime.InteropServices;
using static System.Windows.Forms.DataFormats;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Inflow
{
    public partial class Form1 : MotherWindow
    {
        
        public Form1()
        {
            InitializeComponent();

            //this.Resize += Form1_Resize;
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#FF37E8");



        }

       
        /*
        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
            
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#FF37E8");
        }
        */
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop(); // Stop timer to prevent multiple openings
            SplashScreen newForm = new SplashScreen();
            newForm.Show(this); // Open the new form
        }
    }
}
