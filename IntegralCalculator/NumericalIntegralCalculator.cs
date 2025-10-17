using System;

namespace IntegralCalculator
{
    public class NumericalIntegralCalculator : IIntegralCalculator
    {
        public double Function(double x)
        {
            if (x < 0)
                throw new ArgumentException("Аргумент x должен быть положительным числом для вычисления логарифма");

            return 2 * x - (Math.Log(7) + Math.Log(x)) - 12;
        }

        public double CalculateTrapezoidal(double a, double b, int n)
        {
            ValidateParameters(a, b, n);

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
            ValidateParameters(a, b, n);

            double h = (b - a) / n;
            double sum = 0;

            for (int i = 0; i < n; i++)
            {
                double x_mid = a + (i + 0.5) * h;
                sum += Function(x_mid);
            }

            return sum * h;
        }

        private void ValidateParameters(double a, double b, int n)
        {
            if (a >= b)
                throw new ArgumentException("Нижний предел интегрирования должен быть меньше верхнего предела");

            if (n <= 0)
                throw new ArgumentException("Количество разбиений должно быть положительным числом");

            if (a < 0)
                throw new ArgumentException("Нижний предел интегрирования должен быть положительным для данной функции");
        }
    }
}