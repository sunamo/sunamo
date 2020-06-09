using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SubstringArgs
{
    /// <summary>
    /// Was before created this class
    /// </summary>
    public bool returnInputIfInputIsShorterThanA3 = false;
    public bool returnInputIfIndexFromIsLessThanIndexTo = false;

    public static SubstringArgs Instance = new SubstringArgs();
}