
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Notatnik
{
    public static class Printing
    {
        private static FlowDocument createFlowDocument(string [] lines, Font font, double pageWidth)
        {
            var fd = new FlowDocument();

            fd.Background = Brushes.White;
            fd.Foreground = Brushes.Black;

            fd.FontFamily = font.Family;
            fd.FontStyle = font.Style;
            fd.FontWeight = font.Weight;
            fd.FontSize = font.Size;

            fd.ColumnGap = 0;
            fd.ColumnWidth = pageWidth;

            foreach (string line in lines)
            {
                Paragraph paragraph = new Paragraph(new Run(line));
                fd.Blocks.Add(paragraph);
            }

            return fd;
        }

        public static void PrintText(string[] lines, Font font)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                FlowDocument fd = createFlowDocument(lines, font, printDialog.PrintableAreaWidth);
                printDialog.PrintDocument((fd as IDocumentPaginatorSource).DocumentPaginator, fd.Name);
            }
        }

        public static void PrintText(string text, Font font)
        {
            string[] lines = text.Split('\n');
            for (int i= 0; i < lines.Length; ++i)
                lines[i] = lines[i].TrimEnd('\r', ' ');
            PrintText(lines, font);
        }
    }
}
