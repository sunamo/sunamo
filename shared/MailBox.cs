using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MailBox
{ 
    #region V každé třídě MailSender
    public static GoogleAppsMailbox mb = null;

    public static string SendEmail(string to, string cc, string bcc, bool a1AsReplyTo, string subject, string htmlBody, params string[] attachments)
    {
        string replyTo = "";
        if (a1AsReplyTo)
        {
            replyTo = to;
        }

        return mb.SendEmail(to, cc, bcc, replyTo, subject, htmlBody, true, attachments);
    }

    public static string SendEmail(string to, string cc, string bcc, string replyTo, string subject, string htmlBody, params string[] attachments)
    {
        return mb.SendEmail(to, cc, bcc, replyTo, subject, htmlBody, true, attachments);
    }
    #endregion
}