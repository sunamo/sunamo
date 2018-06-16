using System;
using System.Collections.Generic;
using System.Text;

public class ApplicationDataContainerList : Dictionary<string, AB>
{
    string path = null;

    /// <summary>
    /// Parse text file in format key|fullname|value
    /// </summary>
    /// <param name="path"></param>
    public ApplicationDataContainerList(string path)
    {
        this.path = path;
        string content = TF.ReadFile(path);
        if (content.Length != 0)
        {
            content = content.Substring(0, content.Length - 1);
        }
        
        string[] d = SH.SplitNone(content, "|");
        int to = d.Length / 3;
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
                base.Add(key, get);
            }
        }
    }

    public object this[string key]
    {
        get
        {
            if (base.ContainsKey(key))
            {
                return base[key].B;
            }
            return null;
        }
        set
        {
            string typeName = value.GetType().FullName;
            if (base.ContainsKey(key))
            {
                AB ab = base[key];
                if (typeName == ab.A)
                {
                    ab.B = value;
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in this)
                    {
                        sb.Append(SF.PrepareToSerialization(item.Key, item.Value.A, item.Value.B));
                    }
                    TF.SaveFile(sb.ToString(), path);
                }
                else
                {
                    throw new Exception(string.Format( "Pravděpodobně chyba v aplikaci, pokoušíte se uložit do souboru v AppData položku typu {0} pod klíčem {1} která měla původně typ {2}", typeName, key, ab.A));
                }
            }
            else
            {
                AB ab = AB.Get(typeName, value);
                base.Add(key, ab);
                string zapsatDoSouboru = SF.PrepareToSerialization(key, typeName, value.ToString());
                TF.AppendToFile(zapsatDoSouboru, path);
            }
        }
    }
}
