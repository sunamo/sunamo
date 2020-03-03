using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class ListParser
{
    protected List<string> o = null;

    #region Novejší verze s predáváním pouze indexu
    protected string GetString(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetString(o, p);
        }
        return string.Empty;
    }

    /// <summary>
    /// Když bude DBNull, G 0
    /// </summary>
    /// <param name="dex"></param>
    
    protected Guid GetGuid(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetGuid(o, p);
        }
        return Guid.Empty;
    }
    #endregion
}