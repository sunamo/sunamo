using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Data
{
    public class SendMailData<T>
    {
        public string to = null;
        public string cc = null;
        public string bcc = null;
        public string replyTo = null;
        public string subject = null;
        public string htmlBody = null;
        public string[] attachments = null;
        T resultOfMethod = default(T);
        public T ResultOfMethod
        {
            get
            {
                return resultOfMethod;
            }
        }

        public SendMailData(T resultOfMethod, string to, string cc, string bcc, string replyTo, string subject, string htmlBody, params string[] attachments)
        {
            this.resultOfMethod = resultOfMethod;
            this.to = to;
            this.cc = cc;
            this.bcc = bcc;
            this.replyTo = replyTo;
            this.subject = subject;
            this.htmlBody = htmlBody;
            this.attachments = attachments;
        }
    }
}
