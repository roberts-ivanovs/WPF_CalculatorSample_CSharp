using System;
using System.Text.RegularExpressions;

namespace RIvanvosCalculator
{
    /**
     * Perform string evaluation.
     */
    class CalculatorLogic
    {

        // Find 
        private static readonly Regex FACTORTIAL_TOKEN = new Regex(@"(\d+!|(\({1}(\d|\+|\-|!)+\)!))");
        private string ExpressionCurrent { get; set; } = "";



        /* -------------- PUBLIC METHODS --------------- */
        public CalculatorLogic()
        {
            this.ExpressionCurrent = "";
        }


        /**
         * Clear the current expression.
         * @returns current expression (cleared)
         */
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

        /**
         * Try calculating the current expression
         * @returns current expression (calculated) or "ERROR" string if something went wrong
         */
        public string Result()
        {
            string result = "";
            try
            {
                // Explicitly handle factorials because `Evaluate` method has no operand for them.
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

        /**
         * Swap the signs for the current expression
         * @returns current expresion (with swapped signs as an expression)
         */
        public string SwapSigns()
        {
            this.ExpressionCurrent = "-(" + this.ExpressionCurrent + ")";
            return this.ExpressionCurrent;
        }

        /**
         * Append a string value to the current expression
         * @returns current expression (with appended value)
         */
        public string AppendExpression(String appendable)
        {
            this.ExpressionCurrent += appendable;
            return this.ExpressionCurrent;
        }

        /**
         * Delete last character from the current expression if possible
         * @returns current expression (with the last char removed if possible)
         */
        public string Backspace()
        {
            if (this.ExpressionCurrent.Length > 0)
            {
                this.ExpressionCurrent = this.ExpressionCurrent.Substring(0, this.ExpressionCurrent.Length - 1);
            }
            return this.ExpressionCurrent;
        }

        /* -------------- PRIVATE METHODS --------------- */


        /**
         * Evalue the input expression into a numeric value
         * Copied over from https://stackoverflow.com/a/6052679
         * 
         * Note: Does not support factorial operands `!`
         * 
         * It would be nice to hear in the next upcoming lectures on what is actually happening here.
         * @returns evaluated result as a float
         */
        private static float Evaluate(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return float.Parse((string)row["expression"]);
        }

        /**
         * Find every factorial in a substring and evaluate it. Works recursevly.
         * Once the factorial has been evaluated -> replace the expression in 
         * the input string with the calculated value, return the altered string.
         * 
         * @returns Expression where all factorials have been evaluted
         */
        private static string EvaluateUnknownFactorials(string expression)
        {
            Match match = FACTORTIAL_TOKEN.Match(expression);
            CaptureCollection captures = match.Captures;
            foreach (Capture capture in captures)
            {
                // remove the '!' with the substring
                // Perform recursive check for other factorials
                string group_substring = EvaluateUnknownFactorials(capture.Value.Substring(0, capture.Value.Length - 1));
                long localResult = (long)CalculatorLogic.Evaluate(group_substring);
                long localResultFact = CalculatorLogic.Factorial(localResult);
                expression = DirtyReplace(expression, localResultFact.ToString(), capture.Value, capture.Index);
            }

            // Sanity check for making sure that no factorials are left. The need for this extra check can be 
            // eliminated if unittests are created to prove that such construction has no need
            return expression.Contains("!") ? EvaluateUnknownFactorials(expression) : expression;
        }

        /**
         * Calculate the factorial of the given number
         * @returns factorial of `i`
         */
        private static long Factorial(long i)
        {
            return i <= 1 ? i : i * Factorial(i - 1);
        }

        /**
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
