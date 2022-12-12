using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading;
using Microsoft.Win32;
using System.Windows;
using System.IO;

namespace Semestralka.ViewModel;

public partial class TextEditorViemModel : ObservableObject
{
    [ObservableProperty]
    private string? _openedFile = null;

    [ObservableProperty]
    private string _openedFileContext = "";

    [ObservableProperty]
    private string _consoleContext = "";

    [ObservableProperty]
    private string _consoleRead = "";

    [ObservableProperty]
    private bool _disabledConsole = true;

    private Thread codeThread;
    private string readInput = "";
    private TypeScriptInterpreter.Interpreter interpreter = new TypeScriptInterpreter.Interpreter();
    private bool fileSaved = false;

    public TextEditorViemModel()
    {
        TypeScriptInterpreter.OutputProvider.In = ReadLine;
        TypeScriptInterpreter.OutputProvider.Out = WriteLine;
        TypeScriptInterpreter.OutputProvider.Error = WriteError;
    }

    [RelayCommand]
    private void Save()
    {
        if (OpenedFile != null)
        {
            File.WriteAllText(OpenedFile, OpenedFileContext);
            fileSaved = true;
        }
    }

    [RelayCommand]
    private void SaveAs()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Typescript|*.ts";
        saveFileDialog.FilterIndex = 2;
        saveFileDialog.RestoreDirectory = true;
        bool? openResult = saveFileDialog.ShowDialog();

        if (openResult == null || openResult == false) return;

        OpenedFile = saveFileDialog.FileName;

        File.WriteAllText(saveFileDialog.FileName, OpenedFileContext);
        fileSaved= true;
    }

    [RelayCommand]
    private void Load()
    {
        if (OpenedFile != null && fileSaved == false)
        {
            HandleLoadWithoutSave();
        }

        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Typescript|*.ts";
        bool? openResult = openFileDialog.ShowDialog();

        if (openResult == null || openResult == false) return;

        OpenedFile = openFileDialog.FileName;

        OpenedFileContext = File.ReadAllText(OpenedFile);
    }

    [RelayCommand]
    private void Run()
    {
        if (OpenedFile != null)
        {
            ConsoleContext = $">>>> program started{Environment.NewLine}";
            codeThread = new Thread(() =>
            {
                interpreter.Interpret(OpenedFile);
                ConsoleContext += $"{Environment.NewLine}>>>> program end";
            });

            codeThread.Start();
        }
    }

    [RelayCommand]
    private void EndRead(string input)
    {
        readInput = input;
        DisabledConsole = true;
    }

    private void HandleLoadWithoutSave()
    {
        string messageBoxText = "Do you want to save changes?";
        string caption = "Word Processor";
        MessageBoxButton button = MessageBoxButton.YesNo;
        MessageBoxImage icon = MessageBoxImage.Warning;
        MessageBoxResult result;

        result = MessageBox.Show(messageBoxText, caption, button, icon);

        if (result == MessageBoxResult.Yes)
        {
            SaveAs();
        }

        return;
    }

    private void WriteError(Exception e)
    {
        ConsoleContext = $"Uncaught exception {e.GetType().Name}: {e.Message}{Environment.NewLine}";
    }

    private void WriteLine(string text)
    {
        ConsoleContext += text + Environment.NewLine;
    }

    private string? ReadLine()
    {
        ConsoleContext += ">>>> reading console";
        DisabledConsole = false;
        while (!DisabledConsole) ;
        ConsoleContext += $"{Environment.NewLine}>>>> reading console result: {readInput}";

        ConsoleRead = "";
        OnPropertyChanged(nameof(ConsoleRead));

        return readInput;
    }
}
