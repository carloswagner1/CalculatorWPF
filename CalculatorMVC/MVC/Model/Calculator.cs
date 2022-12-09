using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMVC.MVC.Model
{
    class Calculator
    {
        private double value1 = 0;
        private double value2 = 0;
        public Calculator(double num1 = 0 , double num2 = 0)
        {
            value1 = num1;
            value2 = num2;
        }
        public double add()
        {
            return (value1 + value2);
        }

        public double subtract()
        {
            return (value1 - value2);
        }

        public double multiply()
        {
            return (value1 * value2);
        }

        public double divide()
        {
            return (value1 / value2);
        }
        public double percent()
        {
            return (value2 / 100);
        }
        public double squareRoot()
        {
            return Math.Sqrt(value1);
        }
    }
}
