using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private string targetText;
        private int currentPosition;
        private int errorCount;
        private DateTime startTime;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateText();
            targetTextBlock.Text = targetText;
            ResetStatistics();
            startTime = DateTime.Now;
            currentPosition = 0;
            inputTextBox.IsEnabled = true;
            inputTextBox.Focus();
        }

        private void GenerateText()
        {
            int textLength = 25;
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyz";
            targetText = "";

            for (int i = 0; i < textLength; i++)
            {
                char randomChar = chars[random.Next(chars.Length)];

                if (difficultyComboBox.SelectedItem.ToString() == "Easy")
                {
                    targetText += randomChar;
                }
                else if (difficultyComboBox.SelectedItem.ToString() == "Medium")
                {
                    if (random.Next(2) == 0)
                        targetText += char.ToUpper(randomChar);
                    else
                        targetText += randomChar;
                }
                else
                {
                    targetText += char.ToUpper(randomChar);
                }
            }
        }

        private void ResetStatistics()
        {
            errorCount = 0;
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            double elapsedMinutes = (DateTime.Now - startTime).TotalMinutes;
            double speed = (currentPosition / 5) / elapsedMinutes;
            statisticsTextBlock.Text = $"Speed: {speed:0.00} WPM Errors: {errorCount}";
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inputTextBox.Text.Length > currentPosition)
            {
                char typedChar = inputTextBox.Text[currentPosition];
                char targetChar = targetText[currentPosition];

                if (typedChar == targetChar)
                {
                    currentPosition++;
                }
                else
                {
                    errorCount++;
                }

                UpdateStatistics();
            }

            if (currentPosition >= targetText.Length)
            {
                inputTextBox.IsEnabled = false;
            }
        }

        private void difficultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (targetText != null)
            {
                GenerateText();
                ResetStatistics();
                currentPosition = 0;
                inputTextBox.Text = targetText;
                inputTextBox.IsEnabled = true;
                inputTextBox.Focus();
            }
        }
    }
}