using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

public partial class MSDatabaseLayer :MSDatabaseLayerBase
{
    #region Not dependent on Connection
    /// <summary>
    /// Je stejná jako metoda GetValues, až na to že počítá od 1 a ne od 0
    /// </summary>
    /// <param name="sloupce"></param>
    
    public static string ConvertObjectToDotNetType(Type p)
    {
        if (p == Types.tString)
        {
            return "string";
        }
        else if (p == Types.tInt)
        {
            return "int";
        }
        else if (p == Types.tBool)
        {
            return "bool";
        }
        else if (p == Types.tDateTime)
        {
            return "DateTime";
        }
        else if (p == Types.tBinary)
        {
            return "byte" + "[]";
        }
        else if (p == Types.tFloat)
        {
            return "float";
        }
        else if (p.IsEnum)
        {
            return p.Name;
        }
        else if (p == Types.tDouble)
        {
            return "double";
        }
        else if (p == Types.tDecimal)
        {
            return "decimal";
        }
        else if (p == Types.tSbyte)
        {
            return "sbyte";
        }
        else if (p == Types.tByte)
        {
            return "byte";
        }
        else if (p == Types.tChar)
        {
            return "char";
        }
        else if (p == Types.tShort)
        {
            return "short";
        }
        else if (p == Types.tUshort)
        {
            return "ushort";
        }
        else if (p == Types.tUint)
        {
            return "uint";
        }
        else if (p == Types.tLong)
        {
            return "long";
        }
        else if (p == Types.tUlong)
        {
            return "ulong";
        }
        else
        {
            throw new Exception("Snažíte se vytvořit název třídy s nepodporovaným typem");
        }
    }

    public static string ConvertSqlDbTypeToDotNetType(SqlDbType p)
    {
        switch (p)
        {
            case SqlDbType.Text:
            case SqlDbType.Char:
            case SqlDbType.NText:
            case SqlDbType.NChar:
            case SqlDbType.NVarChar:
                return "string";

            case SqlDbType.Int:
                return "int";

            case SqlDbType.Real:
                return "float";

            case SqlDbType.BigInt:
                return "long";

            case SqlDbType.Bit:
                return "bool";

            case SqlDbType.Date:
            case SqlDbType.DateTime:
            case SqlDbType.DateTime2:
            case SqlDbType.Time:
            case SqlDbType.DateTimeOffset:
            case SqlDbType.SmallDateTime:
                return "DateTime";

            // Bude to až po všech běžně používaných datových typech, protože bych se měl vyvarovat ukládat do malé DB takové množství dat
            case SqlDbType.Timestamp:
            case SqlDbType.Binary:
            case SqlDbType.VarBinary:
            case SqlDbType.Image:
                return "byte" + "[]";

            case SqlDbType.SmallMoney:
            case SqlDbType.Money:
            case SqlDbType.Decimal:
                return "decimal";

            case SqlDbType.Float:
                return "double";

            case SqlDbType.SmallInt:
                return "short";

            case SqlDbType.TinyInt:
                return "byte";

            case SqlDbType.Structured:
            case SqlDbType.Udt:
            case SqlDbType.Xml:
                throw new Exception("Snažíte se převést na int strukturovaný(složitý) datový typ");

            case SqlDbType.UniqueIdentifier:
                return "Guid";


            case SqlDbType.VarChar:
                return "string";

            case SqlDbType.Variant:
                return "object";

            default:
                throw new Exception("Snažíte se převést datový typ, pro který není implementována větev");
        }
    }

