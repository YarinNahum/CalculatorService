using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Library
{
    /// <summary>
    /// The interface for the calculator functions
    /// </summary>
    public interface ICalculatorFunctions
    {
        double Addition(double x, double y);
        double Subtraction(double x, double y);
        double Multiplication(double x, double y);
        double Division(double numerator, double denominator);
    }
}
