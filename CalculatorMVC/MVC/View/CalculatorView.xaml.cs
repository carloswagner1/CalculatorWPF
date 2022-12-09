using CalculatorMVC.MVC.Controller;
using CalculatorMVC.MVC.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace CalculatorMVC.MVC.View
{
    /// <summary>
    /// Interaction logic for CalculatorView.xaml
    /// </summary>
    public partial class CalculatorView : UserControl
    {
        public static string current_calculation = "0";
        public static string lastItem = "";
        public bool isDecimal = false;
        public bool hasOperator = false;
        public bool isResult = false;
        public int operationPosition = 0;
        public string operation = String.Empty;
        public double value1 = 0;
        public double value2 = 0;

        public CalculatorView()
        {
            InitializeComponent();
            DataContext = this;
            Calculation_Display();
        }
        
        private void NumPad_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (isResult)
            {
                current_calculation = "0";
                Calculation_Display();
                isResult= false;
            }              

            if (button.Content.ToString() == ",")
            {
                if (isDecimal)
                    return;
                else if (PrimaryDisplay.Text == "0")
                    current_calculation += "0";
                else if (operation != "" && current_calculation.EndsWith(operation))
                    current_calculation += "0";
                isDecimal= true;
            }                
            current_calculation += button.Content.ToString();
            Calculation_Display();
        }
        private void OperationPad_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (hasOperator)
            {
                var num2 = getLastItem(current_calculation);
                if (num2 == "")
                    return;
                value2 = Double.Parse(num2);
                var calc = new CalculatorController(value1, value2, operation);
                double result = calc.Calculate();
                updateDisplay(result);
                hasOperator = false;
                isResult= false;
                operationPosition = 0;
                operation = String.Empty;

            }
            if (current_calculation.Length > 0 && hasOperator == false)
            {
                value1 = Double.Parse(current_calculation);
                operation = button.Content.ToString();
                current_calculation += button.Content.ToString();
                operationPosition = current_calculation.Length;
                Calculation_Display();
                isDecimal = false;
                hasOperator = true;
            }
        }
        private void FuncPad_Click(object sender, RoutedEventArgs e)
        {
            if (sender == SquareRoot)
            {
                var value = Double.Parse(PrimaryDisplay.Text);
                var result = Math.Sqrt(value);
                SecondaryDisplay.Text = "Raiz de " + PrimaryDisplay.Text + " =";
                current_calculation = result.ToString();
                Calculation_Display();
            }
            else if (sender == Percent)
            {
                var num2 = getLastItem(current_calculation);                
                if (num2 == "")
                    return;
                value2 = Double.Parse(num2);
                var calc = new CalculatorController(value1, value2, operation);
                num2 = calc.Percent().ToString();                
                current_calculation = PrimaryDisplay.Text.Substring(0, operationPosition);                
                current_calculation += num2;
                Calculation_Display();
            }
            else if (sender == ClearButton)
            {
                current_calculation = "0";
                Calculation_Display();
                SecondaryDisplay.Text = string.Empty;
                isDecimal = false;
                hasOperator = false;
            }
            else if (sender == BackSpace)
                Backspace();
        }
        
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if(hasOperator && operationPosition < current_calculation.Length)
            {
                var num2 = getLastItem(current_calculation);
                if (num2 == "")
                    return;
                value2 = Double.Parse(num2);
                var calc = new CalculatorController(value1, value2, operation);
                double result = calc.Calculate();
                updateDisplay(result);
                hasOperator = false;
                operationPosition = 0;
                operation = String.Empty;
                isResult = true;
            }
        }

        //Display Actions        
        public async void Calculation_Display()
        {
            PrimaryDisplay.Text = current_calculation;
            if (current_calculation == "0")
                current_calculation = "";

            switch (PrimaryDisplay.Text.Length)
            {
                case 10:
                    for (int Timer = 0; Timer <= 20; Timer++)
                    {
                        double New_Size = PrimaryDisplay.FontSize - 0.5;
                        PrimaryDisplay.FontSize = New_Size;
                        await Task.Delay(1);
                    }
                    break;

                case 12:
                    for (int Timer = 0; Timer <= 10; Timer++)
                    {
                        double New_Size = PrimaryDisplay.FontSize - 0.5;
                        PrimaryDisplay.FontSize = New_Size;
                        await Task.Delay(1);
                    }
                    break;

                case 14:
                    for (int Timer = 0; Timer <= 20; Timer++)
                    {
                        double New_Size = PrimaryDisplay.FontSize - 0.5;
                        PrimaryDisplay.FontSize = New_Size;
                        await Task.Delay(1);
                    }
                    break;
            }
        }
        private void updateDisplay(double result)
        {
            SecondaryDisplay.Text = current_calculation + " = ";
            current_calculation = result.ToString();
            PrimaryDisplay.Text = current_calculation;
        }
        //Auxiliar methods

        private async void Backspace()
        {
            if (current_calculation.Length == 1)
            {
                current_calculation = "0";
                Calculation_Display();
            }
            if (current_calculation == "0,")
            {
                current_calculation = "0";
                Calculation_Display();
                isDecimal = false;
            }
            if (current_calculation.Length > 1)
            {
                current_calculation = current_calculation.Remove(current_calculation.Length - 1);
                PrimaryDisplay.Text = current_calculation;

                switch (PrimaryDisplay.Text.Length)
                {
                    case 9:
                        for (int Timer = 0; Timer <= 20; Timer++)
                        {
                            double New_Size = PrimaryDisplay.FontSize + 0.5;
                            PrimaryDisplay.FontSize = New_Size;
                            await Task.Delay(1);
                        }
                        break;

                    case 11:
                        for (int Timer = 0; Timer <= 10; Timer++)
                        {
                            double New_Size = PrimaryDisplay.FontSize + 0.5;
                            PrimaryDisplay.FontSize = New_Size;
                            await Task.Delay(1);
                        }
                        break;

                    case 13:
                        for (int Timer = 0; Timer <= 20; Timer++)
                        {
                            double New_Size = PrimaryDisplay.FontSize + 0.5;
                            PrimaryDisplay.FontSize = New_Size;
                            await Task.Delay(1);
                        }
                        break;
                }
            }
        }
        private string getLastItem(string operation)
        {
            var lastItem = "";
            for (var i = operationPosition; i < operation.Length; i++)
            {
                lastItem += operation[i];
            }
            return lastItem;
        }

    }
}
