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
            // Use a fixed border style and lock min/max size so instances cannot be resized
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();

            this.Size = new Size(999, 712);
            this.MinimumSize = this.MaximumSize = this.Size;
            this.BackColor = Color.White;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
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