    public static SqlDbType GetProbablyDataType(string p)
    {
        string trim = p.Trim();

        if (trim == "NULL" || trim == "")
        {
            return SqlDbType.NVarChar;
        }
        if (trim.StartsWith("BLOB("))
        {
            return SqlDbType.VarBinary;
        }
        var d = SH.SplitNone(trim, AllStrings.comma);
        if (d.Length() == 2)
        {
            if (BTS.IsInt(d[0]) && BTS.IsInt(d[1]))
            {
                return SqlDbType.Real;
            }
        }
        else
        {
            if (BTS.ParseByte(trim) != byte.MinValue)
            {
                return SqlDbType.TinyInt;
            }
            if (BTS.ParseShort(trim) != short.MinValue)
            {
                return SqlDbType.SmallInt;
            }
            if (BTS.ParseInt(trim) != int.MinValue)
            {
                return SqlDbType.Int;
            }
            else if (BTS.IsDateTime(trim))
            {
                return SqlDbType.SmallDateTime;
            }
            else if (BTS.IsBool(trim))
            {
                return SqlDbType.Bit;
            }
        }
        return SqlDbType.NVarChar; ;
    }

    public static object ConvertStringToObject(string vstup, SqlDbType type)
    {
        vstup = vstup.Trim();
        switch (type)
        {
            case SqlDbType.NVarChar:
                return vstup;
            case SqlDbType.Int:
                return int.Parse(vstup);
            case SqlDbType.Bit:
                return bool.Parse(vstup);
            case SqlDbType.SmallDateTime:
                return DateTime.Parse(vstup);
            case SqlDbType.Real:
                return float.Parse(vstup);
            case SqlDbType.TinyInt:
                return byte.Parse(vstup);
            case SqlDbType.SmallInt:
                return short.Parse(vstup);

            case SqlDbType.BigInt:

            case SqlDbType.Binary:


            case SqlDbType.Char:

            case SqlDbType.Date:

            case SqlDbType.DateTime:

            case SqlDbType.DateTime2:

            case SqlDbType.DateTimeOffset:

            case SqlDbType.Decimal:

            case SqlDbType.Float:

            case SqlDbType.Image:


            case SqlDbType.Money:

            case SqlDbType.NChar:

            case SqlDbType.NText:





            case SqlDbType.SmallMoney:

            case SqlDbType.Structured:

            case SqlDbType.Text:

            case SqlDbType.Time:

            case SqlDbType.Timestamp:



            case SqlDbType.Udt:

            case SqlDbType.UniqueIdentifier:

            case SqlDbType.VarBinary:

            case SqlDbType.VarChar:

            case SqlDbType.Variant:

            case SqlDbType.Xml:

            default:
                throw new Exception("Nepodporovaný datový typ");

        }
    }


    static string _cs = null;

    public static string cs
    {
        get => _cs; set
        {
            _cs = value;
        }
    }

    //static bool closing = false;

    

    public static string databaseName
    {
        get
        {
            return database2;
        }
    } 
    #endregion


    
    public static bool LoadNewConnection(string cs)
    {
        MSDatabaseLayer.cs = cs;

        //conn = new SqlConnection(cs);
        //try
        //{
        //    conn.Open();
        //    //conn.Close();
        //    //conn.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    return false;
        //}
        return true;
        //if (!string.IsNullOrEmpty(_conn.ConnectionString))
        //{
        //    //OpenWhenIsNotOpen();
        //    conn.Open();
        //}

    }

    /// <summary>
    /// Používá se ve desktopových aplikacích
    /// Používá se když chci otevřít nějakou DB která nenese jen jméno aplikace
    /// </summary>
    /// <param name="file"></param>
    public static bool LoadNewConnectionFirst(string dataSource, string database)
    {
        dataSource2 = dataSource;
        database2 = database;
        bool vr = LoadNewConnection(dataSource, database);
        //RegisterEvents();
        return vr;
    }

    /// <summary>
    /// Cant be in MSDatabaseLayerBase because it was shared between MSDatabaseLayer and MSDatabaseLayerSql5
    /// </summary>
    public static SqlConnection conn = null;
    /// <summary>
    /// Cant be in MSDatabaseLayerBase because it was shared between MSDatabaseLayer and MSDatabaseLayerSql5
    /// </summary>
    public static string dataSource2 = "";
    /// <summary>
    /// Cant be in MSDatabaseLayerBase because it was shared between MSDatabaseLayer and MSDatabaseLayerSql5
    /// </summary>
    public static string database2 = "";


