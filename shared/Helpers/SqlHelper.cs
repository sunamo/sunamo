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
        
        sb.AppendLine(Consts._3Asterisks + sess.i18n(XlfKeys.StartOfListingWholeTable) + " " + tableName + Consts._3Asterisks);
        if (dt.Rows.Count != 0)
        {
            sb.AppendLine(SF.PrepareToSerialization3(CA.ToListString( GetColumnsOfTable(tableName))));
            foreach (DataRow item in dt.Rows)
            {
                sb.AppendLine(SF.PrepareToSerialization3(CA.ToListString( item.ItemArray)));
            }
        }

        sb.AppendLine(Consts._3Asterisks + sess.i18n(XlfKeys.EndOfListingWholeTable) + " " + tableName + Consts._3Asterisks);
        return sb.ToString();
    }

    protected abstract object[] GetColumnsOfTable(string p);
}