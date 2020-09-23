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
        float operand1 = 0;
        float operand2 = 0;
        EOperations operation = EOperations.None;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Copied over from https://stackoverflow.com/a/6052679
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
                    float result = Evaluate(toDisplay);
                    toDisplay += "=";
                    toDisplay += result;
                }
            }
            textDisplay.Text = toDisplay;
        }

        private void genericNumberClick(float number)
        {
            if (operation == EOperations.None)
            {
                operand1 = (operand1 * 10) + number;
                textDisplay.Text = operand1.ToString();
            }
            else
            {
                operand2 = (operand2 * 10) + number;
            }
            this.updateDisplay();
        }

        private void genericOperationClick(EOperations operation)
        {
            this.operation = operation;
            this.updateDisplay();
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnDot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(0f);
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(7f);
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(8f);
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(9f);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(4f);
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(5f);
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(6f);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(1f);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(2f);
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            this.genericNumberClick(3f);
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            this.genericOperationClick(EOperations.Plus);
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            this.genericOperationClick(EOperations.Minus);
        }

        private void btnMult_Click(object sender, RoutedEventArgs e)
        {
            this.genericOperationClick(EOperations.Multiply);
        }

        private void btnDiv_Click(object sender, RoutedEventArgs e)
        {
            this.genericOperationClick(EOperations.Div);
        }

        private void btnEq_Click(object sender, RoutedEventArgs e)
        {
            this.displayResult = true;
            this.updateDisplay();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEr_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChangeSign_Click(object sender, RoutedEventArgs e)
        {

        }

        private void display_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
