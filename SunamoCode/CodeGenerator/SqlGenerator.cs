using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SqlGenerator
{
    StringBuilder sb = new StringBuilder();

    public void Select(string table)
    {
        sb.AppendLine("select * from " + table);
    }

    public override string ToString()
    {
        return sb.ToString();
    }
}