using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
public static partial class CSharpHelper
{
    public static string GetDictionaryValuesFromTwoList(List<string> names, List<string> chars)
    {
        return CSharpHelper.GetDictionaryValuesFromTwoList<string, string>(2, "a", names, chars, new CSharpGeneratorArgs { splitKeyWith = "," });
    }

    //var output = 

    public static string GetDictionaryValuesFromDictionary(Dictionary<string, string> d)
    {
        return CSharpHelper.GetDictionaryValuesFromDictionary<string, string>(0, "name", d);
    }

    public static string GetSummaryXmlDocumentation(List<string> cs)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < cs.Count(); i++)
        {
            var line = cs[i];
            if (line.StartsWith(CodeElementsConstants.XmlDocumentationCsharp))
            {
                line = SH.TrimStart(line, CodeElementsConstants.XmlDocumentationCsharp).Trim();
                sb.AppendLine(line);
                if (line.Contains("</summary>"))
                {
                    break;
                }
            }
        }
        return sb.ToString();
    }
    public static string CreateConstsForSearchUris(List<string> uris)
    {
        CSharpGenerator csg = new CSharpGenerator();

        // In key name of const, in value value
        Dictionary<string, string> dict = new Dictionary<string, string>();
        List<string> all = new List<string>();

        foreach (var item in uris)
        {
            Uri u = new Uri(item);
            string name = ConvertPascalConvention.ToConvention(u.Host);
            dict.Add(name, item);
        }

        CreateConsts(csg, dict);

        csg.List(2, "string", "All", all, new CSharpGeneratorArgs { addHyphens = true });

        return csg.ToString();
    }

    public static string CreateConsts(Dictionary<string, string> dict)
    {
        CSharpGenerator csg = new CSharpGenerator();

        return CreateConsts(csg, dict);
    }

    private static string CreateConsts(CSharpGenerator csg, Dictionary<string, string> dict)
    {
        foreach (var item in dict)
        {

            csg.Field(2, AccessModifiers.Public, true, VariableModifiers.Mapped, "string", item.Key, true, item.Value);
        }
        
        return csg.ToString();
    }

    #region DictionaryWithClass
    public static string DictionaryWithClass<Key,Value>(int tabCount, string nameDictionary, List<Key> keys, Func<Value> randomValue, CSharpGeneratorArgs a = null)
    {
        CSharpGenerator genCS = new CSharpGenerator();
        genCS.StartClass(0, AccessModifiers.Private, false, nameDictionary);
        
        genCS.DictionaryFromRandomValue<Key, Value>(0, nameDictionary, keys, randomValue, a);
        var inner = GetDictionaryValuesFromRandomValue<Key, Value>(tabCount, nameDictionary, keys, randomValue);
        genCS.Ctor(1, ModifiersConstructor.Private, nameDictionary, inner);
        genCS.EndBrace(0);
        return genCS.ToString();
    }
    /// <summary>
    /// addingValue = 0
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="tabCount"></param>
    /// <param name="nameDictionary"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static string DictionaryWithClass<Key, Value>(int tabCount, string nameDictionary, Dictionary<Key, Value> d, CSharpGeneratorArgs a = null)
    {
        if (a == null)
        {
            a.addingValue = false;
        }

        CSharpGenerator genCS = new CSharpGenerator();
        genCS.StartClass(0, AccessModifiers.Private, false, nameDictionary);
        genCS.DictionaryFromDictionary<Key, Value>(0, nameDictionary, d, a);
        var inner = GetDictionaryValuesFromDictionary<Key, Value>(tabCount, nameDictionary, d);
        genCS.Ctor(1, ModifiersConstructor.Private, nameDictionary, inner);
        genCS.EndBrace(0);
        return genCS.ToString();
    }
    #endregion
    #region GetDictionaryValues
    public static string GetDictionaryValuesFromDictionary<Key, Value>(int tabCount, string nameDictionary, Dictionary<Key, Value> dict)
    {
        CSharpGenerator csg = new CSharpGenerator();
        csg.GetDictionaryValuesFromDictionary<Key, Value>(tabCount, nameDictionary, dict);
        return csg.ToString();
    }
    /// <summary>
    /// a: splitKeyWith
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="tabCount"></param>
    /// <param name="nameDictionary"></param>
    /// <param name="keys"></param>
    /// <param name="values"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    public static string GetDictionaryValuesFromTwoList<Key, Value>(int tabCount, string nameDictionary, List<Key> keys, List<Value> values, CSharpGeneratorArgs a)
    {
        CSharpGenerator csg = new CSharpGenerator();
        csg.GetDictionaryValuesFromTwoList<Key, Value>(tabCount, nameDictionary, keys, values,  a);
        return csg.ToString();
    }
    public static string GetDictionaryValuesFromRandomValue<Key, Value>(int tabCount, string nameDictionary, List<Key> keys, Func<Value> randomValue)
    {
        CSharpGenerator csg = new CSharpGenerator();
        csg.GetDictionaryValuesFromRandomValue<Key, Value>(tabCount, nameDictionary, keys, randomValue);
        return csg.ToString();
    }
    #endregion

    //public static string GetDictionary(string nameDictionary)
    //{
    //}
    public static string RemoveXmlDocCommentsExceptSummary(List<string> list, ref bool removedAnything)
    {
        removedAnything = false;
        const string a = @"/// <returns></returns>";
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].Trim() == a)
            {
                removedAnything = true;
                list.RemoveAt(i);
            }
        }
        return SH.JoinNL( list);
    }
    public static string RemoveXmlDocComments(List<string> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            list[i] = SH.RemoveAfterFirst(list[i], "///");
        }
        CA.TrimWhereIsOnlyWhitespace(list);
        var s = SH.JoinNL(list);
        
        CA.DoubleOrMoreMultiLinesToSingle(ref s);
        
        return s;
    }
    public static List<string> RemoveComments(List<string> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            list[i] = SH.RemoveAfterFirst(list[i], "//");
        }
        CA.RemoveStringsEmpty2(list);
        return list;
    }
    public static string GetDictionaryStringObject<Value>(int tabCount, List<string> keys, List<Value> values, string nameDictionary, CSharpGeneratorArgs a)
    {


        int pocetTabu = 0;
        CSharpGenerator gen = new CSharpGenerator();
        gen.DictionaryFromTwoList<string, Value>(tabCount, nameDictionary, keys, values, a);
        if (a.checkForNull)
        {
            gen.If(pocetTabu, nameDictionary + " " + "== null");
        }
        gen.GetDictionaryValuesFromDictionary<string, Value>(pocetTabu, nameDictionary, DictionaryHelper.GetDictionary<string, Value>(keys, values));
        if (a.checkForNull)
        {
            gen.EndBrace(pocetTabu);
        }
        
        string result = gen.ToString();
        return result;
    }
    
    
    public static string DefaultValueForTypeSqLite(string type)
    {
        if (type.Contains(AllStrings.dot))
        {
            type = ConvertTypeShortcutFullName.ToShortcut(type);
        }
        switch (type)
        {
            case "TEXT":
                return AllStrings.qm;
            case "INTEGER":
                return int.MaxValue.ToString();
            case "REAL":
                return "0.0";
            case "DATETIME":
                // Původně tu bylo MinValue kvůli SQLite ale dohodl jsem se že SQLite už nebudu používat a proto si ušetřím v kódu práci s MSSQL 
                return "DateTime.MinValue";
            case "BLOB":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return "null";
            default:
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(),type);
                break;
        }
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepodporovaný typ");
        return null;
    }
   
    public static string GetCtorInner(int tabCount, IEnumerable values)
    {
        const string assignVariable = "this.{0} = {0};";
        CSharpGenerator csg = new CSharpGenerator();
        foreach (var item in values)
        {
            csg.AppendLine(tabCount, SH.Format2(assignVariable, item));
        }
        return csg.ToString().Trim();
    }
    
    /// <summary>
    /// Return also List because Array isn't use
    /// </summary>
    /// <param name = "input"></param>
    /// <param name = "arrayName"></param>
    public static string GetArray(List<string> input, string arrayName)
    {
        CSharpGenerator generator = new CSharpGenerator();
        generator.List(0, "string", arrayName, input);
        return generator.ToString();
    }
    public static string GetList(List<string> input, string listName)
    {
        CSharpGenerator generator = new CSharpGenerator();
        generator.List(0, "string", listName, input);
        return generator.ToString();
    }
    public static List<string> RemoveRegions(List<string> lines)
    {
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            string line = lines[i].Trim();
            if (line.StartsWith("#" + "region" + " ") || line.StartsWith("#endregion"))
            {
                lines.RemoveAt(i);
            }
        }
        return lines;
    }

    public static void ReplaceForConsts(string pathXlfKeys)
    {
        var c = TF.ReadAllLines(pathXlfKeys);
        for (int i = 0; i < c.Count; i++)
        {
            var a = c[i];
            if (a.Contains(CSharpParser.p))
            {
                if (!a.Contains("const") && !a.Contains("class"))
                {
                    a = SH.ReplaceOnce(a, "static ", string.Empty);
                    a = SH.ReplaceOnce(a, "readonly ", string.Empty);
                    c[i] = SH.ReplaceOnce(a, CSharpParser.p, CSharpParser.p + "const ");
                }
            }
        }

        TF.SaveLines(c, pathXlfKeys);
    }

    public static string GetConsts(List<string> list, bool toCamelConvention)
    {
        return GetConsts(null, list, toCamelConvention);
    }

    /// <summary>
    /// A1 can be null
    /// 
    /// GenerateConstants - const without value
    /// GetConsts - static readonly with value
    /// </summary>
    /// <param name="list"></param>
    /// <param name="toCamelConvention"></param>
    /// <returns></returns>
    public static string GetConsts(List<string> names, List<string> list, bool toCamelConvention)
    {
        if (names != null)
        {
            ThrowExceptions.DifferentCountInLists(Exc.GetStackTrace(), type, Exc.CallingMethod(), "names", names, "list", list);
        }

        CSharpGenerator csg = new CSharpGenerator();
        for (int i = 0; i < list.Count; i++)
        {
            var item = list[i];
            string name = item;

            if (names != null)
            {
                name = names[i];
            }

            if (toCamelConvention)
            {
                name = ConvertCamelConvention.ToConvention(item);
            }
            csg.Field(0, AccessModifiers.Public, true, VariableModifiers.ReadOnly, "string", name, true, item);
        }
        return csg.ToString();
    }

    /// <summary>
    /// GenerateConstants - const without value
    /// GetConsts - static readonly with value
    /// </summary>
    /// <param name="tabCount"></param>
    /// <param name="changeInput"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string GenerateConstants(int tabCount, Func<string, string> changeInput, List<string> input)
    {
        CSharpGenerator csg = new CSharpGenerator();
        foreach (var item in input)
        {
            var name = changeInput(item);
            csg.Field(tabCount, AccessModifiers.Public, true, VariableModifiers.Mapped, "string", name, true, string.Empty);
        }
        return csg.ToString();
    }
    public static void WrapWithQuote(Type tKey, ref string keyS)
    {
        if (tKey == Types.tString)
        {
            keyS = SH.WrapWithQm(keyS);
        }
        else if (tKey == Types.tChar)
        {
            keyS = SH.WrapWith(keyS, '\'');
        }
        else
        {
            
        }
    }
    public static string WrapWithQuoteList(Type tValue, IEnumerable valueS)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in valueS)
        {
            var v = item.ToString();
            WrapWithQuote(tValue, ref v);
            sb.Append(v + AllStrings.comma);
        }
        return sb.ToString().TrimEnd(AllChars.comma);
    }
    public static Dictionary<string, string> ParseFields(List<string> l)
    {
        CA.RemoveStringsEmpty2(l);
        CA.ChangeContent(null,l, e => SH.RemoveAfterFirst(e, AllChars.equals));
        CA.TrimEnd(l, AllChars.sc);
        Dictionary<string, string> r = new Dictionary<string, string>();
        foreach (var item in l)
        {
            var p = SH.SplitByWhiteSpaces(item);
            var c = p.Count;
            r.Add( SH.FirstCharLower( p[c-1]), p[c - 2]);
        }
        return r;
    }
    const string tProperty = @"{0} {2} = {1};
    public {0} {3} { get { return {2}; } set { {2} = value; OnPropertyChanged(" + "\"{3}\"); } }" + @"
";
    public static string GenerateProperties(List<string> l)
    {
        var d = ParseFields(l);
        StringBuilder sb = new StringBuilder();
        foreach (var item in d)
        {
            sb.AppendLine(SH.Format3(tProperty, item.Value, CSharpHelperSunamo.DefaultValueForType(item.Value), item.Key, SH.FirstCharUpper(item.Key)));
        }
        return sb.ToString();
    }
}