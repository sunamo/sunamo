using System.Reflection;
using System;

public class RHSQLite
{
    public static string chyba = "";
    public static InsertedRows chybaInsertedRows = null;
    public static ChangedRows chybaChangedRows = null;

    public static bool IsNullOrWhiteSpaceField(Type t, string vstup, string nazev)
    {
        // t mus� b�t t��da ve kter� je A1, ne A1 samotn�!!
        FieldInfo fi =  t.GetField(nazev);
        // Zde mus� b�t null
        string s = fi.GetValue(null).ToString();
        bool vr = SH.IsNullOrWhiteSpace(s);
        if (vr)
        {
            chyba = "Pol��ko " +  nazev + " nem��e b�t pr�zdn�";
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
            chybaInsertedRows = new InsertedRows("Pol��ko " + nazev + " nem��e b�t pr�zdn�. ");
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
            chybaChangedRows = new ChangedRows("Pol��ko " + nazev + " nem��e b�t pr�zdn�. ");
        }
        return vr;
    }
}
