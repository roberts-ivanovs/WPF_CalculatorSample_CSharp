﻿using System;
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

            // Bind the display widgets to the appropriate handler
            this.display = new DisplayLogic(textDisplayPrevious, textDisplayExpression, textDisplayResult);
        }

        /**
         * Perform actions based on the associated Tag on the button
         * 
         * Supported tag actions:
         *  - clearCurrent
         *  - clearAll
         *  - result            // perform string evaluation into a number
         *  - swapSigns
         *  - CONV:EUR:USD
         *  - CONV:USD:EUR
         *  - backspace
         *  
         *  All other tags will be interpreted as raw input for calculation parsing
         */
        private void BtnGeneric_Click(object sender, RoutedEventArgs e)
        {
            string buttonValue = (((Button)sender).Tag.ToString());
            EDisplayState displayAction;  // How the display should be handled
            string displayable = "";  // Represent the string to display on the next "render" for the calculator

            switch (buttonValue)
            {
                case "clearCurrent":
                    displayable = this.calculatorLogic.ClearCurrent();
                    displayAction = EDisplayState.REPLACE;
                    break;
                case "clearAll":
                    displayable = this.calculatorLogic.ClearAll();
                    displayAction = EDisplayState.CLEARALL;
                    break;
                case "result":
                    displayable = this.calculatorLogic.Result();
                    displayAction = EDisplayState.APPEND;
                    break;
                case "swapSigns":
                    displayable = this.calculatorLogic.SwapSigns();
                    displayAction = EDisplayState.REPLACE;
                    break;
                case "CONV:EUR:USD":
                    this.calculatorLogic.AppendExpression("*1.17");
                    displayable = this.calculatorLogic.Result();
                    displayAction = EDisplayState.APPEND;
                    break;
                case "CONV:USD:EUR":
                    this.calculatorLogic.AppendExpression("*0.85");
                    displayable = this.calculatorLogic.Result();
                    displayAction = EDisplayState.APPEND;
                    break;
                case "backspace":
                    displayable = this.calculatorLogic.Backspace();
                    displayAction = EDisplayState.REPLACE;
                    break;
                default:
                    // If something invalid is entered, then it will be handled by the calculator logic.
                    displayable = this.calculatorLogic.AppendExpression(buttonValue);
                    displayAction = EDisplayState.REPLACE;
                    break;
            }

            // Handle display
            switch (displayAction)
            {
                case EDisplayState.CLEARALL:
                    this.display.AllEmpty();
                    break;
                case EDisplayState.APPEND:
                    this.display.AppendText(displayable);
                    break;
                case EDisplayState.REPLACE:
                    this.display.ReplaceCurrent(displayable);
                    break;
            }

            CalculatorLogger.writeToLog(displayAction, buttonValue, displayable);
        }
    }
}