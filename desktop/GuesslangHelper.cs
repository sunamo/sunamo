using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using win.Helpers.Powershell;


/// <summary>
/// To use Guesslang which require Tensorflow   1.7.0rc1 must be used Python 3.6.x. 
/// Is enough pip install guesslang
/// Working badly, for aspx content return SQL
/// </summary>
public class GuesslangHelper
{
    

    public static string DetectLanguage(string s)
    {
        var d = "echo \"" + s.Replace("\"", "'") +  "\" | guesslang";
        const string SourceCode = "The source code is written in";

        var result = PowershellRunner.Invoke(CA.ToEnumerable(d));
        foreach (var item in result[0])
        {
            if (item.Contains(SourceCode))
            {
                var rs = SH.TextAfter(item, SourceCode);
                return rs;
            }
        }

        return null;
    }

    public static void GenerateDictionaryForMapToTextBoxSyntax(Func<Dictionary<string, string>, string> CSharpHelperGetDictionaryValuesFromDictionary)
    {
        string File = @"c:\Program Files\Python36\Lib\site-packages\guesslang\data\languages.json";
        var s = TF.ReadFile(File);
        //TextReader tr = TF.TextReader(File);
        //JsonTextReader js = new JsonTextReader(tr);
        //dynamic son = JsonConvert.DeserializeObject(s);
        dynamic son = new object(); //JsonNewtonSoft.instance.Deserialize(s);
        Dictionary<string, string> d = new Dictionary<string, string>();
        foreach (var item in son)
        {
            var name = item.Name;
            d.Add(name, string.Empty);
        }

        var output = CSharpHelperGetDictionaryValuesFromDictionary(d);
        ClipboardHelper.SetText(output);
    }
}