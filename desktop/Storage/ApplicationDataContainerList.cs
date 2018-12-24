using sunamo;
using sunamo.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


public class ApplicationDataContainerList
{
    string path = null;
    /// <summary>
    /// In key is name
    /// In AB Key full type
    /// </summary>
    Dictionary<string, AB> data = new Dictionary<string, AB>();


    public ApplicationDataContainerList(string path)
    {
        Init(path);
    }

    public ApplicationDataContainerList(FrameworkElement fw)
    {
        Init(AppData.GetFile(AppFolders.Controls, fw.Name));
    }

    /// <summary>
    /// Parse text file in format key|fullname|value
    /// </summary>
    /// <param name="path"></param>
    public void Init(string path)
    {
        this.path = path;
        string content = TF.ReadFile(path);
        if (content.Length != 0)
        {
            content = content.Substring(0, content.Length - 1);
        }
        
        string[] d = SH.SplitNone(content, "|");
        int to = (d.Length / 3) * 3;
        for (int i = 0; i < to; )
        {
            string key = d[i++];
            string fullName = d[i++];
            string third = d[i++];
            object value = null;
            switch (fullName)
            {
                case "System.Collections.Generic.List`1":
                    value = SF.GetAllElementsLine(third);
                    break;
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
                data.Add(key, get);
            }
        }
    }

    internal bool? GetNullableBool(string isChecked)
    {
        if (data.ContainsKey(isChecked))
        {
            return BTS.StringToBool(this[isChecked].ToString());
        }
        return false;
    }

    internal List<string> GetListString(string dataContext)
    {
        
        return DeserializeList(data.ContainsKey(dataContext) ? this[dataContext] : null );
    }

    public string GetString(string text)
    {
        if (data.ContainsKey(text))
        {
            return data[text].B.ToString();
        }
        else
        {
            // File doesnt exists on disc
            return null;
        }
    }

    List<string> DeserializeList(object value)
    {
        if (value == null)
        {
            return new List<string>();
        }

        var list2 = CA.ToListString(value as IEnumerable);
        return SF.GetAllElementsLine(list2[0], AllChars.comma).ToList();
    }

    public void Nuke()
    {
        data.Clear();
        SaveFile();
    }

    public void DeleteEntry(string key)
    {
        data.Remove(key);
        SaveFile();
    }

    public object this[string key]
    {
        get
        {
            if (data.ContainsKey(key))
            {
                return data[key].B;
            }
            return null;
        }
        set
        {
            object val = value;
            string typeName = RH.FullPathCodeEntity( value.GetType());
            if (value is IList)
            {
               val = SF.PrepareToSerialization2(val as IEnumerable, AllStrings.comma);
            }
            if (data.ContainsKey(key))
            {
                AB ab = data[key];
                if (typeName == ab.A)
                {
                    ab.B = val;
                    
                    SaveFile();
                }
                else
                {
                    throw new Exception(string.Format( "Pravděpodobně chyba v aplikaci, pokoušíte se uložit do souboru v AppData položku typu {0} pod klíčem {1} která měla původně typ {2}", typeName, key, ab.A));
                }
            }
            else
            {
                AB ab = AB.Get(typeName, val);
                data.Add(key, ab);
                string zapsatDoSouboru = SF.PrepareToSerialization(CA.ToListString( key, typeName, SH.ListToString( value)));
                TF.AppendToFile(zapsatDoSouboru, path);
            }
        }
    }

    public bool Contains(string key)
    {
        return data.ContainsKey(key);
    }

    public void SaveFile()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in data)
        {
            sb.Append(SF.PrepareToSerialization(CA.ToListString( item.Key, item.Value.A, item.Value.B)));
        }
        TF.SaveFile(sb.ToString(), path);
    }
}
