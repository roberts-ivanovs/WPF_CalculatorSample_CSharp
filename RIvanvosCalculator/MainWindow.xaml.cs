using System;
using System.Windows;
using System.Windows.Controls;
namespace RIvanvosCalculator
{
    public partial class MainWindow : Window
    {

        Parser calculatorLogic;
        public MainWindow()
        {
            this.calculatorLogic = new Parser();
            //InitializeComponent();
        }

        private void BtnGeneric_Click(object sender, RoutedEventArgs e)
        {
            string buttonValue = (((Button)sender).Tag.ToString());
            Console.WriteLine("Clicked tag: " + buttonValue);
            switch (buttonValue)
            {
                case "clearCurrent":
                    this.calculatorLogic.clearCurrent();
                    break;
                case "clearAll":
                    this.calculatorLogic.clearCurrent();
                    break;
                case "result":
                    this.calculatorLogic.clearCurrent();
                    break;
                case "swapSigns":
                    this.calculatorLogic.clearCurrent();
                    break;
                default:
                    // make sure the buttonValue is in [0123456789./+-*%]
                    this.calculatorLogic.appendExpression();
                    break;
            }

        }
    }
}