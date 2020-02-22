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
    /// <returns></returns>
    protected int GetInt(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetInt(o, p);
        }
        return  0;
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected float GetFloat(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetFloat(o, p);
        }
        return -1;
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected long GetLong(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetLong(o, p);
        }
        return -1;
    }



    /// <summary>
    /// Vrací výstup metody bool.Parse
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected bool GetBoolMS(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetBoolMS(o, p);
        }
        return false;
    }

    protected bool GetBool(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetBool(o, p);
        }
        return false;
    }

    /// <summary>
    /// Vrací výstup metody BoolToStringEn - tedu ano/ne. Když bude null, G Ne.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected string GetBoolS(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetBoolS(o, p);
        }
        return false.ToString();
    }

    /// <summary>
    /// Když bude null, G DT.MiV
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected System.DateTime GetDateTime(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetDateTime(o, p);
        }
        return DateTime.MaxValue;
    }

    /// <summary>
    /// Může vrátit null když se bude rovnat DBNull.Value
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected string GetDateTimeS(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetDateTimeS(o, p);
        }
        return DateTime.MaxValue.ToString();
    }

    protected byte[] GetImage(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetImage(o, p);
        }
        return new byte[0];
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected decimal GetDecimal(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetDecimal(o, p);
        }
        return -1;
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected double GetDouble(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetDouble(o, p);
        }
        return -1;
    }

    /// <summary>
    /// Když bude null, G -1
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected short GetShort(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetShort(o, p);
        }
        return -1;
    }

    /// <summary>
    /// Když bude null, G 0
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected byte GetByte(int p)
    {
        if (o.Count > p)
        {
            return StaticParse.GetByte(o, p);
        }
        return 0;
    }

    /// <summary>
    /// Když bude null, G null
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
    protected object GetObject(int p)
    {
        if (o.Count > p)
        {
            return o[p];
        }return null;
        
    }

    /// <summary>
    /// Když bude null, G Guid.Empty
    /// </summary>
    /// <param name="dex"></param>
    /// <returns></returns>
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