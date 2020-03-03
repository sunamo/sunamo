using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class ActionButtonHelper{ 
/// <summary>
    /// tag is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name = "tooltip"></param>
    /// <param name = "imagePath"></param>
    public static ActionButton<T> Get<T>(ControlInitData d)
    {
        ActionButton<T> vr = new ActionButton<T>(d.action, (T)d.tag);
        ControlHelper.SetForeground(vr, d.foreground);
        vr.Content = ContentControlHelper.GetContent(d);
        if (d.OnClick != null)
        {
            vr.Click += d.OnClick;
        }

        vr.Tag = d.tag;
        vr.ToolTip = d.tooltip;
        return vr;
    }
}