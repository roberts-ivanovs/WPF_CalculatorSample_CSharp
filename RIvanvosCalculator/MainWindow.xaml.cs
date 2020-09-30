using System;
using System.Windows;
using System.Windows.Controls;
namespace RIvanvosCalculator
{

    public partial class MainWindow : Window
    {

        CalculatorLogic calculatorLogic;
        DisplayLogic display;
        public MainWindow()
        {
            InitializeComponent();
            this.calculatorLogic = new CalculatorLogic();
            this.display = new DisplayLogic(textDisplayPrevious, textDisplayExpression, textDisplayResult);
        }

        private void BtnGeneric_Click(object sender, RoutedEventArgs e)
        {
            string buttonValue = (((Button)sender).Tag.ToString());
            Console.WriteLine("Clicked tag: " + buttonValue);

            EDisplayState displayAction;
            string displayable = "";
            switch (buttonValue)
            {
                case "clearCurrent":
                    displayable = this.calculatorLogic.clearCurrent();
                    displayAction = EDisplayState.CLEARCURRENT;
                    break;
                case "clearAll":
                    displayable = this.calculatorLogic.clearAll();
                    displayAction = EDisplayState.CLEARALL;
                    break;
                case "result":
                    displayable = this.calculatorLogic.result();
                    displayAction = EDisplayState.APPEND;
                    break;
                case "swapSigns":
                    displayable = this.calculatorLogic.swapSigns();
                    displayAction = EDisplayState.REPLACE;
                    break;
                default:
                    // The input must be a parsable number or a decimal separator
                    displayable = this.calculatorLogic.appendExpression(buttonValue);
                    displayAction = EDisplayState.REPLACE;
                    break;
            }

            switch (displayAction)
            {
                case EDisplayState.CLEARCURRENT:
                    this.display.appendText(displayable);
                    break;
                case EDisplayState.CLEARALL:
                    this.display.allEmpty();
                    break;
                case EDisplayState.APPEND:
                    this.display.appendText(displayable);
                    break;
                case EDisplayState.REPLACE:
                    this.display.replaceCurrent(displayable);
                    break;
            }
        }
    }
}