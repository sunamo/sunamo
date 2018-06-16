using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Deriving from Dictionary<string, AB> can take pair or only value at single time and still use same key
/// </summary>
public class ApplicationDataContainerList : IEnumerable<KeyValuePair<string, AB>>
{
    string path = null;

    /// <summary>
    /// Must be here due to automatically saving after 
    /// 
    /// </summary>
     Dictionary<string, AB> dict = new Dictionary<string, AB>();



    /// <summary>
    /// Parse text file in format key|fullname|value
    /// </summary>
    /// <param name="path"></param>
    public ApplicationDataContainerList(string path)
    {
        this.path = path;
        string content = TF.ReadFile(path);
        // Trim last char
        if (content.Length != 0)
        {
            content = content.Substring(0, content.Length - 1);
        }
        
        string[] d = SH.SplitNone(content, "|");


        int to = d.Length;
        to -= d.Length % 3;
        

        for (int i = 0; i < to; )
        {
            string key = d[i++];
            string fullName = d[i++];
            string third = d[i++];
            object value = null;

            switch (fullName)
            {
                #region MyRegion
                case "System.String":
                    value = third.ToString();
                    break;
                case "System.Int32":
                    int oInt = -1;
                    if (int.TryParse(third, out oInt))
                    {
                        value = oInt;
                    }
                    break;
                case "System.Boolean":
                    bool oBool = false;
                    if (bool.TryParse(third, out oBool))
                    {
                        value = oBool;
                    }
                    break;
                case "System.Single":
                    float oFloat = -1;
                    if (float.TryParse(third, out oFloat))
                    {
                        value = oFloat;
                    }
                    break;
                case "System.DateTime":
                    DateTime oDT = DateTime.MinValue;
                    if (DateTime.TryParse(third, out oDT))
                    {
                        value = oDT;
                    }
                    break;
                case "System.Double":
                    double oDouble = -1;
                    if (double.TryParse(third, out oDouble))
                    {
                        value = oDouble;
                    }
                    break;
                case "System.Decimal":
                    decimal oDecimal = -1;
                    if (decimal.TryParse(third, out oDecimal))
                    {
                        value = oDecimal;
                    }
                    break;
                case "System.Char":
                    char oChar = 'm';
                    if (char.TryParse(third, out oChar))
                    {
                        value = oChar;
                    }
                    break;
                case "System.Byte":
                    byte oByte = 1;
                    if (byte.TryParse(third, out oByte))
                    {
                        value = oByte;
                    }
                    break;
                case "System.SByte":
                    sbyte oSbyte = -1;
                    if (sbyte.TryParse(third, out oSbyte))
                    {
                        value = oSbyte;
                    }
                    break;
                case "System.Int16":
                    short oShort = -1;
                    if (short.TryParse(third, out oShort))
                    {
                        value = oShort;
                    }
                    break;
                case "System.Int64":
                    long oLong = -1;
                    if (long.TryParse(third, out oLong))
                    {
                        value = oLong;
                    }
                    break;
                case "System.UInt16":
                    ushort oUshort = 1;
                    if (ushort.TryParse(third, out oUshort))
                    {
                        value = oUshort;
                    }
                    break;
                #endregion
                case "System.UInt32":
                    uint oUInt = 1;
                    if (uint.TryParse(third, out oUInt))
                    {
                        value = oUInt;
                    }
                    break;
                case "System.UInt64":
                    ulong oULong = 1;
                    if (ulong.TryParse(third, out oULong))
                    {
                        value = oULong;
                    }
                    break;
            }
            if (value != null)
            {
                AB get = AB.Get(fullName, value);
                dict.Add(key, get);
            }
        }
    }

    public void Nuke()
    {
        dict.Clear();
        SaveFile();
    }

    public object this[string key]
    {
        get
        {
            if (dict.ContainsKey(key))
            {
                return dict[key].B;
            }
            return null;
        }
        set
        {
            string typeName = value.GetType().FullName;
            if (dict.ContainsKey(key))
            {
                AB ab = dict[key];
                if (typeName == ab.A)
                {
                    ab.B = value;
                    SaveFile();
                }
                else
                {
                    throw new Exception(string.Format( "Pravděpodobně chyba v aplikaci, pokoušíte se uložit do souboru v AppData položku typu {0} pod klíčem {1} která měla původně typ {2}", typeName, key, ab.A));
                }
            }
            else
            {
                AB ab = AB.Get(typeName, value);
                dict.Add(key, ab);
                string zapsatDoSouboru = SF.PrepareToSerialization(key, typeName, value.ToString());
                TF.AppendToFile(zapsatDoSouboru, path);
            }
        }
    }

    /// <summary>
    /// Private, after always action save automatically
    /// </summary>
    private void SaveFile()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in dict)
        {
            sb.Append(SF.PrepareToSerialization(item.Key, item.Value.A, item.Value.B));
        }
        TF.SaveFile(sb.ToString(), path);
    }

    public void DeleteEntry(string key)
    {
        dict.Remove(key);
        SaveFile();
    }

    public IEnumerator<KeyValuePair<string, AB>> GetEnumerator()
    {
        return dict.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return dict.GetEnumerator();
    }
}
