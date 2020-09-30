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
        private TextBlock oldest;
        private TextBlock older;
        private TextBlock current;

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
            this.oldest.Text = this.older.Text + "=" + this.current.Text;
            this.older.Text = "";
            this.current.Text = appendable;
        }

        public void replaceCurrent(string repalcement)
        {
            this.current.Text = repalcement;
        }
    }
}
