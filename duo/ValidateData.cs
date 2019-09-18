using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Must be in shared because desktop reference PathEditor and therefore this class cant be in desktop 
/// </summary>
public class ValidateData
{
    public bool trim = true;
    public List<string> excludedStrings = new List<string>();
}

