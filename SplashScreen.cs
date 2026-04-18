namespace Inflow
{
    public partial class SplashScreen : MotherWindow
    {


        public SplashScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // SplashScreen
            // 
            ClientSize = new Size(817, 616);
            Name = "SplashScreen";
            Load += SplashScreen_Load;
            ResumeLayout(false);


        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
