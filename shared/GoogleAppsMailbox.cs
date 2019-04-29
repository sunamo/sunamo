using System;
using System.Linq;
using System.Net.Mail;

namespace sunamo
{
    public class GoogleAppsMailbox
    {
        public const string noreply_scz_mail = "noreply@sunamo.cz";

        /// <summary>
        /// Řetězec, který se objeví u příjemce jako odesílatel. Nemusí to být mailová adresa.
        /// </summary>
        public string fromName = null;
        /// <summary>
        /// Povinný. Celá adresa emailu který jste si nastavili na https://ks.aspone.cz/ 
        /// </summary>
        public string fromEmail = null;
        /// <summary>
        /// Povinný. Heslo k mailu userName, které se taktéž nastavuje na https://ks.aspone.cz/
        /// </summary>
        string password = null;
        public string mailOfAdmin = null;

        public string Password
        {
            set
            {
                password = value;
            }
        }

        public static GoogleAppsMailbox noreply_scz = new GoogleAppsMailbox(noreply_scz_mail, noreply_scz_mail, null, AppData.ci.GetCommonSettings(CommonSettingsKeys.noreply_scz));
        public static GoogleAppsMailbox rj_scz = new GoogleAppsMailbox("Radek Jančík", "radek.jancik@sunamo.cz", null, AppData.ci.GetCommonSettings(CommonSettingsKeys.rj_scz));


        /// <summary>
        /// For sending from noreply@sunamo.cz
        /// </summary>
        public GoogleAppsMailbox()
        {

        }

        /// <summary>
        /// Do A3 se ve výchozí stavu předává GeneralCells.EmailOfUser(1). Can be null, its used in scz to send mails to webmaster
        /// Dont forget set password for A2 or use without-parametric ctor
        /// </summary>
        /// <param name="fromName"></param>
        /// <param name="fromEmail"></param>
        /// <param name="mailOfAdmin"></param>
        public GoogleAppsMailbox(string fromName, string fromEmail, string mailOfAdmin, string password = null)
        {
            this.fromName = fromName;
            this.fromEmail = fromEmail;
            this.mailOfAdmin = mailOfAdmin;
            this.password = password;
        }

        /// <summary>
        /// Do A1, A2, A3 se může zadat více adres, stačí je oddělit středníkem
        /// A4 nastav na "", pokud chceš použít jako reply-to adresu A1
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="htmlBody"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public string SendEmail(string to, string cc, string bcc, string replyTo, string subject, string body, bool htmlBody, params string[] attachments)
        {
            string emailStatus = string.Empty;

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true; //Mail aspone nefunguje na SSL zatím, pokud byste zde dali true, tak vám vznikne výjimka se zprávou Server does not support secure connections.
            client.Credentials = new System.Net.NetworkCredential(fromEmail, password);
            client.Port = 587; //Fungovalo mi to když jsem žádný port nezadal a jelo mi to na výchozím
            client.Host = "smtp.gmail.com"; //Adresa smtp serveru. Může končit buď na název vašeho webu nebo na aspone.cz. Zadává se bez protokolu, jak je zvykem
            MailMessage mail = new MailMessage();

            MailAddress ma = new MailAddress(fromEmail, fromName);
            mail.From = ma;
            if (replyTo == "")
            {
                MailAddress ma2 = new MailAddress(to, to);
                mail.ReplyToList.Add(ma2);
            }
            else
            {
                mail.ReplyToList.Add(ma);
            }
            mail.Sender = ma;

            #region Recipient
            if (to.Contains(AllStrings.sc))
            {
                string[] _EmailsTO = to.Split(AllStrings.sc.ToCharArray());
                for (int i = 0; i < _EmailsTO.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(_EmailsTO[i]))
                    {
                        mail.To.Add(new MailAddress(_EmailsTO[i]));
                    }
                }
                if (mail.To.Count == 0)
                {
                    emailStatus = "error: Nebyl zadán primární příjemce zprávy. ";
                    return emailStatus;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(to))
                {
                    mail.To.Add(new MailAddress(to));
                }
                else
                {
                    emailStatus = "error: Nebyl zadán primární příjemce zprávy. ";
                    return emailStatus;
                }
            }
            #endregion

            #region Carbon copy
            if (cc.Contains(AllStrings.sc))
            {
                string[] _EmailsCC = cc.Split(AllStrings.sc.ToCharArray());
                for (int i = 0; i < _EmailsCC.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(_EmailsCC[i]))
                    {
                        mail.CC.Add(new MailAddress(_EmailsCC[i]));
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(cc))
                {
                    mail.CC.Add(new MailAddress(cc));
                }
                else
                {
                    // Neděje se nic, prostě uživatel nic nezadal
                }
            }
            #endregion

            #region Blind Carbon copy
            //BCC
            if (bcc.Contains(AllStrings.sc))
            {
                string[] _EmailsBCC = bcc.Split(AllStrings.sc.ToCharArray());
                for (int i = 0; i < _EmailsBCC.Length; i++)
                {
                    mail.Bcc.Add(new MailAddress(_EmailsBCC[i]));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(bcc))
                {
                    mail.Bcc.Add(new MailAddress(bcc));
                }
            }
            #endregion

            mail.Subject = subject;
            mail.Body =  body;
            mail.IsBodyHtml = htmlBody;

            foreach (var item in attachments)
            {
                if (FS.ExistsFile(item))
                {
                    mail.Attachments.Add(new Attachment(item));
                }
            }

            try
            {
                client.Send(mail);
                mail.Dispose();
                mail = null;
                emailStatus = "success";
            }
            catch (Exception ex)
            {
                emailStatus = "error: ";
                if (ex.Message != null)
                {
                    emailStatus += ex.Message + ". ";
                }
                return emailStatus;
            }

            return emailStatus;
        }
    }
}
