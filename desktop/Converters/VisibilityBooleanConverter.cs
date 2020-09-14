using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class VisibilityBooleanConverter
{
    public static bool ToBool(Visibility v)
    {
        if (v == Visibility.Visible)
        {
            return true;
        }
        return false;
    }

    public static Visibility FromBool(bool b)
    {
        if (b)
        {
            return Visibility.Visible;
        }
        return Visibility.Collapsed;
    }
}