using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TypeScriptInterpreter;

namespace Semestralka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TextEditorWindow : Window
    {
        public TextEditorWindow()
        {
            InitializeComponent();
            DataContext = App.Current.MainVM;
        }

        private void ConsoleTextBoxKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void CodeTextboxChanged(object sender, KeyEventArgs e)
        {
        }
    }
}
