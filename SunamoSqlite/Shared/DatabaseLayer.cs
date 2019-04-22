/// <summary>
/// 
/// </summary>
using System.IO;
using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;
static class DatabaseLayer
{
    internal static SQLiteConnection conn = null;
    static string sDB = AppData.ci.GetFile(AppFolders.Data, ThisApp.Name + ".db3");

    static DatabaseLayer()
    {
    }

    static void conn_Disposed(object sender, EventArgs e)
    {
        DatabaseLayer.LoadNewConnection();
    }

    static internal string ToBlob(byte[] ba)
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
    /// P�evedu �et�zec v hexadexim�ln� form�tu A1 na pole byt�. Pokud nebude hex form�t(nap��kal nebude m�t sud� po�et znak�), VV
    /// </summary>
    static internal byte[] FromBlob(string hexEncoded)
    {
        if (hexEncoded == null || hexEncoded.Length == 0)
        {
            return null;
        }
        try
        {
            hexEncoded = hexEncoded.Replace("X'", "").TrimEnd('\''); ;

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
            throw new System.FormatException("The provided string does not appear to be Hex encoded:" + Environment.NewLine + hexEncoded + Environment.NewLine, ex);
        }

    }

    static bool zaheslovat = false;

    internal static void LoadNewConnection()
    {
        if (!FS.ExistsFile(sDB))
        {
            string nad = Path.GetDirectoryName(sDB);
            FS.CreateFoldersPsysicallyUnlessThere(nad);
            Environment.CurrentDirectory = nad;
            string nazevSpustitelneExeDB = "sqlite3.exe";
            string sDbExe = FS.Combine(nad, nazevSpustitelneExeDB);
            if (!FS.ExistsFile(sDbExe))
            {
                //File.Copy(FS.Combine(slozkaAktualniVerze, nazevSpustitelneExeDB), 
                FS.CopyTo(FS.Combine(Application.StartupPath, nazevSpustitelneExeDB), nad);
            }

            // TODO: S uvozovkami se to zad�vat ned�, zjisti jak se zad�v� cesta k programu kdy� jsou v n� uvozovky
            string cmd = "" + nad + "\\sqlite3.exe" + "" + "";
            Process cess = Process.Start(cmd, Path.GetFileNameWithoutExtension(sDB));
            cess.Kill();
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + sDB + "");
            conn.Open();
            if (zaheslovat)
            {
                conn.ChangePassword("olsehheslo");
            }
        }
        else
        {
            //"Data Source=F:\Mona\sunamo\DocArch\Data\DocArch.db3;Version=3;Password=olsehheslo;"
            string cs = "Data Source=" + sDB + ";Version=3;";
            if (zaheslovat)
            {
                cs += "Password=olsehheslo;";
            }
            conn = new SQLiteConnection(cs);
            conn.Open();
            if (zaheslovat)
            {
                conn.ChangePassword("olsehheslo");
            }
        }

        conn.DefaultTimeout = 10000;
    }
}
