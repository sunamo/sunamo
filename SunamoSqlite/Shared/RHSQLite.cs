using System.Reflection;
using DocArch.SqLite;
using System;
public static class RHSQLite
{
    public static string chyba = "";
    public static InsertedRows chybaInsertedRows = null;
    public static ChangedRows chybaChangedRows = null;

    public static bool IsNullOrWhiteSpaceField(Type t, string vstup, string nazev)
    {
        // t musí být třIda ve které je A1, ne A1 samotné!!
        FieldInfo fi =  t.GetField(nazev);
        // Zde musI být null
        string s = fi.GetValue(null).ToString();
        bool vr = SH.IsNullOrWhiteSpace(s);
        if (vr)
        {
            chyba = "PolLOko " +  nazev + " nemůže být prázdná";
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
            chybaInsertedRows = new InsertedRows("PolOZko " + nazev + " nemůZe být prPzdná. ");
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
            chybaChangedRows = new ChangedRows("Pol¢Zko " + nazev + " nemUZe být prázdná. ");
        }
        return vr;
    }
}
