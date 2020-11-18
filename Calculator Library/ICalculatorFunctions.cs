using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Library
{
    public interface ICalculatorFunctions
    {
        double Addition(double x, double y);
        double Subtraction(double x, double y);
        double Multiplication(double x, double y);
        double Division(double numerator, double denominator);
    }
}
