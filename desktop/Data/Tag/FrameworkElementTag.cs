using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FrameworkElementTag
{
    /// <summary>
    /// Tag which is set in CheckBoxListUC to avoid replace of other tag
    /// 
    /// </summary>
    public object tagCheckBoxListUC = null;
    /// <summary>
    /// Sub for default Tag of fw element
    /// </summary>
    public object Tag = null;

    public override string ToString()
    {
        return Tag.ToString();
    }
}