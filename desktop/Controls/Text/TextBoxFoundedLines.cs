using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace desktop.Controls.Text
{
    public class TextBoxFoundedLines
    {
        TextBox txt = null;


        public TextBoxFoundedLines(TextBox txt)
        {
            this.txt = txt;
        }

        public int actualLine
        {
            set
            {
                txt.ScrollToLine(value);
            }
        }

        public void MoveToNextOccurence()
        {
            
        }
    }
}
