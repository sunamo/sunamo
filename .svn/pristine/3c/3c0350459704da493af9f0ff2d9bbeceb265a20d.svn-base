using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for PrihlasenyUzivatel
/// </summary>
public class LoginedUser
{
    public string login = null;
    /// <summary>
    /// Vždy výsledek metody SH.GetOddIndexesOfWord
    /// Už ji nikde nidky nevyužívej, a u GGdag webu ji pouze oprav 
    /// </summary>
    public string sc = null;

	public LoginedUser()
	{
	}

    /// <summary>
    /// Po použití této stránky není již nutné kontrolvat zda SessionID není SE/null ani zda pasuje k přihlášenému uživateli - to všechno se děje již v této metodě
    /// Hodí se při provádění delších operací, při kterých hrozí že vyprší session - tato metoda totiž kontroluje session a v případě že vyprší se přihlásí znovu z cookies
    /// V případě že cokoliv selže vrátí -1
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    public int ID(SunamoPage sp)
    {
        LoginCookie lc = null;
        
        lc = SunamoMasterPage.GetLoginCookie(sp.Request);

        return ID(lc, sp.idLoginedUser, null, sp);
    }

    public int ID(SunamoMasterPage smp)
    {
        SunamoPage sp = (SunamoPage)smp.Page;
        LoginCookie lc = null;

        lc = SunamoMasterPage.GetLoginCookie(sp.Request);

        return ID(lc, sp.idLoginedUser, null, sp);
    }

    /// <summary>
    /// Po použití této stránky není již nutné kontrolvat zda SessionID není SE/null ani zda pasuje k přihlášenému uživateli - to všechno se děje již v této metodě
    /// Experimentální podpora pro funkčnost metody .ID() i s objektem HttpContext, ne jen SunamoPage
    /// Nevím jestli bude fungovat
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    public int ID(HttpContext sp)
    {
        LoginCookie lc = null;
        lc = HttpHandlerHelper.GetLoginCookie(sp.Request);
        return ID(lc, -1, sp, null);
    }

    private int ID(LoginCookie lc, int idLoginedUser, HttpContext con, SunamoPage sp)
    {
        if (GeneralLayer.AllTablesOk)
        {
            if (GeneralLayer.AllowedRegLogSys)
            {
                if (idLoginedUser != -1)
                {
                    return idLoginedUser;
                }
                
                if (login == null)
                {
                    if (lc != null)
                    {
                        login = lc.login;
                    }
                }
                if (string.IsNullOrEmpty(sc))
                {
                    if (lc != null)
                    {
                        sc = lc.sc;
                    }
                }
                if (login != null)
                {
                    int loginID = GeneralCells.IDOfUser_Login(login);
                    if (loginID != -1)
                    {
                        if (!string.IsNullOrEmpty(sc))
                        {
                            if (sc == TableRowSessions3.GetSessionIDOrSE(loginID))
                            {
                                if (sp != null)
                                {
                                    SunamoMasterPage smp = (SunamoMasterPage)sp.Master;
                                    //smp.WritePernamentCookie()
                                    SessionManager.LoginUser(smp, login, loginID, sc);
                                }
                                else if (con != null)
                                {
                                    SessionManager.LoginUser(con, login, loginID, sc);
                                }
                                else
                                {
                                    throw new Exception("Do metody LoginedUser.ID() se nedostal ani parametr HttpContext, ani SunamoPage.");
                                }
                                
                                return loginID;
                            }
                            else
                            {
                                if (loginID == -1)
                                {
                                    if (sp != null)
                                    {
                                        SunamoMasterPage smp = (SunamoMasterPage)sp.Master;
                                        SessionManager.LogoutUser(smp);
                                        smp.Logout();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return -1;
    }

    /// <summary>
    /// Vrátím -1 pokud už. nebude přihlášen, nebo pokud nebude souhlasit dph(pozměněná sesssion/cookies)
    /// </summary>
}
