using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedTextBox_CMP : UserControl
{
    private TextBox innerTextBox;

    private int _cornerRadius = 5;
    private Color _borderColor = SystemColors.ControlLight;
    private int _borderThickness = 2;

    public RoundedTextBox_CMP()
    {
        this.DoubleBuffered = true;

        innerTextBox = new TextBox();
        innerTextBox.BorderStyle = BorderStyle.None;
        innerTextBox.Location = new Point(Padding.Left, Padding.Top);
        innerTextBox.Width = this.Width+30;
        innerTextBox.TextChanged += (s, e) => this.OnTextChanged(e);
        this.Controls.Add(innerTextBox);
        
        this.BackColor = Color.White; // safe now

    }

    public override string Text
    {
        get => innerTextBox.Text;
        set => innerTextBox.Text = value;
    }

    
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }

    // 🔹 Corner Radius
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CornerRadius
    {
        get => _cornerRadius;
        set
        {
            _cornerRadius = value;
            Invalidate();
        }
    }

    // 🔹 Border Thickness
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int BorderThickness
    {
        get => _borderThickness;
        set
        {
            _borderThickness = value;
            Invalidate();
        }
    }

    [Category("PlaceholderText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string PlaceholderText
    {
        get => innerTextBox.PlaceholderText;
        set => innerTextBox.PlaceholderText = value;
    }
    [Category("TextAlign")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public HorizontalAlignment TextAlign
    {
        get => innerTextBox.TextAlign;
        set => innerTextBox.TextAlign = value;
    }

    [Category("Location")]
    [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
    public Point InnerTextBoxLocation
    {
        get => innerTextBox.Location;
        set => innerTextBox.Location = value;
    }

    

    // 🔹 Handle resizing properly
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        int textHeight = TextRenderer.MeasureText("Text", innerTextBox.Font).Height;

        int y = (this.Height - textHeight) / 2;

        innerTextBox.Location = new Point(Padding.Left, y);
        innerTextBox.Width = Width - Padding.Left - Padding.Right;
    }

    // 🔹 Draw rounded border
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

        using (GraphicsPath path = GetRoundedPath(rect, _cornerRadius))
        using (Pen pen = new Pen(_borderColor, _borderThickness))
        {
            this.Region = new Region(path);
            e.Graphics.DrawPath(pen, path);
        }
    }

    private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();

        int r = Math.Min(radius, Math.Min(rect.Width / 2, rect.Height / 2));
        int d = r * 2;

        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

        path.CloseFigure();
        return path;
    }
   

    

    // Font
    public override Font Font
    {
        get => innerTextBox.Font;
        set
        {
            innerTextBox.Font = value;
            base.Font = value;
        }
    }

    // BackColor
    public override Color BackColor
    {
        get => innerTextBox.BackColor;
        set
        {
            innerTextBox.BackColor = value;
            base.BackColor = value;
            Invalidate();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public char PasswordChar
    {
        get => innerTextBox.PasswordChar;
        set => innerTextBox.PasswordChar = value;
    }

   
}