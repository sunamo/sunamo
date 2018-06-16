using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace desktop.Helpers.SpecialFunctionality
{
    public class TextBoxFoundedLines 
    {
        TextBox txtContent = null;
        public int actualLine = 0;
        public int linesOnDisplay = 0;

        public TextBoxFoundedLines(TextBox txtContent)
        {
            this.txtContent = txtContent;
        }

        public  void MoveToNextOccurence()
        {
            actualLine = actualLine + linesOnDisplay;
            if (actualLine > txtContent.LineCount)
            {
                actualLine = txtContent.LineCount - 1;
            }
            txtContent.ScrollToLine(actualLine);
        }
    }
}
