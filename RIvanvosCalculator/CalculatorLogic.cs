using System;
using System.Text.RegularExpressions;

namespace RIvanvosCalculator
{
    class CalculatorLogic
    {

        private static readonly Regex FACTORTIAL_TOKEN = new Regex(@"(\d+!|(\({1}(\d|\+|\-|!)+\)!))");
        private string ExpressionCurrent { get; set; } = "";



        /* -------------- PUBLIC METHODS --------------- */
        public CalculatorLogic()
        {
            this.ExpressionCurrent = "";
        }


        public string ClearCurrent()
        {
            this.ExpressionCurrent = "";
            return this.ExpressionCurrent;
        }
        public string ClearAll()
        {
            this.ClearCurrent();
            return this.ExpressionCurrent;
        }
        public string Result()
        {
            string result = "";
            try
            {
                // Explicitly handle factorials
                string parsed_result = EvaluateUnknownFactorials(this.ExpressionCurrent);

                result = CalculatorLogic.Evaluate(parsed_result).ToString();
                this.ExpressionCurrent = result;
            }
            catch (Exception)
            {
                this.ClearCurrent();
                result = "ERROR";
            }


            // By returning the result and cleaning the inner memory is how we 
            // achieve the lazy displaying of the result while still "clearing" 
            // the screen once new numbers get pressed
            //this.ClearCurrent();
            return result;
        }
        public string SwapSigns()
        {
            this.ExpressionCurrent = "-(" + this.ExpressionCurrent + ")";
            return this.ExpressionCurrent;
        }
        public string AppendExpression(String appendable)
        {
            this.ExpressionCurrent += appendable;
            return this.ExpressionCurrent;
        }

        public string Backspace()
        {
            if (this.ExpressionCurrent.Length > 0)
            {
                this.ExpressionCurrent = this.ExpressionCurrent.Substring(0, this.ExpressionCurrent.Length - 1);
            }
            return this.ExpressionCurrent;
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

        /*
         * Replace a substring within the `longString` located at `index` with the length 
         * of `originalSubstring` and a new value of `insertable`.
         */
        private static string DirtyReplace(string longString, string insertable, string originalSubstring, int index)
        {
            string substringLeft = longString.Substring(0, index);
            string substringRight = longString.Substring(index + originalSubstring.Length);
            return substringLeft + insertable + substringRight;
        }


    }
}
