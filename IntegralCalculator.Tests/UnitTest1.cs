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

        
        [TestMethod]
        public void Function_WithValidInput_ReturnsCorrectValue()
        {
            
            double x = 1.0;
            double expected = 2 * 1 - Math.Log(7 * 1) - 12;

            
            double actual = _calculator.Function(x);

            
            Assert.AreEqual(expected, actual, 1e-10, "Функция вычислена неверно");
        }

        
        [TestMethod]
        public void CalculateTrapezoidal_ForOurFunction_ReturnsReasonableValue()
        {
            
            double a = 1, b = 10;
            int n = 1000;

            
            double result = _calculator.CalculateTrapezoidal(a, b, n);

            
            Assert.IsFalse(double.IsNaN(result), "Результат не должен быть NaN");
            Assert.IsFalse(double.IsInfinity(result), "Результат не должен быть бесконечностью");
            
            Assert.IsTrue(result < 0, "Результат должен быть отрицательным для данной функции");
        }

        
        [TestMethod]
        public void CalculateMidpoint_ForOurFunction_ReturnsReasonableValue()
        {
            
            double a = 1, b = 10;
            int n = 1000;

            
            double result = _calculator.CalculateMidpoint(a, b, n);

            
            Assert.IsFalse(double.IsNaN(result), "Результат не должен быть NaN");
            Assert.IsFalse(double.IsInfinity(result), "Результат не должен быть бесконечностью");
            Assert.IsTrue(result < 0, "Результат должен быть отрицательным для данной функции");
        }

        
        [TestMethod]
        public void BothMethods_WithManyPartitions_ReturnSimilarResults()
        {
            
            double a = 1, b = 10;
            int n = 10000;

            
            double trapezoidalResult = _calculator.CalculateTrapezoidal(a, b, n);
            double midpointResult = _calculator.CalculateMidpoint(a, b, n);
            double difference = Math.Abs(trapezoidalResult - midpointResult);

            
            Assert.IsTrue(difference < 1.0,
                $"Методы дают слишком разные результаты: разница = {difference}");
        }

        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTrapezoidal_WithInvalidParameters_ThrowsException()
        {
            
            double a = 10, b = 1; 
            int n = 100;

            
            _calculator.CalculateTrapezoidal(a, b, n);
        }

         
      
    }

    
    public class ConstantFunctionCalculator
    {
        public double Function(double x)
        {
            return 5; 
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

    
    public class LinearFunctionCalculator
    {
        public double Function(double x)
        {
            return 2 * x; 
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