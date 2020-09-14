using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SmtpServerData
{
    public string smtpServer { get; set; } = "smtp.gmail.com";
    public int port { get; set; } = 587;

    public static SmtpServerData Gmail()
    {
        var s = new SmtpServerData();
        s.port = 587;
        s.smtpServer = "smtp.gmail.com";
        return s;
    }

    public static SmtpServerData SeznamCz()
    {
        var s = new SmtpServerData();
        s.port = 25;
        s.smtpServer = "smtp.seznam.cz";
        return s;
    }
}