using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

public class RoundedTextBox : TextBox
{
    private int _cornerRadius = 15;
    private Padding _innerPadding = new Padding(12, 8, 12, 8);

    [DefaultValue(15)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int CornerRadius
    {
        get => _cornerRadius;
        set
        {
            _cornerRadius = value;
            ApplyRoundedRegion();
            Invalidate();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Padding InnerPadding
    {
        get => _innerPadding;
        set
        {
            _innerPadding = value;
            ApplyPadding();
            Invalidate();
        }
    }

    public RoundedTextBox()
    {
        this.BorderStyle = BorderStyle.None;
        ApplyPadding();
    }

    private void ApplyPadding()
    {
        // Set the internal padding for the text box
        this.Padding = _innerPadding;
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        ApplyRoundedRegion();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        ApplyRoundedRegion();
    }

    protected override void OnPaddingChanged(EventArgs e)
    {
        base.OnPaddingChanged(e);
        // Sync internal padding if changed externally
        _innerPadding = this.Padding;
    }

    private void ApplyRoundedRegion()
    {
        if (Width <= 0 || Height <= 0) return;

        using (GraphicsPath path = new GraphicsPath())
        {
            Rectangle rect = new Rectangle(0, 0, Width, Height);

            // Ensure corner radius doesn't exceed half the smallest dimension
            int r = Math.Min(_cornerRadius, Math.Min(Width / 2, Height / 2));
            r = Math.Max(1, r);

            // Create rounded rectangle (using r*2 for arc dimensions)
            path.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
            path.AddArc(rect.Right - (r * 2), rect.Y, r * 2, r * 2, 270, 90);
            path.AddArc(rect.Right - (r * 2), rect.Bottom - (r * 2), r * 2, r * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - (r * 2), r * 2, r * 2, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }
    }

    // Optional: Override OnPaint to draw a border (since BorderStyle is None)
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Draw a custom border if needed
        using (GraphicsPath path = new GraphicsPath())
        using (Pen pen = new Pen(Color.Gray, 2))
        {
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int r = Math.Min(_cornerRadius, Math.Min(rect.Width / 2, rect.Height / 2));
            r = Math.Max(1, r);

            path.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
            path.AddArc(rect.Right - (r * 2), rect.Y, r * 2, r * 2, 270, 90);
            path.AddArc(rect.Right - (r * 2), rect.Bottom - (r * 2), r * 2, r * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - (r * 2), r * 2, r * 2, 90, 90);
            path.CloseFigure();

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawPath(pen, path);
        }
    }

    // Ensure proper redrawing
    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        Invalidate();
    }
}