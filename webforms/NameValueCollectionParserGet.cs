using System.Collections.Specialized;
/// <summary>
/// Nemůže být statická protože kdyby parsala různé nvc najednou tak by házela blbosti
/// Tuto třídu nepoužívej, protože musíš při instanci vytvořit všechny proměnné jako int, long, byte, string atd.
/// Místo toho zapiš Request.QueryString.Get("id") a poté se pokus vyparsovat id na správný typ metodou TryParse
/// </summary>
public class NameValueCollectionParserGet
{
    NameValueCollection nvc = null;
    public string lastString = null;
    public int lastInt = int.MinValue;
    public long lastLong = long.MinValue;

    public int Count
    {
        get
        {
            return nvc.Count;
        }
    }

    public NameValueCollectionParserGet(NameValueCollection nvc)
    {
        this.nvc = nvc;
    }

    public bool IsString(string key)
    {
        lastString = nvc.Get(key);
        if (lastString == null || lastString == "")
        {
            // Důsledně dbej na to aby všude kde nelze vrátit výsledné číslo si dával int.MinValue
            return false;
        }

        return true;
    }

    public bool IsLong(string key)
    {
        string vr = nvc.Get(key);
        if (vr == null || vr == "")
        {
            // Důsledně dbej na to aby všude kde nelze vrátit výsledné číslo si dával int.MinValue
            return false;
        }

        if (long.TryParse(vr, out lastLong))
        {
            return true;
        }
        return false;
    }

    public bool IsInt(string key)
    {
        string vr = nvc.Get(key);
        if (vr == null || vr == "")
        {
            // Důsledně dbej na to aby všude kde nelze vrátit výsledné číslo si dával int.MinValue
            return false;
        }

        if (int.TryParse(vr, out lastInt))
        {
            return true;
        }
        return false;
    }

    public bool lastBool = false;

    public bool IsBool(string p)
    {
        string vr = nvc[p];
        if (!string.IsNullOrWhiteSpace(vr))
        {
            //vr = vr.ToLower();
            return bool.TryParse(vr, out lastBool);
        }
        return false;
        
    }

    public string GetString(string p)
    {
        if (IsString(p))
        {
            return lastString;
        }
        return "";
    }

    public int GetInt(string p)
    {
        if (IsInt(p))
        {
            return lastInt;
        }
        return int.MinValue;
    }



    
}
