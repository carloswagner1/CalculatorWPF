using CalculatorMVC.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMVC.MVC.Controller
{
    public class CalculatorController
    {
        
        double result = 0;
        double value1 = 0;
        double value2 = 0;
        string operation = String.Empty;
        

        public CalculatorController(double value1, double value2, string operation)
        {
            this.value1 = value1;
            this.value2 = value2;   
            this.operation = operation;
        }
        
        public double Calculate()
        {             
            var calculator = new Calculator(value1, value2);

            switch (operation)
            {
                case "+":
                    result = calculator.add();
                    break;
                case "-":
                    result = calculator.subtract();
                    break;
                case "*":
                    result = calculator.multiply();
                    break;
                case "/":
                    result = calculator.divide();
                    break;

            }
            return result;
        }
        public double Percent()
        {
            var calculator = new Calculator(value1, value2);
            if (operation == "+" || operation == "-")
                result = value1 * calculator.percent();
            else
                result = calculator.percent();
            return result;           
        }
    }
}