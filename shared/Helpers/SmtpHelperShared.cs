using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SmtpHelper
{
    public static int ParsePort(string s)
    {
        return BTS.ParseInt(s, NumConsts.defaultPortIfCannotBeParsed);
    }
}