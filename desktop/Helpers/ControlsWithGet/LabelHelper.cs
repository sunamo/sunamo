using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class LabelHelper
{
    static Type type = typeof(LabelHelper);

    public static Label Get(ControlInitData d)
    {
        Label vr = new Label();
        ControlHelper.SetForeground(vr, d.foreground);
        vr.Content = ContentControlHelper.GetContent(d);
        vr.Foreground = d.foreground;
        if (d.OnClick != null)
        {
            ThrowExceptions.IsNotNull(Exc.GetStackTrace(), type, Exc.CallingMethod(), "d.OnClick", d.OnClick);
            //vr.MouseDown += d.OnClick;
        }
        vr.Tag = d.tag;
        vr.ToolTip = d.tooltip;
        return vr;
    }
}