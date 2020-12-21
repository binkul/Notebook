using System.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Notatnik
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeWindow();
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
    }
}
