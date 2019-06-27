using sunamo;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

public partial class Logins
{
    public static bool OccupateLogin(string kind, string login)
    {
        bool vr = global::MSStoredProceduresI.ci.SelectCellDataTableObjectOneRow(Tables.Users, kind, login, "ID") != null;
        if (!vr)
        {
            vr = global::MSStoredProceduresI.ci.SelectCellDataTableObjectOneRow(Tables.UsersActivates, kind, login, "ID") != null;
        }

        return vr;
    }

    /// <summary>
    /// Po volání této metody si můžeš jednoduše zjistit ID mládežníka například voláním CasdMladezCells.IDOfYouths_IDUsers(A3)
    /// </summary>
    /// <param name = "context"></param>
    /// <param name = "zpravaVen"></param>
    /// <param name = "idUsers"></param>
    /// <returns></returns>
    public static bool IsLoginedOK(Page context, out string zpravaVen, out int idUsers)
    {
        zpravaVen = "";
        object sidUsers = context.Session["login"];
        idUsers = GeneralCells.IDOfUser_Login(sidUsers.ToString());
        if (idUsers != -1)
        {
            if (context.Session.SessionID == TableRowSessions3.GetSessionID(idUsers))
            {
                return true;
            }
            else
            {
                zpravaVen = "ID Session nalezené v DB nesouhlasí v vaší ID Session, popř. se odhlašte a přihlašte";
                return false;
            }
        }
        else
        {
            zpravaVen = "Nejste přihlášený/" + "";
            return false;
        }

        return false;
    }

    /// <summary>
    /// Vytvoří 24 znakový náhodný řetězec složený z malých písmen a číslic
    /// </summary>
    /// <returns></returns>
    public static string CreateSc()
    {
        SessionIDManager manager = new SessionIDManager();
        return manager.CreateSessionID(HttpContext.Current);
    }

    /// <summary>
    /// Vrátí mi login z loginu nebo mailu
    /// </summary>
    /// <param name = "login2"></param>
    /// <returns></returns>
    public static string GetLoginMaybeFromEmail(string login2)
    {
        string login = "";
        if (login2.Contains('@'))
        {
            login2 = login2.ToLower();
            if (MSStoredProceduresI.ci.SelectExists(Tables.UsersActivates, "Email", login2))
            {
                login = GeneralCells.LoginOfUsersActivates_Email(login2);
            }
            else
            {
                login = GeneralCells.LoginOfUser_Email(login2);
            }
            //mail = true;
        }
        else
        {
            login = login2;
        }

        return login;
    }

    public static string UnsuccessfulLoginAptempt(string login, byte pocetPokusu)
    {
        DateTime actualHour = DateTime.Today;
        actualHour = actualHour.AddHours(DateTime.Now.Hour);
        if (pocetPokusu == 0)
        {
            TableRowLoginAttempt la = new TableRowLoginAttempt(login, actualHour, 1);
            la.InsertToTable2();
        }
        else
        {
            global::MSStoredProceduresI.ci.UpdatePlusIntValue(Tables.LoginAttempt, "Count", 1, AB.Get("Login", login), AB.Get("DT", actualHour));
        }

        pocetPokusu++;
        int zbyva = GeneralConsts.maxPocetPokusu - pocetPokusu;
        string z = "";
        if (zbyva == 0)
        {
            z = " " + "Už vám nezbývají žádné pokusy o přihlášení, zkuste to znovu za hodinu" + ". ";
        }
        else
        {
            z = "Už vám zbývá jen" + " " + zbyva + " " + "pokusů o přihlášení" + ". ";
        }

        return z;
    }

    public static string GenerateRandomLogin(string jmeno, string value)
    {
        return SH.TextWithoutDiacritic(jmeno.Substring(0, 2) + value.Substring(0, 2) + RandomHelper.RandomInt(1000, 9999));
    }

    /// <summary>
    /// POkud uživatel s ID A1 nebude nalezen, G SE
    /// G SE, když 
    /// </summary>
    /// <param name = "idUsers"></param>
    /// <returns></returns>
    public static string GetIndexesOfHash(int idUsers)
    {
        if (idUsers != -1)
        {
            byte[] hash = TableRowUsers3.GetHash(idUsers);
            if (hash == null)
            {
                return "";
            }

            byte[] salt = TableRowUsers3.GetSalt(idUsers);
            return GetIndexesOfHash(hash, salt); //SH.GetOddIndexesOfWord(Encoding.UTF8.GetString(hash));
        }

        return "";
    }

    /// <summary>
    /// Pozor, změna, pokud uživatel s tímto loginem nebude nalezen, nevrátí se null, ale SE
    /// </summary>
    /// <param name = "login"></param>
    /// <returns></returns>
    public static string GetIndexesOfHash(string login)
    {
        //byte[] hash = (byte[])MSStoredProceduresI.ci.SelectCellDataTableObjectOneRow(Tables.Users, "Login", login, "Hash");
        return GetIndexesOfHash(GeneralCells.IDOfUser_Login(login));
    }

    public static bool PairLoginPassword(int userID, string Password)
    {
        byte[] p = TableRowUsers3.GetHash(userID);
        byte[] salt = TableRowUsers3.GetSalt(userID);
        byte[] p2 = HashHelper.GetHash(Encoding.UTF8.GetBytes(Password), salt);
        if (CA.IsTheSame<byte>(p, p2))
        {
            return true;
        }

        return false;
    }

    public static bool PairLoginPassword(string Login, string Password, out int userID)
    {
        userID = GeneralCells.IDOfUser_Login(Login);
        //object[] dt=  MSStoredProceduresI.ci.SelectDataTableOneRow(Tables.Users, "Login", Login);
        if (userID != -1)
        {
            byte[] p = TableRowUsers3.GetHash(userID);
            byte[] salt = TableRowUsers3.GetSalt(userID);
            byte[] p2 = HashHelper.GetHash(Encoding.UTF8.GetBytes(Password), salt);
            if (CA.IsTheSame<byte>(p, p2))
            {
                return true;
            }
        }

        //Fce = 1;
        return false;
    }

    public static string GetRegLogSysStatus()
    {
        if (GeneralLayer.AllowedRegLogSys)
        {
            return "OK";
        }
        else
        {
            return GeneralLayer.RegLogSysStatus;
        }
    }

    /// <summary>
    /// A1 se může klidně nahradit za TableRowUsers3.GetHash();
    /// </summary>
    /// <param name = "hashHeslaASoli"></param>
    /// <param name = "salt"></param>
    /// <returns></returns>
    public static string GetIndexesOfHash(byte[] hashHeslaASoli, byte[] salt)
    {
        byte[] hashDoDPH = HashHelper.GetHash(hashHeslaASoli, salt);
        string inBase64 = Convert.ToBase64String(hashDoDPH);
        string dph = SH.GetOddIndexesOfWord(inBase64);
        return dph;
    }
}