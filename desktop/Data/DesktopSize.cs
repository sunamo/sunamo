using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class DesktopSize : SunamoSize
{
    

    public DesktopSize()
    {

    }

    public DesktopSize(SizeChangedEventArgs e)
    {
        Width = e.NewSize.Width;
        Height = e.NewSize.Height;
    }

    public DesktopSize(double actualWidth, double actualHeight)
    {
        Width = actualWidth;
        Height = actualHeight;
    }

    
}