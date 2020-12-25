using System;
using System.Windows.Media;

namespace Notatnik
{
    public static class WindowsFormsHelper
    {
        public static System.Drawing.Color Convert(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static Color Convert(this System.Drawing.Color color)
        {
            return new Color()
            {
                A = color.A,
                R = color.R,
                G = color.G,
                B = color.B
            };
        }

        public static bool ChooseColor(ref Color color)
        {
            using (System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog())
            {
                colorDialog.Color = color.Convert();
                colorDialog.AllowFullOpen = true;

                var result = colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK;

                if (result) color = colorDialog.Color.Convert();
                return result;
            }
        }

        public static bool ChooseFont(ref Font font)
        {
            using (System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog())
            {
                fontDialog.ShowColor = true;
                fontDialog.ShowEffects = true;
                fontDialog.Font = Font.MapToSystemDrawingFont(font);
                fontDialog.Color = font.Color.Convert();

                bool result = fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK;

                if (result) font = Font.MapToFont(fontDialog.Font, fontDialog.Color);
                return result;
            }
        }

    }
}
