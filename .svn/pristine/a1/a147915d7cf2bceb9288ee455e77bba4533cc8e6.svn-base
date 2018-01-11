public class LoginCookie
{
    public string login = "";
    public string sc = "";
    public int idUser = -1;
    /// <summary>
    /// Vrátím -1 pokud už. nebude přihlášen, nebo pokud nebude souhlasit dph(pozměněná sesssion/cookies)
    /// </summary>
    public int id
    {
        get
        {
            if (GeneralLayer.AllTablesOk)
            {
                
                if (login != null)
                {
                    if (!string.IsNullOrEmpty(sc))
                    {
                        int loginID = idUser;
                        if (loginID == -1)
                        {
                            loginID = idUser = GeneralCells.IDOfUser_Login(login);
                        }


                        if (TableRowSessions3.GetSessionIDOrSE(loginID) == sc) //FileBasedTable.ExistsStf(Tables.Sessions, Columns.SessionID, loginID))
                        {
                            return loginID;
                        }
                        else
                        {
                            return -1;
                        }

                        
                    }
                }
            }
            return -1;
        }
    }
}
