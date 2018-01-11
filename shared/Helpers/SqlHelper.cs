using System.Data;
using System.Diagnostics;
using System;
using System.Text;
public abstract class SqlHelper
{


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string ListingWholeTable(string tableName, DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("***Start of listing whole table " + tableName + "***");
        if (dt.Rows.Count != 0)
        {
            sb.AppendLine(SF.PrepareToSerialization(GetColumnsOfTable(tableName)));
            foreach (DataRow item in dt.Rows)
            {
                sb.AppendLine(SF.PrepareToSerialization(item.ItemArray));
            }
        }
        sb.AppendLine("***End of listing whole table " + tableName + "***");
        return sb.ToString();
    }

    protected abstract object[] GetColumnsOfTable(string p);
}
