using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public partial class ComboBoxHelper
{
    static Type type = typeof(ComboBoxHelper);

    /// <summary>
    /// A1 is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="list12"></param>
    public static ComboBox Get(ControlInitData d)
    {
        ComboBox cb = new ComboBox();
        ControlHelper.SetForeground(cb, d.foreground);
        foreach (var item in d.list)
        {
            cb.Items.Add(item);
        }
        if (d.OnClick != null)
        {
            ThrowExceptions.IsNotAllowed(type, RH.CallingMethod(), "d.OnClick");
        }
        cb.Tag = d.tag;
        cb.ToolTip = d.tooltip;
        cb.IsEditable = d.isEditable;

        return cb;
    }

    /// <summary>
    /// Instead of this use instance 
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="control"></param>
    /// <param name="trim"></param>
    public static void Validate(object tb, ComboBox control, ValidateData d = null)
    {
        control.Validate(tb, d);
    }

    public static bool validated
    {
        set
        {
            ComboBoxExtensions.validated = value;
        }
        get
        {
            return ComboBoxExtensions.validated;
        }
    }
}