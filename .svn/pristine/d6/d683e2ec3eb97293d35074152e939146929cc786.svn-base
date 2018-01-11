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
    /// <returns></returns>
    protected int GetInt(int p)
    {
        return MSTableRowParse.GetInt(o, p);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected float GetFloat(int p)
    {
        return MSTableRowParse.GetFloat(o, p);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected long GetLong(int p)
    {
        return MSTableRowParse.GetLong(o, p);
    }

    

    /// <summary>
    /// Vrací výstup metody bool.Parse
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected bool GetBoolMS(int p)
    {
        return MSTableRowParse.GetBoolMS(o, p);
    }

    protected bool GetBool(int p)
    {
        return MSTableRowParse.GetBool(o, p);
    }

    /// <summary>
    /// Vrací výstup metody BoolToStringCs - tedu ano/ne. Když bude null, G Ne.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected string GetBoolS(int p)
    {
        return MSTableRowParse.GetBoolS(o, p);
    }

    /// <summary>
    /// Když bude null, G DT.MiV
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected System.DateTime GetDateTime(int p)
    {
        return MSTableRowParse.GetDateTime(o, p);
    }

    /// <summary>
    /// Může vrátit null když se bude rovnat DBNull.Value
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected string GetDateTimeS(int p)
    {
        return MSTableRowParse.GetDateTimeS(o, p);
    }

    protected byte[] GetImage(int dex)
    {
        return MSTableRowParse.GetImage(o, dex);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected decimal GetDecimal(int p)
    {
        return MSTableRowParse.GetDecimal(o, p);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected double GetDouble(int p)
    {
        return MSTableRowParse.GetDouble(o, p);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected short GetShort(int p)
    {
        return MSTableRowParse.GetShort(o, p);
    }

    /// <summary>
    /// Když bude null, G 0
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected byte GetByte(int p)
    {
        return MSTableRowParse.GetByte(o, p);
    }

    /// <summary>
    /// Když bude null, G null
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected object GetObject(int p)
    {
        return o[p];
    }

    /// <summary>
    /// Když bude null, G Guid.Empty
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected Guid GetGuid(int p)
    {
        return MSTableRowParse.GetGuid(o, p);
    }
    #endregion
}
