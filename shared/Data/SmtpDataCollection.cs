using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SmtpDataCollection : ObservableCollection<SmtpData>
{
    public List<List<string>> ToSave()
    {
        List<List<string>> result = new List<List<string>>(Count);
        foreach (var item in this)
        {
            result.Add(CA.ToListString(item.smtpServer, item.port, item.login, item.pw, item.IsDefault));
        }

        return result;
    }

}