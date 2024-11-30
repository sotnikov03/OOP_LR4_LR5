using ControlzEx.Standard;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OOP_LR4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InputTextBox_LostFocus(null, null);
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = InputTextBox.Text;

            if (InputTextBox.Tag != null && input == (string)InputTextBox.Tag)
            {
                return;
            }

            if (input.Any(c => char.IsUpper(c) || (c >= 'A' && c <= 'Z')))
            {
                MessageBox.Show(
                    "Использование заглавных букв и символов английского алфавита недопустимо.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                InputTextBox.Text = new string(input.Where(c => !(char.IsUpper(c) || (c >= 'A' && c <= 'Z'))).ToArray());
                InputTextBox.CaretIndex = InputTextBox.Text.Length;
                return;
            }

            int charCount = input.Length;
            CharacterCountText.Text = charCount.ToString();

            if (charCount == 0)
            {
                StringDescriptionText.Text = string.Empty;
                StringDescriptionBorder.Background = new SolidColorBrush(Color.FromRgb(60, 60, 87));
            }
            else if (charCount < 10)
            {
                StringDescriptionText.Text = "Короткая строка";
                StringDescriptionBorder.Background = new SolidColorBrush(Color.FromRgb(85, 170, 85));
            }
            else if (charCount < 20)
            {
                StringDescriptionText.Text = "Средняя строка";
                StringDescriptionBorder.Background = new SolidColorBrush(Color.FromRgb(255, 204, 102));
            }
            else
            {
                StringDescriptionText.Text = "Длинная строка";
                StringDescriptionBorder.Background = new SolidColorBrush(Color.FromRgb(204, 85, 85));
            }
        }

        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text == (string)InputTextBox.Tag)
            {
                InputTextBox.Text = string.Empty;
                InputTextBox.Foreground = Brushes.White;
            }
        }

        private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                InputTextBox.Text = (string)InputTextBox.Tag;
                InputTextBox.Foreground = Brushes.Gray;
            }
        }

        private void LoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "C:\\Users\\Vyach\\source\\repos\\OOP_LR4\\input.txt"; 

            try
            {
                
                string fileContent = File.ReadAllText(filePath);

                
                InputTextBox.Text = fileContent;
            }
            catch (FileNotFoundException)
            {
                // Обрабатываем исключение, если файл не найден
                MessageBox.Show(
                    $"Файл \"{filePath}\" не найден. Проверьте, существует ли файл и указан ли правильный путь.",
                    "Ошибка чтения файла",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Обрабатываем другие возможные исключения
                MessageBox.Show(
                    $"Произошла ошибка при чтении файла: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
