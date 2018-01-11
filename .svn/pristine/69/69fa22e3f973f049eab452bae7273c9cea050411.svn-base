public static class EnumToString
{
    public static string FromStatusOfUser(StatusOfUser s)
    {
        

        switch (s)
        {
            case StatusOfUser.Banned:
                return "Pozastaven bez možnosti přihlášení";
            case StatusOfUser.Allowed:
                return "Povolen";
            case StatusOfUser.Admin:
                return "Správce webu";
            default:
                //MailSender.SendEmailDebug("EnumToString", "FromStatusOfUser", "Nebyla implementována větev " + s.ToString());
                return SunamoStrings.NotImplementedPleaseContactWebAdmin;
        }
    }
}
