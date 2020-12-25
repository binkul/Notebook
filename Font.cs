using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Notatnik
{
    public struct Font
    {
        public FontFamily Family { get; set; }
        public string FamilyName
        {
            get
            {
                return Family.ToString();
            }
        }
        public FontStyle Style { get; set; }
        public FontWeight Weight { get; set; }
        public TextDecorationCollection TextDecoration { get; set; }
        public double Size { get; set; }
        public Color Color { get; set; }
        public Brush Brush
        {
            get
            {
                return new SolidColorBrush(Color);
            }
        }

        public static Font Default
        {
            get
            {
                return new Font()
                {
                    Family = new FontFamily("Segoe UI"),
                    Style = FontStyles.Normal,
                    Weight = FontWeights.Normal,
                    TextDecoration = null,
                    Size = 12,
                    Color = Colors.Black
                };
            }
        }

        public static System.Drawing.Font MapToSystemDrawingFont(Font font)
        {
            System.Drawing.FontStyle style = (font.Style == FontStyles.Italic) ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular;
            if (font.Weight == FontWeights.Bold) 
                style |= System.Drawing.FontStyle.Bold;
            if (font.TextDecoration.Contains(TextDecorations.Underline[0]))
                style |= System.Drawing.FontStyle.Underline;
            if (font.TextDecoration.Contains(TextDecorations.Strikethrough[0]))
                style |= System.Drawing.FontStyle.Strikeout;

            return new System.Drawing.Font(font.FamilyName, (int)font.Size, style);
        }

        public static Font MapToFont(System.Drawing.Font sdFont, System.Drawing.Color sdColor)
        {
            Font font = new Font();
            font.Family = new FontFamily(sdFont.FontFamily.Name);
            font.Style = sdFont.Italic ? FontStyles.Italic : FontStyles.Normal;
            font.Weight = sdFont.Bold ? FontWeights.Bold : FontWeights.Normal;
            font.TextDecoration = new TextDecorationCollection();
            if (sdFont.Underline)
                font.TextDecoration.Add(System.Windows.TextDecorations.Underline);
            if (sdFont.Strikeout)
                font.TextDecoration.Add(TextDecorations.Strikethrough);
            font.Size = sdFont.Size;
            font.Color = sdColor.Convert();
            return font;
        }

        public void ApplyTo(Control control)
        {
            control.FontFamily = Family;
            control.FontStyle = Style;
            control.FontWeight = Weight;
            control.FontSize = Size;
            control.Foreground = Brush;
        }

        public void ApplyTo(TextBox textBox)
        {
            ApplyTo(textBox as Control);
            textBox.TextDecorations = TextDecoration;
        }

        public void ApplyTo(TextBlock textBlock)
        {
            textBlock.FontFamily = Family;
            textBlock.FontStyle = Style;
            textBlock.FontWeight = Weight;
            textBlock.TextDecorations = TextDecoration;
            textBlock.FontSize = Size;
            textBlock.Foreground = Brush;
        }

        public static Font ExtractFrom(Control control)
        {
            Color color = Colors.Black;
            if (control.Foreground is SolidColorBrush)
                color = (control.Foreground as SolidColorBrush).Color;
            Font font = new Font()
            {
                Family = control.FontFamily,
                Style = control.FontStyle,
                Weight = control.FontWeight,
                TextDecoration = null,
                Size = control.FontSize,
                Color = color
            };

            if (control is TextBox)
                font.TextDecoration = (control as TextBox).TextDecorations;
            return font;
        }
    }
}
