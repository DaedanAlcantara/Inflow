using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Inflow
{
    public class MotherWindowFX : GrandmaWindow_FX
    {
        private int borderSize = 0;
        
        public MotherWindowFX()
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
