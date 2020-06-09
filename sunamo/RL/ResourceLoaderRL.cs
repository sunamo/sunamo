using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ResourceLoaderRL
{
    public string GetString(string k)
    {
        return sess.i18n(k);
    }
}