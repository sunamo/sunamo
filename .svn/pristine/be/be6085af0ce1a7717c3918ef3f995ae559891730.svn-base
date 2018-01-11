public  class UserContactInfo
{
    public string Email = null;
    public MailSettings MailSettings = MailSettings.NoMail;
    //TelNumber,PublishPhone,
    public static string columnsToGet = "Email,MailSettings";

    public UserContactInfo(object[] o)
    {
        Email = MSTableRowParse.GetString(o, 0);
        MailSettings = (MailSettings)MSTableRowParse.GetByte(o, 1);
    }
}
