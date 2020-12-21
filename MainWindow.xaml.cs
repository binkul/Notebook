using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace Notatnik
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private string filePath = null;
        private bool changedText = false;

        public MainWindow()
        {
            InitializeComponent();
            ResizeWindow();
            PrepareIoFileDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ResizeWindow()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;
            Notebook.Width = width - Convert.ToInt16(0.05 * width);
;           Notebook.Height = height - Convert.ToInt16(0.05 * height);
            Notebook.Top = (SystemParameters.WorkArea.Height - Notebook.Height) / 2;
            Notebook.Left = (SystemParameters.WorkArea.Width - Notebook.Width) / 2;
        }

        private void PrepareIoFileDialog()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Wybierz plik tekstowy";
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Filter = "Pliki tekstowe (*.txt)|*txt|Pliki XML (*.xml)|*.xml|Pliki źródłowe (*.cs)|*.cs|Wszystkie pliki (*.*)|*.*";

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Zapisz plik tekstowy";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = openFileDialog.Filter;
            saveFileDialog.FilterIndex = 1;
        }

        private void MenuItem_Otworz_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(filePath);
                openFileDialog.FileName = Path.GetFileName(filePath);
            }
            
            bool? result = openFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                filePath = openFileDialog.FileName;
                textBox.Text = File.ReadAllText(filePath);
                statusBarText.Text = Path.GetFileName(filePath);
                changedText = false;
            }
        }

        private void MenuItem_ZapiszJako_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(filePath);
                saveFileDialog.FileName = Path.GetFileName(filePath);
            }

            bool? result = saveFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, textBox.Text);
                statusBarText.Text = Path.GetFileName(filePath);
                changedText = false;
            }
        }

        private void MenuItem_Zapisz_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                File.WriteAllText(filePath, textBox.Text);
                changedText = false;
            }
            else
                MenuItem_ZapiszJako_Click(sender, e);
        }

        private void MenuItem_Zamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            changedText = true;
        }

        private void Notebook_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (changedText)
            {
                MessageBoxResult result = MessageBox.Show("Czy zapisać zmiany w edytowanym dokumencie?", this.Title,
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question,
                    MessageBoxResult.Cancel);

                switch(result)
                {
                    case MessageBoxResult.Yes:
                        MenuItem_Zapisz_Click(sender, null);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
