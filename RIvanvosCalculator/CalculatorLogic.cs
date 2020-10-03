using System;
using System.Text.RegularExpressions;

namespace RIvanvosCalculator
{
    class CalculatorLogic
    {

        private static readonly Regex FACTORTIAL_TOKEN = new Regex(@"(\d+!|(\({1}(\d|\+|\-|!)+\)!))");
        private string expressionCurrent { get; set; } = "";



        /* -------------- PUBLIC METHODS --------------- */
        public CalculatorLogic()
        {
            this.expressionCurrent = "";
        }


        public string clearCurrent()
        {
            this.expressionCurrent = "";
            return this.expressionCurrent;
        }
        public string clearAll()
        {
            this.clearCurrent();
            return this.expressionCurrent;
        }
        public string result()
        {
            string result = "";
            //try
            //{
                // Explicitly handle factorials
                string parsed_result = EvaluateUnknownFactorials(this.expressionCurrent);

                result = CalculatorLogic.Evaluate(parsed_result).ToString();
           // }
           // catch (Exception)
           // {
           //     result = "ERROR";
           // }

            // By returning the result and cleaning the inner memory is how we 
            // achieve the lazy displaying of the result while still "clearing" 
            // the screen once new numbers get pressed
            this.clearCurrent();
            return result;
        }
        public string swapSigns()
        {
            this.expressionCurrent = "-(" + this.expressionCurrent + ")";
            return this.expressionCurrent;
        }
        public string appendExpression(String appendable)
        {
            this.expressionCurrent += appendable;
            return this.expressionCurrent;
        }


        /* -------------- PRIVATE METHODS --------------- */


        /* Copied over from https://stackoverflow.com/a/6052679
         * 
         * It would be nice to hear in the next upcoming lectures on what is actually happening here.
         */
        private static float Evaluate(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return float.Parse((string)row["expression"]);
        }

        /*
         * Find every factorial in a substring and evaluate it. Works recursevly.
         * Once the factorial has been evaluated -> replace the expression in 
         * the input string with the calculated value, return the altered string.
         */
        private static string EvaluateUnknownFactorials(string expression)
        {
            Match match = FACTORTIAL_TOKEN.Match(expression);
            CaptureCollection captures = match.Captures;
            foreach (Capture capture in captures)
            {
                // remove the '!'
                string group_substring = EvaluateUnknownFactorials(capture.Value.Substring(0, capture.Value.Length - 1));
                long localResult = (long)CalculatorLogic.Evaluate(group_substring);
                long localResultFact = CalculatorLogic.Factorial(localResult);
                expression = DirtyReplace(expression, localResultFact.ToString(), capture.Value, capture.Index);
            }

            return expression.Contains("!") ? EvaluateUnknownFactorials(expression) : expression;
        }

        private static long Factorial(long i)
        {
            return i <= 1 ? i : i * Factorial(i - 1);
        }

        private static string DirtyReplace(string longString, string insertable, string originalSubstring, int index)
        {
            string substringLeft = longString.Substring(0, index);
            string substringRight = longString.Substring(index + originalSubstring.Length);
            return substringLeft + insertable + substringRight;
        }


    }
}
