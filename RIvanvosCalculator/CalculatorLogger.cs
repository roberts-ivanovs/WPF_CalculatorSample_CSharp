using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIvanvosCalculator
{
    class CalculatorLogger
    {
        public static void writeToLog(EDisplayState displayState, string clickedTag, string displayable )
        {
            // Timestamp method from: https://stackoverflow.com/a/61212918
            string unixTimestamp = Convert.ToString((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"calc.log", true))
            {
                file.WriteLine($"{unixTimestamp}: clicked on tag <{clickedTag}>, {displayState.ToString()} the the display with {displayable}");
            }
        }
    }
}
