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

    public static SqlConnection conn
    {
        get
        {
            return _conn;
        }
    }

    

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
        if (p == Consts.tString)
        {
            return "string";
        }
        else if (p == Consts.tInt)
        {
            return "int";
        }
        else if (p == Consts.tBool)
        {
            return "bool";
        }
        else if (p == Consts.tDateTime)
        {
            return "DateTime";
        }
        else if (p == Consts.tBinary)
        {
            return "byte[]";
        }
        else if (p == Consts.tFloat)
        {
            return "float";
        }
        else if (p.IsEnum)
        {
            return p.Name;
        }
        else if (p == Consts.tDouble)
        {
            return "double";
        }
        else if (p == Consts.tDecimal)
        {
            return "decimal";
        }
        else if (p == Consts.tSbyte)
        {
            return "sbyte";
        }
        else if (p == Consts.tByte)
        {
            return "byte";
        }
        else if (p == Consts.tChar)
        {
            return "char";
        }
        else if (p == Consts.tShort)
        {
            return "short";
        }
        else if (p == Consts.tUshort)
        {
            return "ushort";
        }
        else if (p == Consts.tUint)
        {
            return "uint";
        }
        else if (p == Consts.tLong)
        {
            return "long";
        }
        else if (p == Consts.uUlong)
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
                return "byte[]";

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

    public static SqlConnection _conn = null;
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

    

    

    public static void AssignConnectionStringScz()
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