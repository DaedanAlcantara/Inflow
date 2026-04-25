using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace Inflow
{
    public class RoundedPanel_CMP : Panel
    {
        private int _cornerRadius = 20;
        private Color _panelColor = Color.IndianRed;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PanelColor
        {
            get => _panelColor;
            set { _panelColor = value; Invalidate(); }
        }

        public RoundedPanel_CMP()
        {
            // Required for custom painting to work cleanly
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Suppress default background to avoid white flash
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using GraphicsPath path = GetRoundedRectPath(rect, _cornerRadius);
            using SolidBrush brush = new SolidBrush(_panelColor);

            g.FillPath(brush, path);

            // Clip child controls to rounded shape
            this.Region = new Region(path);
        }

        private static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);                              // Top-left
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);                      // Top-right
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);               // Bottom-right
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);                      // Bottom-left
            path.CloseFigure();

            return path;
        }
    }
}