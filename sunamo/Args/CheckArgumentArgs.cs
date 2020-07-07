using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CheckArgumentArgs
{
    public StringBuilder sb = null;
    public string argName = null;
    public bool _trim = false;

    

    public CheckArgumentArgs(string argName, StringBuilder sb)
    {
        this.argName = argName;
        this.sb = sb;
    }
}