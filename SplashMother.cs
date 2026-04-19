using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Inflow
{
    public class SplashMother : GrandmaWindow
    {

        public SplashMother()
        {
            
        }

        public SplashMother(Size formSize)
        {
        }

        

        




        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // SplashMother
            // 
            ClientSize = new Size(318, 347);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "SplashMother";
            ShowIcon = false;
            ShowInTaskbar = false;
            ResumeLayout(false);

        }
    }
}

