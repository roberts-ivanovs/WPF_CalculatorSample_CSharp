using System;
using System.Windows.Controls;

namespace RIvanvosCalculator
{
    class CalculatorLogic
    {
        public string expressionCurrent { get; set; } = "";
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

        public void clearCurrent()
        {
            this.expressionCurrent = "";
        }
        public void clearAll()
        {
            this.clearCurrent();
        }
        public void result()
        {
            this.expressionCurrent = CalculatorLogic.Evaluate(this.expressionCurrent).ToString();
        }
        public void swapSigns()
        {
            // todo figure this one out
        }
        public void appendExpression(String appendable)
        {
            this.expressionCurrent += appendable;
        }

    }
}
