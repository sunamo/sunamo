using System;

public class BaseRowTable
{
    protected object[] o = null;

    protected string GetString(int p)
    {
        return TableRowParse.GetString(o, p);
    }

    /// <summary>
    /// Když bude DBNull, G -1
    /// </summary>
    /// <param name="dex"></param>
    
    protected Guid GetGuid(int p)
    {
        return TableRowParse.GetGuid(o, p);
    }
}