using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SqlDebug
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string CountOfRows(List<string> dt)
    {
        Dictionary<string, int> l = new Dictionary<string, int>();
        foreach (var item in dt)
        {
            if (MSStoredProceduresI.ci.SelectExistsTable(item))
            {
                l.Add(item, (int)MSStoredProceduresI.ci.SelectCount(item));
            }
        }

        TextOutputGenerator tog = new TextOutputGenerator();
        tog.CountEvery<string>(l.ToList());
        var r = tog.ToString();
        return r;
    }
}