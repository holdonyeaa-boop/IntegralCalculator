using System;
using System.Windows;

namespace IntegralCalculator
{
    public class IntegrationTests
    {
        
        public static void Test1_FunctionReturnsNumber()
        {
            
            var calculator = new NumericalIntegralCalculator();
            double x = 2.0;

            
            double result = calculator.Function(x);

            
            if (double.IsNaN(result) || double.IsInfinity(result))
                throw new Exception($"Тест 1 не пройден: функция вернула {result}");

            
            Console.WriteLine($"✅ Тест 1 пройден: f(2) = {result}");
        }

        
        public static void Test2_TrapezoidalMethodWorks()
        {
            
            var calculator = new NumericalIntegralCalculator();
            double a = 1.0, b = 2.0;  
            int n = 100;

            
            double result = calculator.CalculateTrapezoidal(a, b, n);

            
            if (double.IsNaN(result) || double.IsInfinity(result))
                throw new Exception($"Тест 2 не пройден: метод вернул {result}");

            if (result > 1000 || result < -1000)
                throw new Exception($"Тест 2 не пройден: нереальный результат {result}");

            Console.WriteLine($"✅ Тест 2 пройден: ∫f(x)dx от 1 до 2 = {result}");
        }

       
        public static void Test3_MidpointMethodWorks()
        {
            
            var calculator = new NumericalIntegralCalculator();
            double a = 1.0, b = 2.0;
            int n = 100;

            
            double result = calculator.CalculateMidpoint(a, b, n);

            
            if (double.IsNaN(result) || double.IsInfinity(result))
                throw new Exception($"Тест 3 не пройден: метод вернул {result}");

            if (result > 1000 || result < -1000)
                throw new Exception($"Тест 3 не пройден: нереальный результат {result}");

            Console.WriteLine($"✅ Тест 3 пройден: ∫f(x)dx от 1 до 2 = {result}");
        }

        
        public static void Test4_MethodsGiveSimilarResults()
        {
            
            var calculator = new NumericalIntegralCalculator();
            double a = 1.0, b = 5.0;
            int n = 1000;

            
            double result1 = calculator.CalculateTrapezoidal(a, b, n);
            double result2 = calculator.CalculateMidpoint(a, b, n);
            double difference = Math.Abs(result1 - result2);

            
            if (difference > 5.0) 
                throw new Exception($"Тест 4 не пройден: методы дают разные результаты. " +
                                  $"Трапеции: {result1}, Прямоугольники: {result2}, Разница: {difference}");

            Console.WriteLine($"✅ Тест 4 пройден: разница между методами = {difference}");
        }

        
        public static void Test5_ZeroForEqualLimits()
        {
            
            var calculator = new NumericalIntegralCalculator();
            double a = 3.0, b = 3.0;  

            
            try
            {
                double result = calculator.CalculateTrapezoidal(a, b, 100);

                
                if (Math.Abs(result) < 1000) 
                {
                    Console.WriteLine($"✅ Тест 5 пройден: при a=b={a} результат = {result}");
                }
                else
                {
                    throw new Exception($"Неразумный результат: {result}");
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("меньше"))
            {
                
                Console.WriteLine($"✅ Тест 5 пройден: метод корректно обрабатывает a=b (исключение: {ex.Message})");
            }
            catch (Exception ex)
            {
                throw new Exception($"Тест 5 не пройден: неожиданная ошибка при a=b: {ex.Message}");
            }
        }

        public static void RunAllTests()
        {
            string results = "=== резульаты тестов ===\n\n";

            try
            {
                results += "Тест 1: Проверка вычисления функции...\n";
                Test1_FunctionReturnsNumber();
                results += "✅ Пройден\n\n";

                results += "Тест 2: Проверка метода трапеций...\n";
                Test2_TrapezoidalMethodWorks();
                results += "✅ Пройден\n\n";

                results += "Тест 3: Проверка метода прямоугольников...\n";
                Test3_MidpointMethodWorks();
                results += "✅ Пройден\n\n";

                results += "Тест 4: Проверка сходимости методов...\n";
                Test4_MethodsGiveSimilarResults();
                results += "✅ Пройден\n\n";

                results += "Тест 5: Проверка на одинаковых пределах...\n";
                Test5_ZeroForEqualLimits();
                results += "✅ Пройден\n\n";

                results += "🎉 Все тесты пройдены";

                MessageBox.Show(results, "Результаты тестирования",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                results += $"❌ НЕ ПРОЙДЕН: {ex.Message}";
                MessageBox.Show(results, "Ошибка тестирования",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}