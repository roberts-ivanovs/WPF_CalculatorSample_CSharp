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

        public DisplayLogic(TextBlock oldest, TextBlock older, TextBlock current)
        {
            this.oldest = oldest;
            this.older = older;
            this.current = current;
            this.allEmpty();
        }

        public void allEmpty()
        {
            this.oldest.Text = "";
            this.older.Text = "";
            this.current.Text = "";
        }

        public void appendText(string appendable)
        {
            this.oldest.Text = this.older.Text;
            this.older.Text = this.current.Text + "=";
            this.current.Text = appendable;

            // Append the `current` value to the `older` on next `replaceCurrent()` call
            this.combine = true;
        }

        public void replaceCurrent(string repalcement)
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
