/// <summary>
/// 
/// </summary>
using System.IO;
public class TableRowUsers3
{
    public static byte[] GetHash(int idUser)
    {
        return FileBasedTable.GetSbf(Tables.Users, "Hash", idUser);
    }

    public static void SetHash(int idUser, byte[] value)
    {
        FileBasedTable.SetSbf(Tables.Users, "Hash", idUser, value);
    }

    public static byte[] GetSalt(int idUser)
    {
        return FileBasedTable.GetSbf(Tables.Users, "Salt", idUser);
    }

    public static void SetSalt(int idUser, byte[] value)
    {
        FileBasedTable.SetSbf(Tables.Users, "Salt", idUser, value);
    }

    public static void UpdateInTable(TableRowUsersBase b)
    {
        //"TelNumber", b.TelNumber, "PublishPhone", b.PublishPhone,"CanReset", b.CanReset,
        MSStoredProceduresI.ci.UpdateValuesCombination(Tables.Users, "ID", b.ID, "SessionID", b.SessionID, "Login", b.Login, "Email", b.Email, "LastSeen", b.LastSeen, "SecQue", b.SecQue, "SecAns", b.SecAns,  "Sex", b.Sex, "DateBorn", b.DateBorn, "MailSettings", b.MailSettings, "IDState", b.IDState, "IDRegion", b.IDRegion, "IDDistrict", b.IDDistrict, "IDCity", b.IDCity, "Status", b.Status, "BlockNewComment", b.BlockNewComment, "FacebookNick", b.FacebookNick, "TwitterNick", b.TwitterNick, "GoogleNick", b.GoogleNick, "WidthUserPage", b.WidthUserPage, "HtmlAbout", b.HtmlAbout);
    }
}
