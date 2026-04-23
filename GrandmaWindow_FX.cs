using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Inflow
{
    public class GrandmaWindow_FX : Form
    {

        
        protected GrandmaWindow_FX()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = true;
            this.Size = new Size(800, 600);

        }
        /*
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int radius = 20;
            int borderThickness = 4;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(Width - radius, Height - radius, radius, radius, 0, 90);
                path.AddArc(0, Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                this.Region = new Region(path);

                using (Pen pen = new Pen(Color.White, borderThickness))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
        */
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;
            const int SC_RESTORE = 0xF120;
            const int WM_NCHITTEST = 0x0084;
            const int resizeAreaSize = 10;

            #region Form Resize
            // ... keep all your resize code exactly as is ...
            #endregion

            // Remove border and keep snap window
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }

            // COMMENT OUT OR DELETE THIS ENTIRE BLOCK:
            /*
            //Keep form size when it is minimized and restored
            if (m.Msg == WM_SYSCOMMAND)
            {
                int wParam = (m.WParam.ToInt32() & 0xFFF0);

                if (wParam == SC_MINIMIZE)
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)
                    this.Size = formSize;
            }
            */

            base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrandmaWindow_FX));
            SuspendLayout();
            // 
            // GrandmaWindow_FX
            // 
            ClientSize = new Size(282, 253);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GrandmaWindow_FX";
            ResumeLayout(false);

        }

    }
}
