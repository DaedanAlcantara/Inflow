using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Inflow
{
    public class MotherWindow : AbstractWindow
    {
        private int borderSize = 0;
        private Size formSize;
        
        public MotherWindow()
        {
            this.Padding = new System.Windows.Forms.Padding(borderSize);
            this.TopMost = true;
            this.WindowState = FormWindowState.Normal;
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
        }

        

        
    }
}
