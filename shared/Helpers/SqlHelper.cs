using System.Data;
using System.Diagnostics;
using System;
using System.Text;
public abstract class SqlHelper
{


    /// <summary>
    /// 
    /// </summary>
    
    public string ListingWholeTable(string tableName, DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("**" + "*" + "Start of listing whole table" + " " + tableName + "***");
        if (dt.Rows.Count != 0)
        {
            sb.AppendLine(SF.PrepareToSerialization(CA.ToListString( GetColumnsOfTable(tableName))));
            foreach (DataRow item in dt.Rows)
            {
                sb.AppendLine(SF.PrepareToSerialization(CA.ToListString( item.ItemArray)));
            }
        }
        sb.AppendLine("**" + "*" + "End of listing whole table" + " " + tableName + "***");
        return sb.ToString();
    }

    protected abstract object[] GetColumnsOfTable(string p);
}
