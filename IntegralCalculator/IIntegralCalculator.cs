using System;

namespace IntegralCalculator
{
    public interface IIntegralCalculator
    {
        double Function(double x);
        double CalculateTrapezoidal(double a, double b, int n);
        double CalculateMidpoint(double a, double b, int n);
    }
}