using System;
using System.Windows.Controls;

namespace RIvanvosCalculator
{
    class CalculatorLogic
    {
        private string expressionCurrent { get; set; } = "";
        public CalculatorLogic()
        {
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
                result = CalculatorLogic.Evaluate(this.expressionCurrent).ToString();
            }
            catch (Exception)
            {
                result = "ERROR";
            }

            // By returning the result and cleaning the inner memory is how we 
            // achieve the lazy displaying of the result.
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

    }
}
