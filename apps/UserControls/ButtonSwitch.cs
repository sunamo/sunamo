using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace apps
{
    public class ButtonSwitch : Button
    {
        SolidColorBrush panel1 = new SolidColorBrush(Colors.LightBlue);
        SolidColorBrush panel2 = new SolidColorBrush(Colors.White);
        SolidColorBrush panel3 = new SolidColorBrush(Colors.White);
        SolidColorBrush colorD = new SolidColorBrush(Colors.White);
        SolidColorBrush colorE = new SolidColorBrush(Colors.LightBlue);


        public void ToLeft()
        {
            if (panel3 == colorE)
            {
                panel3 = colorD;
                panel2 = colorE;
            }
            else if (panel2 == colorE)
            {
                panel2 = colorD;
                panel1 = colorE;
            }
            else
            {
                panel1 = colorD;
                panel3 = colorE;
            }
        }

        public void ToRight()
        {
            if (panel3 == colorE)
            {
                panel3 = colorD;
                panel1 = colorE;
            }
            else if (panel2 == colorE)
            {
                panel2 = colorD;
                panel3 = colorE;
            }
            else
            {
                panel1 = colorD;
                panel2 = colorE;
            }
        }

        public SolidColorBrush Panel1
        {
            get
            {
                return panel1;
            }
        }

        public SolidColorBrush Panel2
        {
            get
            {
                return panel2;
            }
        }

        public SolidColorBrush Panel3
        {
            get
            {
                return panel3;
            }
        }
    }
}
