using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// To not inicialize 
/// </summary>
public class FSThread
{
    public static string GetFileName(string n)
    {
        return Path.GetFileName(n);
    }
}