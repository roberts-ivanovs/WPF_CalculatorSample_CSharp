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
            switch (buttonValue)
            {
                case "clearCurrent":
                    this.calculatorLogic.clearCurrent();
                    displayAction = EDisplayState.CLEARCURRENT;
                    break;
                case "clearAll":
                    this.calculatorLogic.clearAll();
                    displayAction = EDisplayState.CLEARALL;
                    break;
                case "result":
                    this.calculatorLogic.result();
                    displayAction = EDisplayState.APPEND;
                    break;
                case "swapSigns":
                    this.calculatorLogic.swapSigns();
                    displayAction = EDisplayState.REPLACE;
                    break;
                default:
                    // TODO make sure the buttonValue is in [0123456789./+-*%]
                    this.calculatorLogic.appendExpression(buttonValue);
                    displayAction = EDisplayState.REPLACE;
                    break;
            }

            switch (displayAction)
            {
                case EDisplayState.CLEARCURRENT:
                    this.display.appendText("");
                    break;
                case EDisplayState.CLEARALL:
                    this.display.allEmpty();
                    break;
                case EDisplayState.APPEND:
                    this.display.appendText(this.calculatorLogic.expressionCurrent);
                    break;
                case EDisplayState.REPLACE:
                    this.display.replaceCurrent(this.calculatorLogic.expressionCurrent);
                    break;
            }
        }
    }
}