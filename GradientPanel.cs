using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

public class GradientPanel : Panel
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color ColorTop { get; set; } = Color.Blue;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color ColorBottom { get; set; } = Color.LightBlue;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using (LinearGradientBrush brush = new LinearGradientBrush(
            this.ClientRectangle,
            this.ColorTop,
            this.ColorBottom,
            LinearGradientMode.Vertical))
        {
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
    }
}