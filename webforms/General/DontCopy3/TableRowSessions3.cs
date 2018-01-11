using webforms;

public class TableRowSessions3
{
    public static void SetSessionID(int userID, string sessionID)
    {
        if (userID == -1)
        {
            MSStoredProceduresI.ci.Update(Tables.Users, "SessionID", "", "ID", userID);
        }
        else
        {
            //FileBasedTable.SetStf(Tables.Sessions, "SessionID", userID, sessionID);
            MSStoredProceduresI.ci.Update(Tables.Users, "SessionID", sessionID, "ID", userID);
        }
    }

    /// <summary>
    /// Vrátí náhodné sc pokud nebude sc pro uživatele A1 v DB nalezeno
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public static string GetSessionID(int userID)
    {
        string sessionID = GetSessionIDOrSE(userID);
        if (sessionID == "")
        {
            // Toto je obrana aby někdo nebyl odhlášen a jeho SC tak nebylo "" a někdo do handleru nezadal prázdné SC a to ho nepustilo dál
            return Logins.CreateSc();
        }
        return sessionID;
    }

    /// <summary>
    /// Používá se hlavně v ashx. Před jejím použitím musíš zkontrolovat zda sc, se kterým budeš výstup této metody kontrolovat, není null ani SE
    /// </summary>
    /// <param name="idUser"></param>
    /// <returns></returns>
    public static string GetSessionIDOrSE(int userID)
    {
        //return FileBasedTable.GetStf(Tables.Sessions, "SessionID", idUser);
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.Users, "SessionID", "ID", userID);
    }

}
