using System;
using System.Web.UI;
public static class ControlHelper
{
    public static ControlCollection GetControlsByType(Control appsPage, Type type)
    {

        System.Collections.Generic.List<Control> cntrls = new System.Collections.Generic.List<Control>();


        foreach (Control child in appsPage.Controls)
        {
            if (type == child.GetType())
                cntrls.Add(child);
            ControlCollection fromChild = GetControlsByType(child, type);
            foreach (Control item in fromChild)
            {
                cntrls.Add(item);
            }
        }

        ControlCollection vr = new ControlCollection(appsPage);
        foreach (var item in cntrls)
        {
            vr.Add(item);
        }
        return vr;
    }

}
