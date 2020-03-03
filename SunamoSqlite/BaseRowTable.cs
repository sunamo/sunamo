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
    protected int GetInt(int p)
    {
        return TableRowParse.GetInt(o, p);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="p"></param>
    protected float GetFloat(int p)
    {
        return TableRowParse.GetFloat(o, p);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    protected long GetLong(int p)
    {
        return TableRowParse.GetLong(o, p);
    }



    /// <summary>
    /// Vrací výstup metody bool.Parse
    /// </summary>
    /// <param name="p"></param>
    protected bool GetBool(int p)
    {
        return TableRowParse.GetBool(o, p);
    }



    /// <summary>
    /// Vrací výstup metody BoolToStringEn - tedu ano/ne. Když bude null, G Ne.
    /// </summary>
    /// <param name="p"></param>
    protected string GetBoolS(int p)
    {
        return TableRowParse.GetBoolS(o, p);
    }

    /// <summary>
    /// Když bude null, G DT.MiV
    /// </summary>
    /// <param name="p"></param>
    protected System.DateTime GetDateTime(int p)
    {
        return TableRowParse.GetDateTime(o, p);
    }

    /// <summary>
    /// Může vrátit null když se bude rovnat DBNull.Value
    /// </summary>
    /// <param name="p"></param>
    protected string GetDateTimeS(int p)
    {
        return TableRowParse.GetDateTimeS(o, p);
    }

    protected byte[] GetImage(int dex)
    {
        return TableRowParse.GetImage(o, dex);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    protected decimal GetDecimal(int p)
    {
        return TableRowParse.GetDecimal(o, p);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    protected double GetDouble(int p)
    {
        return TableRowParse.GetDouble(o, p);
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    protected short GetShort(int p)
    {
        return TableRowParse.GetShort(o, p);
    }

    /// <summary>
    /// Když bude null, G 0
    /// </summary>
    /// <param name="dex"></param>
    protected byte GetByte(int p)
    {
        return TableRowParse.GetByte(o, p);
    }

    /// <summary>
    /// Když bude null, G null
    /// </summary>
    /// <param name="dex"></param>
    protected object GetObject(int p)
    {
        return o[p];
    }

    /// <summary>
    /// Když bude null, G Guid.Empty
    /// </summary>
    /// <param name="dex"></param>
    protected Guid GetGuid(int p)
    {
        return TableRowParse.GetGuid(o, p);
    }
}