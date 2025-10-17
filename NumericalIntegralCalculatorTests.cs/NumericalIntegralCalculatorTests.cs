using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NumericalIntegralCalculatorTests.cs
namespace IIntegralCalculator.Tests
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
            // Arrange (подготовка)
            double x = 1.0;
            double expected = 2 * 1 - Math.Log(7 * 1) - 12;

            // Act (действие)
            double actual = _calculator.Function(x);

            // Assert (проверка)
            Assert.AreEqual(expected, actual, 1e-10, "Функция вычислена неверно");
        }

        // Тест 2: Проверка метода трапеций на известном интеграле
        [TestMethod]
        public void CalculateTrapezoidal_LinearFunction_ReturnsCorrectArea()
        {
            // Arrange
            // ∫(2x)dx от 0 до 2 = x²|₀² = 4
            var linearCalculator = new TestLinearCalculator();
            double a = 0, b = 2;
            int n = 1000;
            double expected = 4.0; // x² от 0 до 2 = 4

            // Act
            double actual = linearCalculator.CalculateTrapezoidal(a, b, n);

            // Assert
            Assert.AreEqual(expected, actual, 0.001, "Метод трапеций для линейной функции работает неверно");
        }

        // Тест 3: Проверка метода прямоугольников на известном интеграле
        [TestMethod]
        public void CalculateMidpoint_LinearFunction_ReturnsCorrectArea()
        {
            // Arrange
            // ∫(2x)dx от 0 до 2 = x²|₀² = 4
            var linearCalculator = new TestLinearCalculator();
            double a = 0, b = 2;
            int n = 1000;
            double expected = 4.0;

            // Act
            double actual = linearCalculator.CalculateMidpoint(a, b, n);

            // Assert
            Assert.AreEqual(expected, actual, 0.001, "Метод прямоугольников для линейной функции работает неверно");
        }

        // Тест 4: Проверка сходимости методов при увеличении разбиений
        [TestMethod]
        public void BothMethods_WithManyPartitions_ReturnSimilarResults()
        {
            // Arrange
            double a = 1, b = 10;
            int n = 10000; // Большое количество разбиений

            // Act
            double trapezoidalResult = _calculator.CalculateTrapezoidal(a, b, n);
            double midpointResult = _calculator.CalculateMidpoint(a, b, n);
            double difference = Math.Abs(trapezoidalResult - midpointResult);

            // Assert
            Assert.IsTrue(difference < 0.001,
                $"Методы дают слишком разные результаты: трапеции={trapezoidalResult}, прямоугольники={midpointResult}");
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

        // Тест 6: Проверка на постоянной функции
        [TestMethod]
        public void BothMethods_ConstantFunction_ReturnExactArea()
        {
            // Arrange
            // ∫(5)dx от 0 до 3 = 5 * 3 = 15
            var constantCalculator = new TestConstantCalculator();
            double a = 0, b = 3;
            int n = 100;
            double expected = 15.0;

            // Act
            double trapezoidalResult = constantCalculator.CalculateTrapezoidal(a, b, n);
            double midpointResult = constantCalculator.CalculateMidpoint(a, b, n);

            // Assert
            Assert.AreEqual(expected, trapezoidalResult, 1e-10, "Метод трапеций для константы неверен");
            Assert.AreEqual(expected, midpointResult, 1e-10, "Метод прямоугольников для константы неверен");
        }
    }

    // Вспомогательный класс для тестирования линейной функции f(x) = 2x
    public class TestLinearCalculator : IIntegralCalculator
    {
        public double Function(double x)
        {
            return 2 * x; // f(x) = 2x
        }

        public double CalculateTrapezoidal(double a, double b, int n)
        {
            var calculator = new NumericalIntegralCalculator();
            return calculator.CalculateTrapezoidal(a, b, n);
        }

        public double CalculateMidpoint(double a, double b, int n)
        {
            var calculator = new NumericalIntegralCalculator();
            return calculator.CalculateMidpoint(a, b, n);
        }
    }

    // Вспомогательный класс для тестирования постоянной функции f(x) = 5
    public class TestConstantCalculator : IIntegralCalculator
    {
        public double Function(double x)
        {
            return 5; // f(x) = 5
        }

        public double CalculateTrapezoidal(double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0.5 * (Function(a) + Function(b));

            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += Function(x);
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