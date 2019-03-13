using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using sunamo.Values;
using System.Data;

public partial class MSDatabaseLayer{
    static SqlConnection _conn = null;
    static string cs = null;
    static bool closing = false;

    static string dataSource2 = "";
    static string database2 = "";

    public static string GetValues(params object[] sloupce)
    {
        return MSDatabaseLayerBase.GetValues(sloupce);
    }

    public static string databaseName
    {
        get
        {
            return database2;
        }
    }

    public static SqlConnection conn
    {
        get
        {
            return _conn;
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
                break;
            case SqlDbType.Int:
                return "int";
                break;
            case SqlDbType.Real:
                return "float";
                break;
            case SqlDbType.BigInt:
                return "long";
                break;
            case SqlDbType.Bit:
                return "bool";
                break;
            case SqlDbType.Date:
            case SqlDbType.DateTime:
            case SqlDbType.DateTime2:
            case SqlDbType.Time:
            case SqlDbType.DateTimeOffset:
            case SqlDbType.SmallDateTime:
                return "DateTime";
                break;
            // Bude to až po všech běžně používaných datových typech, protože bych se měl vyvarovat ukládat do malé DB takové množství dat
            case SqlDbType.Timestamp:
            case SqlDbType.Binary:
            case SqlDbType.VarBinary:
            case SqlDbType.Image:
                return "byte[]";
                break;
            case SqlDbType.SmallMoney:
            case SqlDbType.Money:
            case SqlDbType.Decimal:
                return "decimal";
                break;
            case SqlDbType.Float:
                return "double";
                break;
            case SqlDbType.SmallInt:
                return "short";
                break;
            case SqlDbType.TinyInt:
                return "byte";
                break;
            case SqlDbType.Structured:
            case SqlDbType.Udt:
            case SqlDbType.Xml:
                throw new Exception("Snažíte se převést na int strukturovaný(složitý) datový typ");
                break;
            case SqlDbType.UniqueIdentifier:
                return "Guid";
                break;

            case SqlDbType.VarChar:
                return "string";
                break;
            case SqlDbType.Variant:
                return "object";
                break;
            default:
                throw new Exception("Snažíte se převést datový typ, pro který není implementována větev");
                break;
        }
    }

    public static void AssignConnectionStringScz(HttpApplication app)
    {
        
        AssignConnectionString("Data Source=.;Database=sunamo.cz;Integrated Security=True;MultipleActiveResultSets=True;");
    }

public static void AssignConnectionString(string cs2)
    {

        if (_conn == null)
        {
            cs = cs2;
            LoadNewConnectionFirst(cs);
        }
    }

/// <summary>
    /// NSN
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ConvertObjectToDotNetType(object p)
    {
        if (p.GetType() == Consts.tString)
        {
            return "string";
        }
        else if (p.GetType() == Consts.tInt)
        {
            return "int";
        }
        else if (p.GetType() == Consts.tBool)
        {
            return "bool";
        }
        else if (p.GetType() == Consts.tDateTime)
        {
            return "DateTime";
        }
        else if (p.GetType() == Consts.tBinary)
        {
            return "byte[]";
        }
        else if (p.GetType() == Consts.tFloat)
        {
            return "float";
        }
        else if (p.GetType().IsEnum)
        {
            return p.GetType().Name;
        }
        else if (p.GetType() == Consts.tDouble)
        {
            return "double";
        }
        else if (p.GetType() == Consts.tDecimal)
        {
            return "decimal";
        }
        else if (p.GetType() == Consts.tSbyte)
        {
            return "sbyte";
        }
        else if (p.GetType() == Consts.tSbyte)
        {
            return "byte";
        }
        else if (p.GetType() == Consts.tChar)
        {
            return "char";
        }
        else if (p.GetType() == Consts.tShort)
        {
            return "short";
        }
        else if (p.GetType() == Consts.tUshort)
        {
            return "ushort";
        }
        else if (p.GetType() == Consts.tUint)
        {
            return "uint";
        }
        else if (p.GetType() == Consts.tLong)
        {
            return "long";
        }
        else if (p.GetType() == Consts.uUlong)
        {
            return "ulong";
        }
        else
        {
            throw new Exception("Snažíte se vytvořit třídu s nepodporovaným typem");
        }
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

        conn.Disposed += new EventHandler(conn_Disposed);
        conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
        conn.StateChange += new System.Data.StateChangeEventHandler(conn_StateChange);
        return vr;
    }

public static void LoadNewConnectionFirst(string cs2)
    {
        LoadNewConnection(cs2);
        if (_conn != null)
        {
            _conn.Disposed += new EventHandler(conn_Disposed);
            _conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
            _conn.StateChange += new System.Data.StateChangeEventHandler(conn_StateChange);
        }
    }

    static void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
    {
        // TODO: Později implementovat
    }

    static void conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
    {
        if (e.CurrentState == System.Data.ConnectionState.Broken)
        {
            if (_conn != null && !string.IsNullOrEmpty(_conn.ConnectionString))
            {
                if (!closing)
                {

                    _conn.Open();
                }

            }
        }
        else if (e.CurrentState == System.Data.ConnectionState.Closed)
        {
            if (_conn != null && !string.IsNullOrEmpty(_conn.ConnectionString))
            {
                if (!closing)
                {
                    _conn.Open();
                }

            }
        }
    }

    static void conn_Disposed(object sender, EventArgs e)
    {
        if (!closing)
        {
            LoadNewConnection(cs);
        }
    }

/// <summary>
    /// Není veřejná, místo ní používej pro otevírání databáze metodu LoadNewConnectionFirst
    /// Používá se když chci otevřít nějakou DB která nenese jen jméno aplikace
    /// </summary>
    /// <param name="file"></param>
    private static bool LoadNewConnection(string dataSource, string database)
    {
        string cs = null;
        cs = "Data Source=" + dataSource;
        if (!string.IsNullOrEmpty(database))
        {
            cs += ";Database=" + database;
        }
        cs += ";Integrated Security=True;MultipleActiveResultSets=True;";
        _conn = new SqlConnection(cs);
        try
        {
            _conn.Open();
        }
        catch (Exception)
        {
            return false;
        }
        return true;

    }
public static void LoadNewConnection(string cs)
    {
        _conn = new SqlConnection(cs);

        if (!string.IsNullOrEmpty(_conn.ConnectionString))
        {
            _conn.Open();
        }
        
    }

    internal static string GetValuesDirect(int v)
    {
        return MSDatabaseLayerBase.GetValuesDirect(v);
    }
}