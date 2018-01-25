using System;
using System.Linq;
using System.Net.Mail;

namespace sunamo
{
    public class GoogleAppsMailbox
    {
        /// <summary>
        /// Řetězec, který se objeví u příjemce jako odesílatel. Nemusí to být mailová adresa.
        /// </summary>
        string fromName = null;
        /// <summary>
        /// Povinný. Celá adresa emailu který jste si nastavili na https://ks.aspone.cz/ 
        /// </summary>
        string userName = null;
        /// <summary>
        /// Povinný. Heslo k mailu userName, které se taktéž nastavuje na https://ks.aspone.cz/
        /// </summary>
        string password = "4W6k4?MLja";
        public string mailOfAdmin = null;



        /// <summary>
        /// Do A3 se ve výchozí stavu předává GeneralCells.EmailOfUser(1)
        /// </summary>
        /// <param name="fromName"></param>
        /// <param name="userName"></param>
        /// <param name="mailOfAdmin"></param>
        public GoogleAppsMailbox(string fromName, string userName, string mailOfAdmin)
        {
            this.fromName = fromName;
            this.userName = userName;
            this.mailOfAdmin = mailOfAdmin;
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
        public string SendEmail(string to, string cc, string bcc, string replyTo, string subject, string htmlBody, params string[] attachments)
        {
            string emailStatus = string.Empty;

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true; //Mail aspone nefunguje na SSL zatím, pokud byste zde dali true, tak vám vznikne výjimka se zprávou Server does not support secure connections.
            client.Credentials = new System.Net.NetworkCredential(userName, password);
            //client.Port = 587; //Fungovalo mi to když jsem žádný port nezadal a jelo mi to na výchozím
            client.Host = "smtp.gmail.com"; //Adresa smtp serveru. Může končit buď na název vašeho webu nebo na aspone.cz. Zadává se bez protokolu, jak je zvykem
            MailMessage mail = new MailMessage();

            MailAddress ma = new MailAddress(userName, fromName);
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
            if (to.Contains(";"))
            {
                string[] _EmailsTO = to.Split(";".ToCharArray());
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
            if (cc.Contains(";"))
            {
                string[] _EmailsCC = cc.Split(";".ToCharArray());
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
            if (bcc.Contains(";"))
            {
                string[] _EmailsBCC = bcc.Split(";".ToCharArray());
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
            mail.Body = htmlBody;
            mail.IsBodyHtml = true;

            foreach (var item in attachments)
            {
                if (System.IO.File.Exists(item))
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
