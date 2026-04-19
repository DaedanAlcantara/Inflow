using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Inflow
{
    public class MotherWindow : GrandmaWindow
    {
        private int borderSize = 0;
        
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
