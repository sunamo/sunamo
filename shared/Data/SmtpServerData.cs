using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SmtpServerData
{
    public string smtpServer = "smtp.gmail.com";
    public int port = 587;

    public static SmtpServerData SeznamCz()
    {
        var s = new SmtpServerData();
        s.port = 25;
        s.smtpServer = "smtp.seznam.cz";
        return s;
    }
}