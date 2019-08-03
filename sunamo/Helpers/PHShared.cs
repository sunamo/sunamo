using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using sunamo;
using System.Linq;

public partial class PH
{
    public static void Start(string p)
    {
        try
        {
            Process.Start(p);
        }
        catch (Exception)
        {
        }
    }
}