using System;
public static class GeneralCells
{
    
    /// <summary>
    /// Vrátí -1 pokud takový login nenalezne
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public static int IDOfUser_Login(string login)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(false,Tables.Users,  "ID", "Login", login);
    }

    public static string NameOfRegion(short idRegion)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.Region, "Name", "ID", idRegion);
    }

    public static byte IDOfTypeOfContact_Name(string p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.TypesOfContacts, "Name", p, "ID");
    }

    public static DateTime DateBornOfUser(int p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableDateTimeOneRow(Tables.Users, "DateBorn", "ID", p, MSStoredProceduresI.DateTimeMinVal);
    }

    public static string NameOfTypeOfContacts(byte p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.TypesOfContacts, "Name", "ID", p);
    }

    public static int IDOfUser_Email(string p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(false,Tables.Users, "ID", "Email", p);
    }

    /// <summary>
    /// V případě nenalezení vrátí -1
    /// </summary>
    /// <param name="ext"></param>
    /// <returns></returns>
    public static short IDOfFileExts_Ext(string ext)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableShortOneRow(false,Tables.FileExts, "Ext", ext, "ID");
    }

    public static string ExtOfFileExts(short pripona)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.FileExts, "Ext", "ID", pripona);
    }

    public static string LoginOfUser(int p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.Users, "Login", "ID", p);
    }

    public static string LoginOfUser_Email(string p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.Users, "Login", "Email", p);
    }

    public static string EmailOfUser(int p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.Users, "Email", "ID", p);
    }

    public static int IDUsersOfSurvey(short idSurvey)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(false,Tables.Wid_Polls, "IDUsers", "ID", idSurvey);
    }

    public static string EmailOfUser_Login(string login)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.Users, "Email", "Login", login);
    }

    public static byte OverallAnswersOfSurvey(short idSurvey)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.Wid_Polls, "ID", idSurvey, "OverallAnswers");
    }

    /// <summary>
    /// V případě nenalezení vrátí -1
    /// </summary>
    /// <param name="idSurveyAnswer"></param>
    /// <returns></returns>
    public static short IDSurveyOfSurveyAnswer(int idSurveyAnswer)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableShortOneRow(false,Tables.Wid_PollsAnswers, "ID", idSurveyAnswer, "IDSurvey");
    }

    public static string NameOfSurveyAnswer(int idSurveyAnswer)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.Wid_PollsAnswers, "Name", "ID", idSurveyAnswer);
    }

    public static DateTime DateExpiredOfSurvey(short idSurvey)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableDateTimeOneRow(Tables.Wid_Polls, "DateExpired", "ID", idSurvey, MSStoredProceduresI.DateTimeMinVal);
    }

    public static int IDUsersOfAnswer(int idAnswer)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(false,Tables.Wid_PollsAnswers,  "IDUsers", "ID", idAnswer);
    }

    public static int IDOfPageName_Name(string stranka)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageName,  "ID", "Name", stranka);
    }

    public static int IDOfPageArgument_Arg(string args)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageArgument,  "ID", "Arg", args);
    }

    public static string ArgOfPageArgument(int p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.PageArgument, "Arg", "ID", p);
    }

    public static int IDPageArgOfPageOld(int item)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld, "IDPageArg", "IDPage", item);
    }

    public static int IDPageNameOfPageOld(int item)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld, "IDPageName", "IDPage", item);
    }

    public static string NameOfPageName(int idName)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.PageName, "Name", "ID", idName);
    }

    public static bool SexOfUser(int idUsers)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableBoolOneRow(Tables.Users, "ID", idUsers, "Sex");
    }

    public static int MailSettingsOfUser(int idUsers)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.Users, "ID", idUsers, "MailSettings");
    }

    public static string LoginOfUsersActivates_Email(string email)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.UsersActivates, "Login", "Email", email);
    }

    public static bool AllowNewCommentsOfPage(int idPage)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableBoolOneRow(Tables.Page, "ID", idPage, "AllowNewComments");
    }

    public static string CodeOfUsersReactivates(int idUser)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.UsersReactivates, "Code", "IDUsers", idUser);
    }

    public static bool IsBlockedOfIPAddress(int idIp)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableBoolOneRow(Tables.IPAddress, "ID", idIp, "IsBlocked");
    }

    public static bool BlockNewCommentOfUser(int idUser)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableBoolOneRow(Tables.Users, "ID", idUser, "BlockNewComment");
    }

    public static bool IsOldOfPage(int p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableBoolOneRow(Tables.Page, "ID", p, "IsOld");
    }

    public static string HtmlAboutOfUser(int idUser)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.Users, "HtmlAbout", "ID", idUser);
    }

    public static short WidthUserPage(int p)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableShortOneRow(false, Tables.Users, "ID", p, "WidthUserPage");
    }

    public static byte IDTableOfPageNew(int idPage)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.PageNew, "IDPage", idPage, "IDTable");
    }

    public static int IDItemOfPageNew(int idPage)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageNew, "IDItem", "IDPage", idPage);
    }

    public static byte IDWebOfPageOld(int idPage)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.PageOld, "IDPage", idPage, "IDWeb");
    }

    public static int OverallCountOfPage(int nejvyssiIDPage)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.Page, "OverallViews", "ID", nejvyssiIDPage);
    }

    public static byte IDOfState(string venueLocationCountry)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.State, "Name", venueLocationCountry, "ID");
    }
}