    /// <summary>
    /// Není veřejná, místo ní používej pro otevírání databáze metodu LoadNewConnectionFirst
    /// Používá se když chci otevřít nějakou DB která nenese jen jméno aplikace
    /// </summary>
    /// <param name="file"></param>
    private static bool LoadNewConnection(string dataSource, string database)
    {

        cs = "Data Source=" + dataSource;
        if (!string.IsNullOrEmpty(database))
        {
            cs += ";Database=" + database;
        }
        cs += ";" + "Integrated Security=True;MultipleActiveResultSets=True" + ";TransparentNetworkIPResolution=False;Max Pool Size=50000;Pooling=True;";
        //_conn = new SqlConnection(cs);

        //OpenWhenIsNotOpen();
        //conn.Open();

        return LoadNewConnection(cs);


    }


    #region Commented
    //public static void AssignConnectionStringScz()
    //{

    //    AssignConnectionString("Data Source=46.36.40.198;Database=sunamo.cz;User ID=sa;Password="+ AppData.ci.GetCommonSettings(CommonSettingsKeys.pwSql) + ";MultipleActiveResultSets=True" + ";");
    //}

    //public static void AssignConnectionStringLocalScz()
    //{

    //    AssignConnectionString("Data Source=.;Database=sunamo.cz;Integrated Security=True;MultipleActiveResultSets=True" + ";");
    //}

    //public static void AssignConnectionString(string cs2)
    //{
    //    if (_conn == null)
    //    {
    //        cs = cs2;
    //        LoadNewConnectionFirst(cs);
    //    }
    //}

    //private static void RegisterEvents()
    //{
    //    conn.Disposed -= new EventHandler(conn_Disposed);
    //    conn.InfoMessage -= new SqlInfoMessageEventHandler(conn_InfoMessage);
    //    conn.StateChange -= new System.Data.StateChangeEventHandler(conn_StateChange);

    //    conn.Disposed += new EventHandler(conn_Disposed);
    //    conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
    //    conn.StateChange += new System.Data.StateChangeEventHandler(conn_StateChange);
    //}

    //static void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
    //{
    //    // TODO: Později implementovat
    //}

    ////public static Action loadDefaultDatabase;

    ///// <summary>
    ///// Bad pattern, there happen "Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached."
    ///// </summary>
    //public static void OpenWhenIsNotOpen()
    //{
    //    if (_conn.State != ConnectionState.Open)
    //    {
    //        conn.Open();
    //    }
    //}

    //static void conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
    //{
    //    if (e.CurrentState == System.Data.ConnectionState.Broken)
    //    {
    //        if (_conn != null && !string.IsNullOrEmpty(_conn.ConnectionString))
    //        {
    //            if (!closing)
    //            {
    //                //OpenWhenIsNotOpen();
    //                conn.Open();

    //            }

    //        }
    //    }
    //    else if (e.CurrentState == System.Data.ConnectionState.Closed)
    //    {
    //        if (_conn != null && string.IsNullOrEmpty(_conn.ConnectionString))
    //        {
    //            if (!closing)
    //            {
    //                ReloadConnection();
    //                //
    //            }

    //        }
    //    }
    //}

    //private static void ReloadConnection()
    //{
    //    if (string.IsNullOrEmpty(_conn.ConnectionString))
    //    {
    //        //loadDefaultDatabase();
    //    }
    //    else
    //    {
    //        //OpenWhenIsNotOpen();
    //        conn.Open();
    //    }
    //}

    //static void conn_Disposed(object sender, EventArgs e)
    //{
    //    if (!closing)
    //    {
    //        //ReloadConnection();
    //        LoadNewConnection(cs);
    //    }
    //} 
    #endregion
}