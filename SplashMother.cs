using System;
using System.Collections.Generic;
using System.Text;

namespace Inflow
{
    public class SplashMother : AbstractWindow
    {
        private Size formSize;

        public SplashMother()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(400, 300);
            this.BackColor = Color.White;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.DoubleBuffered = true;
        }

        public SplashMother(Size formSize)
        {
            this.formSize = formSize;
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

