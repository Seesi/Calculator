using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        string firstString = "";
        decimal firstNumber = 0;
        string secondString = "";
        decimal secondNumber = 0;
        decimal total = 0;
        string operation = "";

        public MainPage()
        {
            InitializeComponent();
        }


        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var currentText = button.Text;
            switch (currentText)
            {
                case "+":
                case "-":
                case "x":
                case "/":
                    SetCurrentOperation(currentText);
                    break;
                case "AC":
                    ClearOperation();
                    break;
                case "=":
                    CloseEvaluation();
                    break;
                default:
                    ExecuteOperation(currentText);
                    break;
            }
        }

        private void SetCurrentOperation(string currentText)
        {
            if (firstNumber is 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(operation))
            {
                CloseEvaluation();
                UpdateExpression();
                UpdateEvaluation();
                operation = currentText;
            }
            else
            {
                operation = currentText;
                UpdateExpression();
                UpdateEvaluation();
            }

        }

        private void ExecuteOperation(string currentText)
        {
            if (firstNumber is 0 && string.IsNullOrEmpty(operation))
            {
                if (decimal.TryParse(currentText, out decimal fNumber))
                {
                    firstString = currentText;
                    firstNumber = fNumber;
                }

            }
            else if (firstNumber is not 0 && string.IsNullOrEmpty(operation))
            {
                if (decimal.TryParse(currentText, out decimal fNumber))
                {
                    firstString += currentText;
                    firstNumber = decimal.Parse(firstString);
                }
            }
            else if (firstNumber is not 0 && !string.IsNullOrEmpty(operation))
            {
                if (decimal.TryParse(currentText, out decimal sNumber))
                {
                    secondString += currentText;
                    secondNumber = decimal.Parse(secondString);
                    switch (operation)
                    {
                        case "+":
                            total = Add(firstNumber, secondNumber);
                            break;
                        case "-":
                            total = Substract(firstNumber, secondNumber);
                            break;
                        case "x":
                            total = Multiply(firstNumber, secondNumber);
                            break;
                        case "/":
                            total = Divide(firstNumber, secondNumber);
                            break;
                    }
                }
            }


            UpdateExpression();
            UpdateEvaluation();

        }

        private void CloseEvaluation()
        {
            firstNumber = total;
            firstString = total.ToString();
            operation = "";
            secondString = "";
            secondNumber = 0;
            UpdateExpression();
            UpdateEvaluation();
        }

        private void ClearOperation()
        {
            firstNumber = 0;
            secondNumber = 0;
            total = 0;
            firstString = "";
            secondString = "";
            operation = "";
            UpdateExpression();
            UpdateEvaluation();
        }

        private void UpdateExpression()
        {
            Expression.Text = $"{firstString}{operation}{secondString}";
        }

        private void UpdateEvaluation()
        {
            Evaluation.Text = total == 0 ? "" : total.ToString();
        }

        private decimal Add(decimal first, decimal second) => first + second;

        private decimal Substract(decimal first, decimal second) => first - second;

        private decimal Divide(decimal first, decimal second) => Math.Round(first / second, 2);

        private decimal Multiply(decimal first, decimal second) => Math.Round(first * second, 2);

    }
}

