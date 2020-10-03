using System;
using System.Text.RegularExpressions;

namespace RIvanvosCalculator
{
    class CalculatorLogic
    {

        private static readonly Regex FACTORTIAL_TOKEN = new Regex(@"(\d+!|\(.+\)!)");
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
            try
            {
                // Explicitly handle factorials
                string parsed_result = EvaluateUnknownFactorials(this.expressionCurrent);

                result = CalculatorLogic.Evaluate(parsed_result).ToString();
            }
            catch (Exception)
            {
                result = "ERROR";
            }

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
         * 
         */
        private static string EvaluateUnknownFactorials(string expression)
        {
            Match match = FACTORTIAL_TOKEN.Match(expression);
            CaptureCollection captures = match.Captures;
            foreach (Capture capture in captures)
            {
                // remove the '!'
                string group_substring = capture.Value.Substring(0, capture.Value.Length - 1);


                // Perform recursive factorial checking if necessary
                if (group_substring.StartsWith("(") && group_substring.EndsWith(")"))
                {
                    string subresult = EvaluateUnknownFactorials(group_substring);
                    int subvalue = (int)CalculatorLogic.Evaluate(subresult);
                    group_substring = group_substring.Replace(group_substring, subvalue.ToString());
                }

                int localResult = (int)CalculatorLogic.Evaluate(group_substring);
                localResult = CalculatorLogic.Factorial(localResult);
                expression = expression.Replace(capture.Value, localResult.ToString());
            }
            return expression;
        }

        private static int Factorial(int i)
        {
            return i <= 1 ? i : i * (i - 1);
        }


    }
}
