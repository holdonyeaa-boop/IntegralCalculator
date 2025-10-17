using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace IntegralCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int partitions = int.Parse(txtPartitions.Text);
                if (partitions < 0)
                {
                    MessageBox.Show("Количество разбиений должно быть положительным числом!", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double lowerLimit = 0;
                double upperLimit = 10;

                IIntegralCalculator calculator = new NumericalIntegralCalculator();

                var stopwatch = Stopwatch.StartNew();
                double trapezoidalResult = calculator.CalculateTrapezoidal(lowerLimit, upperLimit, partitions);
                double midpointResult = calculator.CalculateMidpoint(lowerLimit, upperLimit, partitions);
                stopwatch.Stop();

                double difference = Math.Abs(trapezoidalResult - midpointResult);

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txtTrapezoidalResult.Text = $"Метод трапеций: {trapezoidalResult:F2}";
                    txtMidpointResult.Text = $"Метод средних прямоугольников: {midpointResult:F2}";
                    txtDifference.Text = $"Разница между методами: {difference:E5}";
                    txtTime.Text = $"Время вычисления: {stopwatch.ElapsedMilliseconds} мс";
                }), DispatcherPriority.Background);

            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректное целое число для количества разбиений!",
                              "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка в параметрах: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при вычислении: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}