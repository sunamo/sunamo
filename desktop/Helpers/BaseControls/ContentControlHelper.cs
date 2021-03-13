using desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
public partial class ContentControlHelper
{
    public static T CastTo<T>(object o) where T : class
    {
        if (o is T)
        {
            return (T)o;
        }
        var cc = (ContentControl)o;
        return cc.Content as T;

    }

    
}