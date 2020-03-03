using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class MSBaseRowTable
{
    protected object[] o = null;

    #region Novejší verze s predáváním pouze indexu
    protected string GetString(int p)
    {
        return MSTableRowParse.GetString(o, p);
    }

    /// <summary>
    /// Když bude DBNull, G -1
    /// </summary>
    /// <param name="dex"></param>
    
    protected Guid GetGuid(int p)
    {
        return MSTableRowParse.GetGuid(o, p);
    }
    #endregion
}