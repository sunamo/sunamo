using System.Reflection;
using System;

public class RHSQLite
{
    public static string chyba = "";
    public static InsertedRows chybaInsertedRows = null;
    public static ChangedRows chybaChangedRows = null;

    public static bool IsNullOrWhiteSpaceField(Type t, string vstup, string nazev)
    {
        // t musi byt trida ve ktere je A1, ne A1 samotne!!
        FieldInfo fi =  t.GetField(nazev);
        // Zde musi byt null
        string s = fi.GetValue(null).ToString();
        bool vr = SH.IsNullOrWhiteSpace(s);
        if (vr)
        {
            chyba = "Polozko" + " " +  nazev + " " + "nemuze byt prazdna";
        }
        return vr;
    }

    public static bool IsNullOrWhiteSpaceFieldInsertedRows(object o, string vstup, string nazev)
    {
        Type t = o.GetType();
        FieldInfo fi = t.GetField(nazev);
        string s = fi.GetValue(o).ToString();
        bool vr = SH.IsNullOrWhiteSpace(s);
        if (vr)
        {
            chybaInsertedRows = new InsertedRows("Polozko" + " " + nazev + " " + "nemuze byt prazdna" + ". ");
        }
        return vr;
    }

    public static bool IsNullOrWhiteSpaceFieldChangedRows(object o, string vstup, string nazev)
    {
        Type t = o.GetType();
        FieldInfo fi = t.GetField(nazev);
        string s = fi.GetValue(o).ToString();
        bool vr = SH.IsNullOrWhiteSpace(s);
        if (vr)
        {
            chybaChangedRows = new ChangedRows("Polozko" + " " + nazev + " " + "nemuze byt prazdna" + ". ");
        }
        return vr;
    }
}
