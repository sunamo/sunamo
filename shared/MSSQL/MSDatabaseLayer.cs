using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

public partial class MSDatabaseLayer 
{
    public static MSDatabaseLayerInstance ci = new MSDatabaseLayerInstance();
    public static Action loadDefaultDatabase;
    public static SqlConnection conn
    {
        get
        {
            return _conn;
        }
    }

    public static SqlConnection _conn = null;
    //public static SqlConnection _conn
    //{
    //    get
    //    {
    //        return __conn;
    //    }
    //    set
    //    {
    //        __conn = value;
    //    }
    //}

    /// <summary>
    /// Direct znamená že mohu přímo zadat počet parametrů které si přeji vytvořit
    /// Parametr s číslem A1 už ve výsledku nebude.
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static string GetValuesDirect(int to)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(AllStrings.lb);
        for (int i = 0; i < to; i++)
        {
            sb.Append("@p" + (i).ToString() + AllStrings.comma);
        }
        return sb.ToString().TrimEnd(AllChars.comma) + AllStrings.rb;
    }

    /// <summary>
    /// NSN
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ConvertObjectToDotNetType(object p)
    {
        if (p.GetType() == Types.tString)
        {
            return "string";
        }
        else if (p.GetType() == Types.tInt)
        {
            return "int";
        }
        else if (p.GetType() == Types.tBool)
        {
            return "bool";
        }
        else if (p.GetType() == Types.tDateTime)
        {
            return "DateTime";
        }
        else if (p.GetType() == Types.tBinary)
        {
            return "byte" + "[]";
        }
        else if (p.GetType() == Types.tFloat)
        {
            return "float";
        }
        else if (p.GetType().IsEnum)
        {
            return p.GetType().Name;
        }
        else if (p.GetType() == Types.tDouble)
        {
            return "double";
        }
        else if (p.GetType() == Types.tDecimal)
        {
            return "decimal";
        }
        else if (p.GetType() == Types.tSbyte)
        {
            return "sbyte";
        }
        else if (p.GetType() == Types.tSbyte)
        {
            return "byte";
        }
        else if (p.GetType() == Types.tChar)
        {
            return "char";
        }
        else if (p.GetType() == Types.tShort)
        {
            return "short";
        }
        else if (p.GetType() == Types.tUshort)
        {
            return "ushort";
        }
        else if (p.GetType() == Types.tUint)
        {
            return "uint";
        }
        else if (p.GetType() == Types.tLong)
        {
            return "long";
        }
        else if (p.GetType() == Types.tUlong)
        {
            return "ulong";
        }
        else
        {
            throw new Exception("Snažíte se vytvořit třídu s nepodporovaným typem");
        }
    }

    public static string GetValues2(object[] sloupce)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(AllStrings.lb);
        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            sb.Append("@p" + (i).ToString() + AllStrings.comma);
        }
        return sb.ToString().TrimEnd(AllChars.comma) + AllStrings.rb;
    }

    /// <summary>
    /// Jsou rozděleny do 2 dict ze 2 důvodů: 
    /// 1) aby se rychleji získavali popisy daných datových typů
    /// 2) aby jsem odlišil a zaznamenal typy které chci používat a které nikoliv
    /// </summary>
    public static Dictionary<SqlDbType2, string> usedTa = new Dictionary<SqlDbType2, string>();
    public static Dictionary<SqlDbType2, string> hiddenTa = new Dictionary<SqlDbType2, string>();

    static MSDatabaseLayer()
    {
        usedTa.Add(SqlDbType2.SmallDateTime, "Datum a čas");
        usedTa.Add(SqlDbType2.Real, "Číslo s desetinnou čárkou");
        usedTa.Add(SqlDbType2.Int, "Číslo bez desetinné čárky v rozsahu od -2,147,483,648 do 2,147,483,647");
        usedTa.Add(SqlDbType2.NVarChar, "Omezený řetězec na max. 4000 znaků");
        usedTa.Add(SqlDbType2.Bit, "Duální hodnota pravda/nepravda");
        usedTa.Add(SqlDbType2.TinyInt, "Číslo bez desetinné čárky v rozsahu od 0 do 255");
        usedTa.Add(SqlDbType2.SmallInt, "Číslo bez desetinné čárky v rozsahu od -32,768 do 32,767");
        usedTa.Add(SqlDbType2.Binary, "Zápis bajtů v rozmezí velikosti 1-8000");

        // Je to sice nejlepší možná varianta(Text) pro ukládání textů, ale i tak to NEpatří do DB
        //hiddenTa.Add(SqlDbType2.Text, "Neomezený řetězec");
        hiddenTa.Add(SqlDbType2.VarChar, "Omezený řetězec na 8000 znaků");
        //hiddenTa.Add(SqlDbType2.NText, "Omezený řetězec na 1073741823");
        //hiddenTa.Add(SqlDbType2.VarBinary, "Zápis bajtů v rozmezí velikosti 1-8000");
        //hiddenTa.Add(SqlDbType2.Image, "Zápis bajtů v rozmezí velikosti 0-2147483647");

        hiddenTa.Add(SqlDbType2.Char, "Řetězec non-unicode s pevnou šířkou do velikosti 8000 znaků");
        hiddenTa.Add(SqlDbType2.NChar, "Řetězec Unicode s pevnou šířkou do velikosti 4000 znaků");

        SetFactoryColumnDb();
        
    }

    public static void SetFactoryColumnDb()
    {
        SloupecDBBase<MSSloupecDB, SqlDbType2>.factoryColumnDB = MSFactoryColumnDB.Instance;
    }

    /// <summary>
    /// Vrací @p0,@p1,...,@pX
    /// Vrátí tolik hodnot kolik je v A1.
    /// Do A1 se mohou předávat libovolné hodnoty - AB, string, object,...
    /// </summary>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public static string GetValues(params object[] sloupce)
    {
        int to = sloupce.Length;

        return GetValuesDirect(to);
    }

    /// <summary>
    /// NSN
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
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

    
    static string cs = null;
    static bool closing = false;

    static string dataSource2 = "";
    static string database2 = "";

    public static string databaseName
    {
        get
        {
            return database2;
        }
    }



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
        RegisterEvents();
        return vr;
    }

    private static void RegisterEvents()
    {
        conn.Disposed -= new EventHandler(conn_Disposed);
        conn.InfoMessage -= new SqlInfoMessageEventHandler(conn_InfoMessage);
        conn.StateChange -= new System.Data.StateChangeEventHandler(conn_StateChange);

        conn.Disposed += new EventHandler(conn_Disposed);
        conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
        conn.StateChange += new System.Data.StateChangeEventHandler(conn_StateChange);
    }

    public static void LoadNewConnectionFirst(string cs2)
    {
        LoadNewConnection(cs2);
        if (_conn != null)
        {
            RegisterEvents();
        }
    }

    static void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
    {
        // TODO: Později implementovat
    }

    public static Action loadDefaultDatabase;

    public static void OpenWhenIsNotOpen()
    {
        if (_conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
    }

    static void conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
    {
        if (e.CurrentState == System.Data.ConnectionState.Broken)
        {
            if (_conn != null && !string.IsNullOrEmpty(_conn.ConnectionString))
            {
                if (!closing)
                {
                    OpenWhenIsNotOpen();


                }

            }
        }
        else if (e.CurrentState == System.Data.ConnectionState.Closed)
        {
            if (_conn != null && string.IsNullOrEmpty(_conn.ConnectionString))
            {
                if (!closing)
                {
                    ReloadConnection();
                    //
                }

            }
        }
    }

    private static void ReloadConnection()
    {
        if (string.IsNullOrEmpty(_conn.ConnectionString))
        {
            loadDefaultDatabase();
        }
        else
        {
            OpenWhenIsNotOpen();
        }
    }

    static void conn_Disposed(object sender, EventArgs e)
    {
        if (!closing)
        {
            ReloadConnection();
            //LoadNewConnection(cs);
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
        cs += ";" + "Integrated Security=True;MultipleActiveResultSets=True" + ";";
        _conn = new SqlConnection(cs);
        try
        {
            OpenWhenIsNotOpen();
        }
        catch (Exception ex)
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
            OpenWhenIsNotOpen();
        }

    }

    /// <summary>
    /// Je stejná jako metoda GetValues, až na to že počítá od 1 a ne od 0
    /// </summary>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public static string GetValues1(params object[] sloupce)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(AllStrings.lb);
        int to = sloupce.Length + 1;
        for (int i = 1; i < to; i++)
        {
            sb.Append("@p" + (i).ToString() + AllStrings.comma);
        }
        return sb.ToString().TrimEnd(AllChars.comma) + AllStrings.rb;
    }



    public static List<object> GetUsedDataTypes()
    {
        List<object> vr = new List<object>();
        vr.Add("");
        foreach (var item in usedTa)
        {
            vr.Add(item.Key);
        }
        return vr;
    }

    public static SqlDbType GetSqlDbTypeFromType(Type type)
    {

        string t = type.ToString();
        switch (t)
        {
            case "System.Int32":
                return SqlDbType.Int;
            case "System.DateTime":
                return SqlDbType.SmallDateTime;
            case "System.Single":
                return SqlDbType.Real;
            case "System.String":
                return SqlDbType.NVarChar;
            case "System.Boolean":
                return SqlDbType.Bit;
            case "System.Int16":
                return SqlDbType.SmallInt;
            case "System.Byte":
                return SqlDbType.TinyInt;
            default:
                throw new Exception("Neimplementovaná větev");
              
        }
    }


}