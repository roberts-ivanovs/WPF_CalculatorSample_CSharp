using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RIvanvosCalculator
{
    class DisplayLogic
    {
        private TextBlock oldest;   // The furthest display layer
        private TextBlock older;    // The middle display layer
        private TextBlock current;  // The "closest" display layer

        // If true, then `middle` layer display will be combined with 
        // the `current` layer on the next `replaceCurrent` call.
        private bool combine = false;

        /**
         * Register the TextBlock instances that will be altered as 
         * the `DisplayLogic` methods get called.
         */
        public DisplayLogic(TextBlock oldest, TextBlock older, TextBlock current)
        {
            this.oldest = oldest;
            this.older = older;
            this.current = current;
            this.AllEmpty();
        }

        /**
         * Clear all text boxes
         */
        public void AllEmpty()
        {
            this.oldest.Text = "";
            this.older.Text = "";
            this.current.Text = "";
        }

        /**
         * Append the string inptut to the `current` TextBlock and push the history 
         * to `older` and `oldest`
         */
        public void AppendText(string appendable)
        {
            this.oldest.Text = this.older.Text;
            this.older.Text = this.current.Text + "=";
            this.current.Text = appendable;

            // Append the `current` value to the `older` on next `replaceCurrent()` call
            this.combine = true;
        }

        /**
         * Replace the `TextBlock current` text with the function input string
         */
        public void ReplaceCurrent(string repalcement)
        {
            // Check if we need to save the last result
            if (this.combine)
            {
                this.older.Text += this.current.Text;
                this.combine = false;
            }
            this.current.Text = repalcement;
        }

    }
}
