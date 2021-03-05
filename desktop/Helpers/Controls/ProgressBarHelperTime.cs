using desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

public class ProgressBarHelperTime
{
    ProgressBarHelper pbh = null;
    public double ai = 0;
    double allSecondsMinusOne = 0;
    Timer t2 = null;

    public ProgressBarHelperTime(ProgressBar pb, double allSeconds, UIElement ui)
    {
        pbh = new ProgressBarHelper(pb, allSeconds, ui);
        allSecondsMinusOne = allSeconds - 1;

        //var t = new System.Threading.Timer();
        t2 = new Timer();
        t2.AutoReset = true;
        t2.Interval = 1000;
        t2.Elapsed += T2_Elapsed;
        t2.Start();

        
    }

    private void T2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        pbh.DonePartially();
        ai++;
        if (ai == allSecondsMinusOne)
        {
            t2.Stop();
        }
    }
}