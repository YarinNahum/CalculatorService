using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Library
{
    public class CalculatorFunctionsImpl : ICalculatorFunctions
    {
        public double Addition(double x, double y)
        {
            return x + y;
        }

        public double Division(double numenator, double denominator)
        {
            return numenator / denominator;
        }

        public double Multiplication(double x, double y)
        {
            return x * y;
        }

        public double Subtraction(double x, double y)
        {
            return x - y;
        }
    }
}
