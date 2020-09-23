using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RIvanvosCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum EOperations
    {
        Plus, Minus, Div, Multiply, None
    }

    public partial class MainWindow : Window
    {
        bool displayResult = false;
        string operand1 = "";
        string operand2 = "";
        EOperations operation = EOperations.None;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void resetState()
        {
            this.operand1 = "";
            this.operand2 = "";
            this.operation = EOperations.None;
            this.displayResult = false;
        }

        // Copied over from https://stackoverflow.com/a/6052679
        // It would be nice to hear in the next upcoming lectures on how to properly do this in C#.
        public static float Evaluate(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return float.Parse((string)row["expression"]);
        }

        private string operationsToString(EOperations operation)
        {
            switch (operation)
            {
                case EOperations.Plus:
                    return "+";
                case EOperations.Minus:
                    return "-";
                case EOperations.Div:
                    return "/";
                case EOperations.Multiply:
                    return "*";
                default:
                    return "";
            }
        }

        private void updateDisplay()
        {
            string toDisplay = operand1.ToString();
            if (operation != EOperations.None)
            {
                toDisplay += this.operationsToString(operation);
                toDisplay += operand2.ToString();

                if (displayResult)
                {
                    float result = 0f;
                    try
                    {
                        result = Evaluate(toDisplay);
                    }
                    catch (Exception)
                    {
                        this.resetState();
                        this.updateDisplay();
                        return;
                    }

                    toDisplay += "=";
                    toDisplay += result;
                }
            }
            textDisplay.Text = toDisplay;
        }

        private void genericNumberClick<T>(T repr)
        {
            // reset the state if necessary
            if (displayResult)
            {
                this.resetState();
            }

            if (operation == EOperations.None)
            {
                operand1 += repr.ToString();
                textDisplay.Text = operand1.ToString();
            }
            else
            {
                operand2 += repr.ToString();
            }
            this.updateDisplay();
        }

        private void genericOperationClick(EOperations operation)
        {
            // reset the state if necessary
            if (displayResult)
            {
                this.resetState();
            }
            this.operation = operation;
            this.updateDisplay();
        }

        private void BtnDot_Click(object sender, RoutedEventArgs e)
        {
            genericNumberClick<string>(".");
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            float btnNumber = float.Parse(((Button)sender).Content.ToString());
            this.genericNumberClick(btnNumber);
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            this.genericOperationClick(EOperations.Plus);
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            this.genericOperationClick(EOperations.Minus);
        }

        private void BtnMult_Click(object sender, RoutedEventArgs e)
        {
            this.genericOperationClick(EOperations.Multiply);
        }

        private void BtnDiv_Click(object sender, RoutedEventArgs e)
        {
            this.genericOperationClick(EOperations.Div);
        }

        private void BtnEq_Click(object sender, RoutedEventArgs e)
        {
            this.displayResult = true;
            this.updateDisplay();
        }

        private void BtnClearCurrent_Click(object sender, RoutedEventArgs e)
        {
            if (this.displayResult)
            {
                this.resetState();
            }
            else if (this.operation != EOperations.None)
            {
                this.operand2 = "";
            }
            else
            {
                this.operand1 = "";
            }
            this.updateDisplay();
        }

        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            this.resetState();
            this.updateDisplay();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            // Don't perform anything if we're displaying a result
            if (this.displayResult) return;
            if (this.operation == EOperations.None && this.operand1.Length > 0)
            {
                this.operand1 = this.operand1.Substring(0, this.operand1.Length - 1);
            }
            else if (this.operation != EOperations.None && this.operand2.Length > 0)
            {
                this.operand2 = this.operand2.Substring(0, this.operand2.Length - 1);
            }
            this.updateDisplay();
        }

        private void BtnChangeSign_Click(object sender, RoutedEventArgs e)
        {
            // Don't perform anything if we're displaying a result
            if (this.displayResult) return;
            if (this.operation == EOperations.None)
            {
                //this.operand1 = Evaluate("-" + this.operand1).ToString();
                if (this.operand1 != "")
                {
                    this.operand1 = Evaluate("-" + this.operand1).ToString();
                }
            }
            else
            {
                if (this.operand2 != "")
                {
                    this.operand2 = Evaluate("-" + this.operand2).ToString();
                }
            }
            this.updateDisplay();
        }

    }
}
