using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DumpAsStringArgs : DumpAsStringHeaderArgs
{
    public string name = string.Empty; 
    public object o; 
    public DumpProvider d = DumpProvider.Yaml;
    /// <summary>
    /// Good for fast comparing objects
    /// </summary>
    public bool onlyValues;
    public string deli = AllStrings.swd;
}
