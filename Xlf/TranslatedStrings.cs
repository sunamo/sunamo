using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// For using in content template etc
/// </summary>
public class TranslatedStrings
{
    public static TranslatedStrings Instance = new TranslatedStrings();
    static Type type = typeof(TranslatedStrings);

    private TranslatedStrings()
    {

    }

    public Func<string, string> get = null;

    public void FillIfIsEmpty(string k)
    {
        var v = RHXlf.GetValueOfProperty(k, type, Instance, false);

        if (v.ToString() == string.Empty)
        {
            var tr = get(k);
            RHXlf.SetValueOfProperty(k, type, Instance, false, tr);
            //v = RHXlf.GetValueOfProperty(k, type, Instance, false);
        }
    }

    public string SetAsDefault { get; set; } = string.Empty;

    public string Delete { get; set; } = string.Empty;
}