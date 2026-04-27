using System;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Inflow
{
    public partial class Nitro_FX : UserControl
    {
        private User_BX currentUser;

        public Nitro_FX()
        {
            InitializeComponent();
        }

        private void Nitro_FX_Load(object sender, EventArgs e)
        {
            CenterPanelsHorizontally();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterPanelsHorizontally();
        }

        private void CenterPanelsHorizontally()
        {
            CenterPanel(NextTaskDisplayPanel);
            CenterPanel(NextTask2DisplayPanel);
            CenterPanel(NextTask3DisplayPanel);
            CenterPanel(CurrentTaskDisplayPanel);
            CenterPanel(StopButton);    // optional for the buttons
            CenterButtonRow();
        }

        private void CenterPanel(Control panel)
        {
            panel.Left = (this.ClientSize.Width - panel.Width) / 2;
        }

        private void CenterButtonRow()
        {
            int totalWidth = StopButton.Width + 55 + NextTaskButton.Width; // 55 = gap between them
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            StopButton.Left = startX;
            NextTaskButton.Left = startX + StopButton.Width + 55;
        }

        internal void SetUser(User_BX user)
        {
            if (currentUser == null)
            {
                currentUser = user;
            }
            else
            {
                throw new InvalidOperationException("User has already been set for this Nitro_FX instance.");
            }
        }
    }
}