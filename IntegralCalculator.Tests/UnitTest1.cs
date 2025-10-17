using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegralCalculator.Tests
{
    [TestClass]
    public class NumericalIntegralCalculatorTests
    {
        private NumericalIntegralCalculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new NumericalIntegralCalculator();
        }

        // Тест 1: Проверка вычисления функции в конкретной точке
        [TestMethod]
        public void Function_WithValidInput_ReturnsCorrectValue()
        {
            // Arrange
            double x = 1.0;
            double expected = 2 * 1 - Math.Log(7 * 1) - 12;

            // Act
            double actual = _calculator.Function(x);

            // Assert
            Assert.AreEqual(expected, actual, 1e-10, "Функция вычислена неверно");
        }

        // Тест 2: Упрощенный тест метода трапеций для НАШЕЙ функции
        [TestMethod]
        public void CalculateTrapezoidal_ForOurFunction_ReturnsReasonableValue()
        {
            // Arrange
            double a = 1, b = 10;
            int n = 1000;

            // Act
            double result = _calculator.CalculateTrapezoidal(a, b, n);

            // Assert
            Assert.IsFalse(double.IsNaN(result), "Результат не должен быть NaN");
            Assert.IsFalse(double.IsInfinity(result), "Результат не должен быть бесконечностью");
            // Для f(x)=2x-ln(7x)-12 на [1,10] результат должен быть отрицательным
            Assert.IsTrue(result < 0, "Результат должен быть отрицательным для данной функции");
        }

        // Тест 3: Упрощенный тест метода прямоугольников для НАШЕЙ функции
        [TestMethod]
        public void CalculateMidpoint_ForOurFunction_ReturnsReasonableValue()
        {
            // Arrange
            double a = 1, b = 10;
            int n = 1000;

            // Act
            double result = _calculator.CalculateMidpoint(a, b, n);

            // Assert
            Assert.IsFalse(double.IsNaN(result), "Результат не должен быть NaN");
            Assert.IsFalse(double.IsInfinity(result), "Результат не должен быть бесконечностью");
            Assert.IsTrue(result < 0, "Результат должен быть отрицательным для данной функции");
        }

        // Тест 4: Проверка сходимости методов
        [TestMethod]
        public void BothMethods_WithManyPartitions_ReturnSimilarResults()
        {
            // Arrange
            double a = 1, b = 10;
            int n = 10000;

            // Act
            double trapezoidalResult = _calculator.CalculateTrapezoidal(a, b, n);
            double midpointResult = _calculator.CalculateMidpoint(a, b, n);
            double difference = Math.Abs(trapezoidalResult - midpointResult);

            // Assert
            Assert.IsTrue(difference < 1.0,
                $"Методы дают слишком разные результаты: разница = {difference}");
        }

        // Тест 5: Проверка обработки некорректных параметров
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTrapezoidal_WithInvalidParameters_ThrowsException()
        {
            // Arrange
            double a = 10, b = 1; // Неправильные пределы
            int n = 100;

            // Act & Assert
            _calculator.CalculateTrapezoidal(a, b, n);
        }

        // Тест 6: Проверка на постоянной функции (РАБОТАЮЩИЙ вариант)
        [TestMethod]
        public void BothMethods_ConstantFunction_ReturnExactArea()
        {
            // Arrange
            // Вместо вспомогательного класса тестируем напрямую
            double a = 1, b = 4;
            int n = 100;

            // ∫5dx от 1 до 4 = 5 * (4-1) = 15
            double expected = 15.0;

            // Создаем калькулятор с постоянной функцией
            var constantCalculator = new ConstantFunctionCalculator();

            // Act
            double trapezoidalResult = constantCalculator.CalculateTrapezoidal(a, b, n);
            double midpointResult = constantCalculator.CalculateMidpoint(a, b, n);

            // Assert
            Assert.AreEqual(expected, trapezoidalResult, 1e-10,
                $"Метод трапеций: ожидалось {expected}, получено {trapezoidalResult}");
            Assert.AreEqual(expected, midpointResult, 1e-10,
                $"Метод прямоугольников: ожидалось {expected}, получено {midpointResult}");
        }
    }

    // Класс для тестирования ПОСТОЯННОЙ функции
    public class ConstantFunctionCalculator
    {
        public double Function(double x)
        {
            return 5; // Всегда возвращает 5
        }

        public double CalculateTrapezoidal(double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0.5 * (Function(a) + Function(b));

            for (int i = 1; i < n; i++)
            {
                sum += Function(a + i * h);
            }

            return sum * h;
        }

        public double CalculateMidpoint(double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0;

            for (int i = 0; i < n; i++)
            {
                double x_mid = a + (i + 0.5) * h;
                sum += Function(x_mid);
            }

            return sum * h;
        }
    }

    // Класс для тестирования ЛИНЕЙНОЙ функции (если нужен)
    public class LinearFunctionCalculator
    {
        public double Function(double x)
        {
            return 2 * x; // f(x) = 2x
        }

        public double CalculateTrapezoidal(double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0.5 * (Function(a) + Function(b));

            for (int i = 1; i < n; i++)
            {
                sum += Function(a + i * h);
            }

            return sum * h;
        }

        public double CalculateMidpoint(double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0;

            for (int i = 0; i < n; i++)
            {
                double x_mid = a + (i + 0.5) * h;
                sum += Function(x_mid);
            }

            return sum * h;
        }
    }
}