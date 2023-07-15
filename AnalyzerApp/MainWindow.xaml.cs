﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;
namespace AnalyzerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cancellationTokenSource;
        public MainWindow()
        {
            InitializeComponent();
            string folderPath = "C:\\Users\\HP\\source\\repos\\AnalyzerApp\\AnalyzerApp\\MyFiles"; 
            PopulateFileListBox(folderPath);
        }

        private void PopulateFileListBox(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath);
                foreach (var file in files)
                {
                    FileListBox.Items.Add(System.IO.Path.GetFileName(file));
                }
            }
            else
            {
                MessageBox.Show("Folder not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    
    private async void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedFile = FileListBox.SelectedItem?.ToString();
            if (selectedFile == null)
            {
                MessageBox.Show("Please select a file to analyze.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                cancellationTokenSource = new CancellationTokenSource();

                // Start the analysis in a separate task
                var analysisTask = AnalyzeFileAsync(selectedFile, cancellationTokenSource.Token);

                // Start the progress bar update task
                var progressBarTask = UpdateProgressBarAsync(analysisTask, cancellationTokenSource.Token);

                // Wait for the analysis to complete
                await analysisTask;

                // Cancel the progress bar update task if it's still running
                cancellationTokenSource.Cancel();
                await progressBarTask;
            }
            catch (OperationCanceledException)
            {
                // The analysis was cancelled, handle it as needed
                MessageBox.Show("Analysis cancelled.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"An error occurred during analysis: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AnalyzeFileAsync(string filePath, CancellationToken cancellationToken)
        {
            var extension = System.IO.Path.GetExtension(filePath);
            switch (extension)
            {
                case ".json":
                    await AnalyzeJsonFileAsync(filePath, cancellationToken);
                    break;
                case ".txt":
                    await AnalyzeTextFileAsync(filePath, cancellationToken);
                    break;
                default:
                    throw new NotSupportedException("Unsupported file type.");
            }
        }
        private Task AnalyzeJsonFileAsync(string filePath, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var fileContent = File.ReadAllText(filePath);
                var jsonDocument = JsonDocument.Parse(fileContent);

                var characters = fileContent.Length;
                var words = CountWordsInJsonFile(filePath);
                var sentences = Regex.Matches(fileContent, @"[.!?]").Count;

                // Update UI with the analysis results
                Dispatcher.Invoke(() =>
                {
                    CharactersLabel.Content = $"Characters: {characters}";
                    WordsLabel.Content = $"Words: {words}";
                    SentencesLabel.Content = $"Sentences: {sentences}";
                });
            }, cancellationToken);
        }


        public int CountWordsInJsonFile(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);
            var jsonWithoutSymbols = Regex.Replace(fileContent, @"[^a-zA-Zа-яА-Я0-9\s]", "");
            var words = jsonWithoutSymbols.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        private Task AnalyzeTextFileAsync(string filePath, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var fileContent = File.ReadAllText(filePath);
                var characters = fileContent.Length;
                var words = fileContent.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
                var sentences = Regex.Matches(fileContent, @"[.!?]").Count;

                // Update UI with the analysis results
                Dispatcher.Invoke(() =>
                {
                    CharactersLabel.Content = $"Characters: {characters}";
                    WordsLabel.Content = $"Words: {words}";
                    SentencesLabel.Content = $"Sentences: {sentences}";
                });
            }, cancellationToken);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }



        private async Task UpdateProgressBarAsync(Task analysisTask, CancellationToken cancellationToken)
        {
            ProgressBar.IsIndeterminate = true;
            while (!analysisTask.IsCompleted && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(50); 
            }

            // Hide the progress bar after analysis is completed or cancelled
            ProgressBar.IsIndeterminate = false;
        }

    }
}
