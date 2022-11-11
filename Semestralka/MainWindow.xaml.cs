using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    public partial class MainWindow : Window
    {
        public string? OpenedFile { get; set; } = null;
        public TypeScriptInterpreter.Interpreter Interpreter { get; } = new TypeScriptInterpreter.Interpreter();
        public MainWindow()
        {
            TypeScriptInterpreter.OutputProvider.In = ReadLine;
            TypeScriptInterpreter.OutputProvider.Out = WriteLine;
            InitializeComponent();
        }

        private void WriteLine(string text)
        {
            ConsoleTextBox.Text += text + Environment.NewLine;
        }

        private string? ReadLine()
        {
            ConsoleReadDialog consoleReadDialog = new ConsoleReadDialog();
            bool? dialogResult = consoleReadDialog.ShowDialog();
            if (dialogResult == null || dialogResult == false) return "";
            string result = consoleReadDialog.Result;
            WriteLine(result);
            return result;
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Typescript by peter|*.tsp";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            bool? openResult = saveFileDialog.ShowDialog();
            
            if (openResult == null || openResult == false) return;

            File.WriteAllText(saveFileDialog.FileName, CodeTextBox.Text);
        }

        private void RunButtonClicked(object sender, RoutedEventArgs e)
        {
            Interpreter.Interpret(OpenedFile);
        }

        private void LoadButtonClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Typescript by peter|*.tsp";
            bool? openResult = openFileDialog.ShowDialog();
            
            if (openResult == null || openResult == false) return;

            if (OpenedFile != null)
            {
                HandleLoadWithoutSave();
            }

            OpenedFile = openFileDialog.FileName;

            CodeTextBox.Text = File.ReadAllText(OpenedFile);
        }

        private void HandleLoadWithoutSave()
        {
            
        }
    }
}
