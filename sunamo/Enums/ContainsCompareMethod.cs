using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum ContainsCompareMethod
{
    WholeInput,
    SplitToWords,
    /// <summary>
    /// split to words and check for ! at [0]
    /// </summary>
    Negations
}