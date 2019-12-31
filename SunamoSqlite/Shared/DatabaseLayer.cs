/// <summary>
/// 
/// </summary>


using System.IO;
using System;
using System.Data.SQLite;

using System.Diagnostics;
using System.Text;
using sunamo.Essential;
using System.Collections.Generic;
using System.Data;

public class DatabaseLayer
{
    public static void Init(string dbPath)
    {
        try
        {

            SQLiteConnection.CreateFile(dbPath);
            Load(dbPath);
        }
        catch (Exception ex)
        {
        }
    }

    public static void Load(string dbPath)
    {
        try
        {
            DatabaseLayer.dbFile = dbPath;
            DatabaseLayer.LoadNewConnection();
            SloupecDBBase<SloupecDB, TypeAffinity>.databaseLayer = new DatabaseLayerInstance();
            // only to read
            //DatabaseLayer.conn.AutoCommit = true;
        }
        catch (Exception ex)
        {

            ThisApp.SetStatus(TypeOfMessage.Error, Exceptions.TextOfExceptions(ex));
        }
        
    }

    public static bool IsSqlite(string path)
    {
        byte[] bytes = new byte[17];
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            fs.Read(bytes, 0, 16);
        }
        string chkStr = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
        return chkStr.Contains("SQLite format");
    }

    /// <summary>
    /// Jsou rozděleny do 2 dict ze 2 důvodů: 
    /// 1) aby se rychleji získavali popisy daných datových typů
    /// 2) aby jsem odlišil a zaznamenal typy které chci používat a které nikoliv
    /// </summary>
    public static Dictionary<TypeAffinity, string> usedTa = new Dictionary<TypeAffinity, string>();
    public static Dictionary<TypeAffinity, string> hiddenTa = new Dictionary<TypeAffinity, string>();

    public static DatabaseLayerInstance ci = new DatabaseLayerInstance();

    public static SQLiteConnection conn = null;
    public static string dbFile = null;

    static DatabaseLayer()
    {
        SloupecDBBase<SloupecDB, TypeAffinity>.factoryColumnDB = new FactoryColumnDB();
    }

    private DatabaseLayer()
    {
    }

    private static void conn_Disposed(object sender, EventArgs e)
    {
        DatabaseLayer.LoadNewConnection();
    }

    static public string ToBlob(byte[] ba)
    {
        if (ba == null || ba.Length == 0)
        {
            return "";
        }
        const string HexFormat = "{0:X2}";
        StringBuilder sb = new StringBuilder();
        foreach (byte b in ba)
        {
            sb.Append(SH.Format2(HexFormat, b));
        }
        return "X'" + sb.ToString() + "'";
    }

    /// <summary>
    /// converts from a string Hex representation to an array of bytes
    /// Převedu řetězec v hexadeximální formátu A1 na pole bytů. Pokud nebude hex formát(napříkal nebude mít sudý počet znaků), VV
    /// </summary>
    static public byte[] FromBlob(string hexEncoded)
    {
        if (hexEncoded == null || hexEncoded.Length == 0)
        {
            return null;
        }
        try
        {
            hexEncoded = hexEncoded.Replace("X'", "").TrimEnd(AllChars.bs); ;

            int l = Convert.ToInt32(hexEncoded.Length / 2);
            byte[] b = new byte[l];
            for (int i = 0; i <= l - 1; i++)
            {
                b[i] = Convert.ToByte(hexEncoded.Substring(i * 2, 2), 16);
            }
            return b;
        }
        catch (Exception ex)
        {
            throw new System.FormatException("The provided string does not appear to be Hex encoded" + ":" + Environment.NewLine + hexEncoded + Environment.NewLine, ex);
        }
    }

    private static bool s_zaheslovat = false;

    private static string s_applicationStartupPath = null;
    public static string ApplicationStartupPath
    {
        get
        {
            return s_applicationStartupPath;
        }
        set
        {
            s_applicationStartupPath = value;
        }
    }

    public static void LoadNewConnection()
    {
        if (!FS.ExistsFile(dbFile))
        {
            string nad = Path.GetDirectoryName(dbFile);
            FS.CreateFoldersPsysicallyUnlessThere(nad);
            Environment.CurrentDirectory = nad;
            string nazevSpustitelneExeDB = "sqlite3.exe";
            string sDbExe = FS.Combine(nad, nazevSpustitelneExeDB);
            if (!FS.ExistsFile(sDbExe))
            {
                //File.Copy(FS.Combine(slozkaAktualniVerze, nazevSpustitelneExeDB), 
                FS.CopyTo(FS.Combine(s_applicationStartupPath, nazevSpustitelneExeDB), nad);
            }

            // TODO: S uvozovkami se to zadávat nedá, zjisti jak se zadává cesta k programu když jsou v ní uvozovky
            string cmd = "" + nad + "\\sqlite3.exe" + "" + "";
            Process cess = Process.Start(cmd, FS.GetFileNameWithoutExtension(dbFile));
            cess.Kill();
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + dbFile + "");
            conn.Open();
            if (s_zaheslovat)
            {
                conn.ChangePassword("olsehheslo");
            }
        }
        else
        {
            //"Data Source=F:\Mona\sunamo\DocArch\Data\DocArch.db3;Version=3;Password=olsehheslo;"
            string cs = "Data Source=" + dbFile + ";" + "Version=3" + ";";
            if (s_zaheslovat)
            {
                cs += "Password=olsehheslo" + ";";
            }
            conn = new SQLiteConnection(cs);
            conn.Open();
            if (s_zaheslovat)
            {
                conn.ChangePassword("olsehheslo");
            }
        }

        conn.DefaultTimeout = 10000;
    }
}