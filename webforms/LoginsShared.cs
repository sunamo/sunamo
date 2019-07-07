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
                            return new LoginResponse(LoginResponseType.Warning, "Musíte si nejdříve svůj účet aktivovat. Chcete <a href=\\\\\\\\\"/Me/SendActivationEmailAgain.aspx?login=" + login + "\\\\\\\\\">znovu poslat</a> aktivační email?");
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
                                    DateTime dateChanged = global::MSStoredProceduresI.ci.SelectCellDataTableDateTimeOneRow(Tables.UsersReactivates, "DateChanged", global::SqlServerHelper.DateTimeMinVal, CA.ToArrayT<AB>(AB.Get("IDUsers", idUser)), CA.ToArrayT<AB>(AB.Get("Code", "")));
                                    return new LoginResponse(LoginResponseType.Warning, "Musíte si nejdříve svůj účet reaktivovat, protože u vašeho účtu byl změnen email před" + " " + DTHelper.CalculateAgeAndAddRightStringKymCim(dateChanged, true, Langs.cs, global::SqlServerHelper.DateTimeMinVal) + " na " + GeneralCells.EmailOfUser(idUser) + ", uvedený jako nový email. Přejete si jej <a href=\\\\\\\\\"" + UA.GetWebUri(page, "Me/SendReactivationEmail.aspx?uid=" + idUser) + "\\\\\\\\\">poslat znovu</a>?");
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
                                var v = MasterPageHelper.GetSmp(page);
                                v.DoLogin(login, idUser, sc, rememberPass, rememberUser2);
                                if (string.IsNullOrWhiteSpace(continueUri))
                                {
                                    continueUri = Consts.HttpWwwCzSlash;
                                }

                                return new LoginResponse(LoginResponseType.Redirect, continueUri);
                            }
                            else
                            {
                                string z = UnsuccessfulLoginAptempt(login, pocetPokusu);
                                return new LoginResponse(LoginResponseType.Warning, "Špatná kombinace uživatelského jména a hesla. Chcete se <a href=\\\\\\\\\"Register.aspx?ReturnUrl=" + UH.UrlEncode(continueUri) + "\\\\\\\\\">registrovat</a>? " + z);
                                //Warning("Špatná kombinace uživatelského jména a hesla. Chcete se <a href=\\\\\\\\\"Register.aspx?ReturnUrl=" + UH.UrlEncode(ContinueUri.Value) + "\\\\\\\\\">registrovat</a>?");
                            }
                        }
                        else
                        {
                            return new LoginResponse(LoginResponseType.Warning, "Další pokusy o přihlášení budete mít povoleny až za hodinu");
                        }
                    }
                    else
                    {
                        return new LoginResponse(LoginResponseType.Warning, "Uživatelské jméno ani heslo nemůže zůstat prázdné" + ".");
                    }
                }
                else
                {
                    return new LoginResponse(LoginResponseType.Warning, "Nezadali jste uživatele nebo zadaný uživatel není v tabulce aktivovaných uživatelů" + ". ");
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
}