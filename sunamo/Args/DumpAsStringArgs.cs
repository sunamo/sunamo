using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DumpAsStringArgs
{
    public string name = string.Empty; 
    public object o; 
    public DumpProvider d = DumpProvider.Yaml;
    public List<string> onlyNames = new List<string>();
    /// <summary>
    /// Good for fast comparing objects
    /// </summary>
    public bool onlyValues;
    public string deli = AllStrings.swd;
}
