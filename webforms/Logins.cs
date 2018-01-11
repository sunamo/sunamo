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

namespace webforms
{
    public class LoginResponse
    {
        public LoginResponseType type = LoginResponseType.Alert;
        public string value = null;

        public LoginResponse(LoginResponseType type, string value)
        {
            this.type = type;
            this.value = value;
        }
    }

    public enum LoginResponseType
    {
        Redirect,
        Warning,
        Alert
    }

    public class Logins
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
        /// <param name="context"></param>
        /// <param name="zpravaVen"></param>
        /// <param name="idUsers"></param>
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
                zpravaVen = "Nejste přihlášený/á";
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

        public static LoginResponse LoginCommonAllPages(SunamoPage page, string login2, string heslo2, bool rememberUser2, bool rememberPass, string continueUri)
        {
            if (SunamoPageHelper.IsIpAddressRight(page) != null)
            {
                if (GeneralLayer.AllowedRegLogSys)
                {
                    //string dph = Session.SessionID ;
                    int idUser = -1;
                    bool pair = false;

                    login2 = login2.Trim();
                    heslo2 = heslo2.Trim();

                    //bool mail = false;
                    string login = GetLoginMaybeFromEmail(login2);
                    if (login != "" && (global::MSStoredProceduresI.ci.SelectExists(Tables.Users, "Login", login) || global::MSStoredProceduresI.ci.SelectExists(Tables.UsersActivates, "Login", login)))
                    {
                        if (login != "" && heslo2 != "")
                        {
                            if (global::MSStoredProceduresI.ci.SelectExists(Tables.UsersActivates, "Login", login))
                            {
                                return new LoginResponse(LoginResponseType.Warning, "Musíte si nejdříve svůj účet aktivovat. Chcete <a href=\"/Me/SendActivationEmailAgain.aspx?login=" + login + "\">znovu poslat</a> aktivační email?");
                            }

                            DateTime dt = DateTime.Today;
                            dt = dt.AddHours(DateTime.Now.Hour);
                            byte pocetPokusu = global::MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.LoginAttempt, "Count", AB.Get("Login", login), AB.Get("DT", dt));
                            if (pocetPokusu < GeneralConsts.maxPocetPokusu)
                            {
                                if (pair = Logins.PairLoginPassword(login, heslo2, out idUser))
                                {
                                    string email = global::MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.UsersReactivates, "Email", CA.ToArrayT<AB>(AB.Get("IDUsers", idUser)), CA.ToArrayT<AB>(AB.Get("Code", "")));
                                    if (email != "")
                                    {
                                        DateTime dateChanged = global::MSStoredProceduresI.ci.SelectCellDataTableDateTimeOneRow(Tables.UsersReactivates, "DateChanged", global::MSStoredProceduresI.DateTimeMinVal, CA.ToArrayT<AB>(AB.Get("IDUsers", idUser)), CA.ToArrayT<AB>(AB.Get("Code", "")));
                                        return new LoginResponse(LoginResponseType.Warning, "Musíte si nejdříve svůj účet reaktivovat, protože u vašeho účtu byl změnen email před " + DTHelper.CalculateAgeAndAddRightStringKymCim(dateChanged, true, Langs.cs, global::MSStoredProceduresI.DateTimeMinVal) + " na " + GeneralCells.EmailOfUser(idUser) + ", uvedený jako nový email. Přejete si jej <a href=\"" + web.UH.GetWebUri(page, "Me/SendReactivationEmail.aspx?uid=" + idUser) + "\">poslat znovu</a>?");
                                    }

                                    string sc = "";
                                    string scAktual = TableRowSessions3.GetSessionIDOrSE(idUser);
                                    if (scAktual != "")
                                    {
                                        sc = scAktual;
                                    }
                                    else
                                    {
                                        sc = Logins.CreateSc();
                                    }
                                    bool autoLogin = rememberPass;
                                    bool rememberUser = rememberUser2;
                                    SessionManager.LoginUser(page, login, idUser, sc);

                                    MasterPageHelper.GetSmp(page).DoLogin(login, idUser, sc, rememberPass, rememberUser2);

                                    if (string.IsNullOrWhiteSpace(continueUri))
                                    {
                                        continueUri = Consts.HttpWwwCzSlash;
                                    }

                                    return new LoginResponse(LoginResponseType.Redirect, continueUri);
                                }
                                else
                                {

                                    string z = UnsuccessfulLoginAptempt(login, pocetPokusu);
                                    return new LoginResponse(LoginResponseType.Warning, "Špatná kombinace uživatelského jména a hesla. Chcete se <a href=\"Register.aspx?ReturnUrl=" + UH.UrlEncode(continueUri) + "\">registrovat</a>? " + z);
                                    //Warning("Špatná kombinace uživatelského jména a hesla. Chcete se <a href=\"Register.aspx?ReturnUrl=" + UH.UrlEncode(ContinueUri.Value) + "\">registrovat</a>?");
                                }
                            }
                            else
                            {
                                return new LoginResponse(LoginResponseType.Warning, "Další pokusy o přihlášení budete mít povoleny až za hodinu");
                            }
                        }
                        else
                        {
                            return new LoginResponse(LoginResponseType.Warning, "Uživatelské jméno ani heslo nemůže zůstat prázdné.");
                        }
                    }
                    else
                    {
                        return new LoginResponse(LoginResponseType.Warning, "Nezadali jste uživatele nebo zadaný uživatel není v tabulce aktivovaných uživatelů. ");
                    }
                }
                else
                {
                    return new LoginResponse(LoginResponseType.Alert, Logins.GetRegLogSysStatus());
                    //JavascriptInjection.alert(Logins.GetRegLogSysStatus(), Page);
                }
            }
            else
            {
                return new LoginResponse(LoginResponseType.Warning, SunamoStrings.YouHaveNotValidIPv4Address);
            }
        }
        /// <summary>
        /// Vrátí mi login z loginu nebo mailu
        /// </summary>
        /// <param name="login2"></param>
        /// <returns></returns>
        public static string GetLoginMaybeFromEmail(string login2)
        {
            string login = "";
            if (login2.Contains('@'))
            {
                login2 = login2.ToLower();
                if (global::MSStoredProceduresI.ci.SelectExists(Tables.UsersActivates, "Email", login2))
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
                z = " Už vám nezbývají žádné pokusy o přihlášení, zkuste to znovu za hodinu. ";
            }
            else
            {
                z = "Už vám zbývá jen " + zbyva + " pokusů o přihlášení. ";
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
        /// <param name="idUsers"></param>
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
        /// <param name="login"></param>
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
            if (CA.AreTheSame(p, p2))
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
                if (CA.AreTheSame(p, p2))
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
        /// <param name="hashHeslaASoli"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string GetIndexesOfHash(byte[] hashHeslaASoli, byte[] salt)
        {
            byte[] hashDoDPH = HashHelper.GetHash(hashHeslaASoli, salt);
            string inBase64 = Convert.ToBase64String(hashDoDPH);
            string dph = SH.GetOddIndexesOfWord(inBase64);
            return dph;
        }
    }
}
