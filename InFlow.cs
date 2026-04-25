using System.Runtime.InteropServices;
using static System.Windows.Forms.DataFormats;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Drawing.Text;
namespace Inflow
{
    public partial class InFlow : MotherWindowFX
    {
        
        public InFlow()
        {
           
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeComponent();
            this.Text = "Inflow";
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
            panel1.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#0E24F0");
            panel1.ColorTop = System.Drawing.ColorTranslator.FromHtml("#FF37E8");
            

            
        }

       
       
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
           
            SignUpForm_FX newForm = new SignUpForm_FX();
            timer1.Stop(); 
            this.Hide(); 
            newForm.TopMost = true;
            newForm.Show(); 
           
            
            /*
                MainWindowMother_FX newForm = new MainWindowMother_FX();
                timer1.Stop();
                this.Hide();
                newForm.TopMost = true;
                newForm.Show();
            */

        }
        private void Inflow_Load(object sender, EventArgs e)
        {
            
        }
    }
}
