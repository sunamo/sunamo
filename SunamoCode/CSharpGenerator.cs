using System.Text;
using System.Collections.Generic;
using System;
using System.Collections;
using sunamo.Constants;
using sunamo.Values;

public class CSharpGenerator : GeneratorCodeAbstract
{
    static Type type = typeof(CSharpGenerator);

    public CSharpGenerator()
    {
    }

    public void StartClass(int tabCount, AccessModifiers _public, bool _static, string className, params string[] derive)
    {
        AddTab(tabCount);
        PublicStatic(_public, _static);
        sb.AddItem((object)(" class " + className));
        if (derive.Length != 0)
        {
            sb.AddItem((object)":");
            for (int i = 0; i < derive.Length - 1; i++)
            {
                sb.AddItem((object)(derive[i] + ","));
            }
            sb.AddItem((object)derive[derive.Length - 1]);
        }
        StartBrace(tabCount);
    }

    private void PublicStatic(AccessModifiers _public, bool _static)
    {
        WriteAccessModifiers(_public);
        if (_static)
        {
            sb.AddItem((object)"static");
        } 
    }

    private void WriteAccessModifiers(AccessModifiers _public)
    {
        string methodName = "WriteAccessModifiers";

        if (_public == AccessModifiers.Public)
        {
            sb.AddItem((object)"public");
        }
        else if (_public == AccessModifiers.Protected)
        {
            sb.AddItem((object)"protected");
        }
        else if (_public == AccessModifiers.Private)
        {
            //sb.AddItem("private");
        }
        else if (_public == AccessModifiers.Internal)
        {
            sb.AddItem((object)"public");
        }
        else
        {
            ThrowExceptions.NotImplementedCase(type, methodName, _public);
        }
    }

    public void EndRegion(int tabCount)
    {
        AppendLine(tabCount, "#endregion");
    }

    public void Region(int tabCount, string v)
    {
        AppendLine(tabCount, "#region " + v);
        
    }

    public void Attribute(int tabCount, string name, string attrs)
    {
        AddTab(tabCount);
        sb.AppendLine("[" + name + "(" + attrs + ")]");
    }

    public void Field(int tabCount, AccessModifiers _public, bool _static, VariableModifiers variableModifiers, string type, string name, bool addHyphensToValue, string value)
    {
        ObjectInitializationOptions oio = ObjectInitializationOptions.Original;
        if (addHyphensToValue)
        {
            oio = ObjectInitializationOptions.Hyphens;
        }
        Field(tabCount, _public, _static, variableModifiers, type, name, oio, value);
    }

    /// <summary>
    /// Pokud do A2 zadáš Private tak se jednoduše žádný modifikátor nepřidá - to proto že se může jednat o vnitřek metody atd.
    /// A1 se bude ignorovat pokud v A7 bude NewAssign
    /// Do A8 value se nesmí vkládal null, program by havaroval
    /// If A7 NewAssign, put into A8 string.Empty => constructor will be empty
    /// If A7 Original, put into A8 part after =
    /// </summary>
    /// <param name="tabCount"></param>
    /// <param name="_public"></param>
    /// <param name="_static"></param>
    /// <param name="variableModifiers"></param>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="oio"></param>
    /// <param name="value"></param>
    public void Field(int tabCount, AccessModifiers _public, bool _static, VariableModifiers variableModifiers, string type, string name, ObjectInitializationOptions oio, string value)
    {
        AddTab(tabCount);
        ModificatorsField(_public, _static, variableModifiers);
        ReturnTypeName(type, name);
        sb.AddItem((object)"=");
            if (oio == ObjectInitializationOptions.Hyphens)
            {
                value = "\"" + value + "\"";
            }
        else if (oio == ObjectInitializationOptions.NewAssign)
        {
            value = "new " + type + "()";
        }

        sb.AddItem((object)value);
        //}
        sb.AddItem((object)";");
            sb.AppendLine();
    }

    public void Field(int tabCount, AccessModifiers _public, bool _static, VariableModifiers variableModifiers, string type, string name, bool defaultValue)
    {
        AddTab(tabCount);
        ModificatorsField(_public, _static, variableModifiers);
        ReturnTypeName(type, name);
        DefaultValue(type, defaultValue);
        sb.RemoveEndDelimiter();
        sb.AddItem((object)";");
        sb.AppendLine();
        //this.sb.AddItem(sb.ToString());
    }

    /// <summary>
    ///  Use method CSharpHelper.DefaultValueForType
    /// </summary>
    /// <param name="type"></param>
    /// <param name="defaultValue"></param>
    private void DefaultValue(string type, bool defaultValue)
    {
        if (defaultValue)
        {
            sb.AddItem((object)"=");
            sb.AddItem((object)CSharpHelperSunamo.DefaultValueForType(type));
        }
    }

    public static List<string> AddIntoClass(List<string> contentFileNew, List< string> insertedLines, out int classIndex, string ns)
    {
        // index line with class
        classIndex = -1;
        // whether im after {
        bool cl = false;
        bool lsf = false;
       
        for (int i = 0; i < contentFileNew.Count; i++)
        {
            if (!cl)
            {
                // can be public / public partial
                if (contentFileNew[i].Contains(" class "))
                {
                    contentFileNew[i] = contentFileNew[i].Replace(ns + "Page", "Page");
                    classIndex = i;
                    cl = true;
                }
            }
            else if (cl && !lsf)
            {
                if (contentFileNew[i].Contains("{"))
                {
                    lsf = true;
                }
            }
            else if (cl && lsf)
            {
                if (contentFileNew[i].Contains("}"))
                {
                    contentFileNew.InsertRange(i, insertedLines);
                    break;
                }
            }
        }
        return contentFileNew;
    }

    public void Namespace(int tabCount, string ns)
    {
        sb.AddItem("namespace " + ns);
        sb.AppendLine();
        sb.AddItem("{");
        sb.AppendLine();
    }

    private void ModificatorsField(AccessModifiers _public, bool _static, VariableModifiers variableModifiers)
    {
        WriteAccessModifiers(_public);
        if (variableModifiers == VariableModifiers.Mapped)
        {
            sb.AddItem((object)"const");
        }
        else
        {
            if (_static)
            {
                sb.AddItem((object)"static");
            }
            if (variableModifiers == VariableModifiers.ReadOnly)
            {
                sb.AddItem((object)"readonly");
            }
        }
    }

    /// <summary>
    /// NI
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="inner"></param>
    /// <param name="args"></param>
    public void Ctor(int tabCount, ModifiersConstructor mc, string ctorName, string inner, params string[] args)
    {
        AddTab(tabCount);
        sb.AddItem((object)SH.FirstCharLower(mc.ToString()));
        sb.AddItem((object)ctorName);
        StartParenthesis();
        List<string> nazevParams = new List<string>(args.Length / 2);
        for (int i = 0; i < args.Length; i++)
        {
            sb.AddItem((object)args[i]);
            string nazevParam = args[++i];
            nazevParams.Add(nazevParam);
            if (i != args.Length - 1)
            {
                sb.AddItem((object)(nazevParam + ","));
            }
            else
            {
                sb.AddItem((object)nazevParam);
            }
        }
        EndParenthesis();

        StartBrace(tabCount);
        //sb.AppendLine();
        Append(tabCount + 1, inner);
        
        EndBrace(tabCount -2);
        sb.AppendLine();
    }

    /// <summary>
    /// Do A1 byly uloženy v pořadí typ, název, typ, název
    /// Statický konstruktor zde nevytvoříte
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="autoAssing"></param>
    /// <param name="args"></param>
    public void Ctor(int tabCount, ModifiersConstructor mc, string ctorName, bool autoAssing, bool isBase, params string[] args)
    {
        AddTab(tabCount);
        sb.AddItem((object)SH.FirstCharLower(mc.ToString()));
        sb.AddItem((object)ctorName);
        StartParenthesis();
        List<string> nazevParams = new List<string>( args.Length / 2);
        for (int i = 0; i < args.Length; i++)
        {
            sb.AddItem((object)args[i]);
            string nazevParam = args[++i];
            nazevParams.Add(nazevParam);
            if (i != args.Length - 1)
            {
                sb.AddItem((object)(nazevParam + ","));
            }
            else
            {
                sb.AddItem((object)nazevParam);
            }
        }
        
        EndParenthesis();
        if (!isBase)
        {
            sb.AddItem((object)(": base(" + SH.Join(',', nazevParams.ToArray()) + ")"));
        }
        
        StartBrace(tabCount);
        if (autoAssing && isBase)
        {
            foreach (string item in nazevParams)
            {
                
                This(tabCount, item);
                sb.AddItem((object)"=");
                sb.AddItem((object)(item + ";"));
                sb.AppendLine();
            }
        }
        EndBrace(tabCount);
        sb.AppendLine();
    }

    /// <summary>
    /// _get, _set can be string or bool
    /// </summary>
    /// <param name="tabCount"></param>
    /// <param name="_public"></param>
    /// <param name="_static"></param>
    /// <param name="returnType"></param>
    /// <param name="name"></param>
    /// <param name="_get"></param>
    /// <param name="_set"></param>
    /// <param name="field"></param>
    public void Property(int tabCount, AccessModifiers _public, bool _static, string returnType, string name, object _get, object _set, string field)
    {     
        #region MyRegion
        AddTab(tabCount);
        PublicStatic(_public, _static);
	    #endregion
        ReturnTypeName(returnType, name);
        AddTab(tabCount);
        StartBrace(tabCount);
        if (!BTS.FalseOrNull( _get))
        {
            var s = _get.ToString();
            AddTab(tabCount + 1);
            sb.AddItem((object)"get");
            StartBrace(tabCount + 1);
            AddTab(tabCount + 2);

            if (s == true.ToString())
            {
                sb.AddItem((object)("return " + field + ";"));    
            }
            else
            {
                sb.AddItem(s);
            }

            sb.AppendLine();
            EndBrace(tabCount + 1);
        }
        if (!BTS.FalseOrNull(_set))
        {
            AddTab(tabCount + 1);
            sb.AddItem((object)"set");
            StartBrace(tabCount + 1);
            AddTab(tabCount + 2);

            var s = _set.ToString();
            if (s == true.ToString())
            {
            sb.AddItem((object)(field + " = value;"));            
            }
            else
            {
                sb.AddItem(s);
            }

            sb.AppendLine();
            EndBrace(tabCount + 1);
        }
        
        EndBrace(tabCount);
        sb.AppendLine();
    }

    /// <summary>
    /// A6 inner již musí býy odsazené pro tuto metod
    /// </summary>
    /// <param name="_public"></param>
    /// <param name="_static"></param>
    /// <param name="returnType"></param>
    /// <param name="name"></param>
    /// <param name="inner"></param>
    /// <param name="args"></param>
    public void Method(int tabCount, AccessModifiers _public, bool _static, string returnType, string name, string inner, string args)
    {
        AddTab(tabCount);
        PublicStatic(_public, _static);
        ReturnTypeName(returnType, name);
        StartParenthesis();
        sb.AddItem((object)args);
        EndParenthesis();

        AppendLine();
        StartBrace(tabCount);
        AddTab(tabCount + 1);
        sb.AddItem((object)inner);
        sb.AppendLine();
        EndBrace(tabCount);
        sb.AppendLine();
    }

    private void ReturnTypeName(string returnType, string name)
    {
        sb.AddItem((object)returnType);
        sb.AddItem((object)name);
    }

    public void Method(int tabCount, string header, string inner)
    {
        AddTab(tabCount);
        sb.AddItem((object)header);
        
        StartBrace(tabCount);
        //AddTab(tabCount + 1);
        sb.AddItem((object)inner);
        sb.AppendLine("");
        EndBrace(tabCount);
        sb.AppendLine();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="usings"></param>
    public void Using(string usings)
    {
        if (!usings.StartsWith("using "))
        {
            usings = "using " + usings + ";";
        }
        else if(!usings.Trim().EndsWith(AllStrings.sc))
        {
            usings += ";";
        }
        
        sb.AddItem(usings);
        
        sb.AppendLine();
    }

    /// <summary>
    /// Pokud chceš nový řádek bez jakéhokoliv textu, zadej například 2, ""
    /// Nepoužívej na to metodu jen s tabCount, protože ji pak IntelliSense nevidělo.
    /// </summary>
    /// <param name="tabCount"></param>
    /// <param name="p"></param>
    /// <param name="p2"></param>
    
    /// <summary>
    /// Automaticky doplní počáteční závorku
    /// </summary>
    /// <param name="podminka"></param>
    public void If(int tabCount, string podminka)
    {
        AddTab(tabCount);
        sb.AppendLine( "if(" + podminka + ")");
        StartBrace(tabCount);
    }

    #region Dictionary
    public void DictionaryNumberNumber<T, U>(int tabCount, string nameDictionary, Dictionary<T, U> nameCommentEnums)
    {
        DictionaryFromDictionary<T, U>(tabCount, nameDictionary, nameCommentEnums);
    }

    public void DictionaryStringString(int tabCount, string nameDictionary, Dictionary<string, string> nameCommentEnums)
    {
        DictionaryFromDictionary<string, string>(tabCount, nameDictionary, nameCommentEnums);
    }

    public void DictionaryStringListString(int tabCount, string nameDictionary, Dictionary<string, List<string>> result)
    {
        string cn = "Dictionary<string, List<string>>";
        NewVariable(tabCount, AccessModifiers.Private, cn, nameDictionary, true);
        foreach (var item in result)
        {
            var list = CA.WrapWithQm(item.Value);
            this.AppendLine(tabCount, nameDictionary + ".Add(\"" + item.Key + "\", CA.ToListString(" + SH.Join(AllChars.comma,list) + "));");
        }
    }

    #region DictionaryFrom
    public void DictionaryFromRandomValue<Key, Value>(int tabCount, string nameDictionary, List<Key> keys, Func<Value> randomValue, bool addingValue = true)
    {
        

        Dictionary<Key, Value> dict = new Dictionary<Key, Value>();
        for (int i = 0; i < keys.Count; i++)
        {
            dict.Add(keys[i], randomValue.Invoke());
        }

        DictionaryFromDictionary<Key, Value>(tabCount, nameDictionary, dict, addingValue);
    }

    public void DictionaryFromTwoList<Key, Value>(int tabCount, string nameDictionary, List<Key> keys, List<Value> values, bool addingValue = true)
    {
        ThrowExceptions.DifferentCountInLists(type, RH.CallingMethod(), "keys", keys, "values", values);

        Dictionary<Key, Value> dict = new Dictionary<Key, Value>();
        for (int i = 0; i < keys.Count; i++)
        {
            dict.Add(keys[i], values[i]);
        }

        DictionaryFromDictionary<Key, Value>(tabCount, nameDictionary, dict, addingValue);
    }

    public void DictionaryFromDictionary<Key, Value>(int tabCount, string nameDictionary, Dictionary<Key, Value> dict, bool addingValue = true)
    {
        string valueType = null;
        if (dict.Count > 0)
        {
            valueType = ConvertTypeShortcutFullName.ToShortcut(DictionaryHelper.GetFirstItemValue<Key,Value>(dict).GetType().FullName);
        }
        string cn = "Dictionary<string, " + valueType + ">";//
        NewVariable(tabCount, AccessModifiers.Private, cn, nameDictionary, false);
        AppendLine();
        CreateInstance(cn, nameDictionary);

        if (addingValue)
        {
            GetDictionaryValuesFromDictionary(tabCount, nameDictionary, dict);
        }
        
    }

    public void DictionaryFromDictionaryInnerList<Key, Value>(int tabCount, string nameDictionary, Dictionary<Key, Value> dict, bool addingValue = true)
    {
        string valueType = null;
        if (dict.Count > 0)
        {
            valueType = ConvertTypeShortcutFullName.ToShortcut(DictionaryHelper.GetFirstItemValue<Key, Value>(dict).GetType().FullName);
        }
        string cn = "Dictionary<string, List<" + valueType + ">>";//
        NewVariable(tabCount, AccessModifiers.Private, cn, nameDictionary, false);
        AppendLine();
        CreateInstance(cn, nameDictionary);

        if (addingValue)
        {
            GetDictionaryValuesFromDictionary(tabCount, nameDictionary, dict);
        }

    }

    public void GetDictionaryValuesFromRandomValue<Key, Value>(int tabCount, string nameDictionary, List<Key> keys, Func<Value> randomValue)
    {
        Dictionary<Key, Value> dict = new Dictionary<Key, Value>();
        for (int i = 0; i < keys.Count; i++)
        {
            dict.Add(keys[i], randomValue.Invoke());
        }

        GetDictionaryValuesFromDictionary<Key, Value>(tabCount, nameDictionary, dict);
    }

    public void GetDictionaryValuesFromTwoList<Key, Value>(int tabCount, string nameDictionary, List<Key> keys, List<Value> values)
    {
        ThrowExceptions.DifferentCountInLists(type, RH.CallingMethod(), "keys", keys, "values", values);

        Dictionary<Key, Value> dict = new Dictionary<Key, Value>();
        for (int i = 0; i < keys.Count; i++)
        {
            dict.Add(keys[i], values[i]);
        }

        GetDictionaryValuesFromDictionary<Key, Value>(tabCount, nameDictionary, dict);
    }

    public void GetDictionaryValuesFromDictionaryInnerList<Key, Value>(int tabCount, string nameDictionary, Dictionary<Key, List< Value>> dict)
    {
        Key key = default(Key);
        Value value = default(Value);

        foreach (var item in dict)
        {
            key = item.Key;
            value = RuntimeHelper.CastToGeneric<Value>( item.Value.FirstOrNull());

            break;
        }

        var tKey = key.GetType();
        var tValue = value.GetType();

        IEnumerable valueS;
        string keyS = null;
        
        keyS = key.ToString();

        Field(tabCount, AccessModifiers.Private, false, VariableModifiers.None, string.Format("Dictionary<{0}, List<{1}>>", tKey.Name, tValue.Name), nameDictionary, true);

        foreach (var item in dict)
        {
            keyS = item.Key.ToString();
            valueS = item.Value;

            CSharpHelper.WrapWithQuote(tKey, ref keyS);

            var valueS2 = CSharpHelper.WrapWithQuoteList(tValue,  valueS);

            this.AppendLine(tabCount, nameDictionary + ".Add(" + keyS + ", CA.ToList<" + tValue.Name + ">("  + valueS2 + "));");
        }
    }

    public void GetDictionaryValuesFromDictionary<Key, Value>(int tabCount, string nameDictionary, Dictionary<Key, Value> dict)
    {
        Key key = default(Key);
        Value value = default(Value);

        foreach (var item in dict)
        {
            key = item.Key;
            value = item.Value;

            break;
        }

        var tKey = key.GetType();
        var tValue = value.GetType();

        string valueS, keyS = null;
        valueS = value.ToString();
        keyS = key.ToString();

        foreach (var item in dict)
        {
            keyS = item.Key.ToString();
            valueS = item.Value.ToString();

            CSharpHelper.WrapWithQuote(tKey, ref keyS);

            CSharpHelper.WrapWithQuote(tValue, ref valueS);


            this.AppendLine(tabCount, nameDictionary + ".Add(" + keyS + ", " + valueS + ");");
        }
    }
    #endregion

    public void DictionaryStringObject<Value>(int tabCount, string nameDictionary, Dictionary<string, Value> dict)
    {
        DictionaryFromDictionary<string, Value>(tabCount, nameDictionary, dict);
    }


    #endregion

    /// <summary>
    /// Automaticky doplní počáteční závorku
    /// </summary>
    public void Else(int tabCount)
    {
        AddTab(tabCount);
        sb.AppendLine("else");
        StartBrace(tabCount);
    }

    public void EnumWithComments(AccessModifiers _public, string nameEnum, Dictionary<string, string> nameCommentEnums)
    {
        WriteAccessModifiers(_public);
        int tabCount = 1;
        AddTab(tabCount);
        sb.AddItem((object)("enum " + nameEnum));
        StartBrace(tabCount);
        foreach (var item in nameCommentEnums)
        {
            XmlSummary(tabCount + 1, item.Value);
            this.AppendLine(tabCount + 1, item.Key + ",");
        }
        EndBrace(tabCount);
    }

    private void XmlSummary(int tabCount, string summary)
    {
        this.AppendLine(tabCount, "/// <summary>");
        this.AppendLine(tabCount, "/// " + summary);
        this.AppendLine(tabCount, "/// </summary>");
    }

    private void AppendAttribute(int tabCount, string name, string inParentheses)
    {
        string zav = "";
        if (inParentheses != null)
        {
            zav = "(" + inParentheses + ")";
        }
        this.AppendLine(tabCount, "[" + name + zav + "]");
    }

    public void List(int tabCount, string genericType, string listName, List<string> list, bool addHyphens = true)
    {
        string cn = "List<" + genericType + AllStrings.gt;
        NewVariable(tabCount, AccessModifiers.Private, cn, listName, false);
        if (addHyphens)
        {
            list = CA.WrapWith(list, AllStrings.qm);
        }
        if (genericType == "string")
        {
            AppendLine(tabCount, listName + " = CA.ToListString(" + SH.Join(AllChars.comma, list) + ");");
        }
        else
        {
            AppendLine(tabCount, listName + " = new List<" + genericType + ">(CA.ToEnumerable(" + SH.Join(AllChars.comma,list) + "));");
        }


    }



    public void This(int tabCount, string item)
    {
        Append(tabCount, "this." + item);
    }

    

    private void NewVariable(int tabCount, AccessModifiers _public, string cn, string name, bool createInstance)
    {
        AddTab2(tabCount, "");
        WriteAccessModifiers(_public);
        sb.AddItem((object)cn);
        sb.AddItem((object)name);
        if (createInstance)
        {
            sb.EndLine(AllChars.sc);
            AppendLine();
            CreateInstance(cn, name);
        }
        else
        {
            sb.AddItem((object)"= null;");
        }
        sb.AppendLine();
    }

    private void CreateInstance(string className, string variableName)
    {
        sb.AddItem((object)(variableName + " = new " + className + "();"));
    }

    public void Enum(int tabCount, AccessModifiers _public, string nameEnum, List<EnumItem> enumItems)
    {  
        WriteAccessModifiers(_public);
        AddTab(tabCount);
        sb.AddItem((object)("enum " + nameEnum));
        StartBrace(tabCount);
        foreach (var item in enumItems)
        {
            if (item.Attributes != null)
            {
                foreach (var item2 in item.Attributes)
                {
                    AppendAttribute(tabCount + 1, item2.Key, item2.Value);
                }
            }
            string hex = "";
            if (item.Hex != "")
            {
                hex = "=" + item.Hex;
            }

            this.AppendLine(tabCount + 1, item.Name + hex + ",");
        }
        EndBrace(tabCount);
    }

    /// <summary>
    /// A4 nepřidává do uvozovek
    /// </summary>
    /// <param name="tabCount"></param>
    /// <param name="objectName"></param>
    /// <param name="variable"></param>
    /// <param name="value"></param>
    public void AssignValue(int tabCount, string objectName, string variable, string value, bool addToHyphens)
    {
        AddTab(tabCount);
        sb.AddItem((object)(objectName + "." + variable));
        sb.AddItem((object)"=");
        if (addToHyphens)
        {
            value = SH.WrapWith(value, '"');
        }

        sb.AddItem((object)(value + ";"));
        sb.AppendLine();
    }

    public void AddValuesViaAddRange(int tabCount, string timeObjectName, string v, string type, List<string> whereIsUsed2, bool wrapToHyphens)
    {
        string objectIdentificator = "";
        if (timeObjectName != null)
        {
            objectIdentificator = timeObjectName + ".";
        }
        if (wrapToHyphens)
        {
            whereIsUsed2 = CA.WrapWith(whereIsUsed2, "\"");
        }
        AddTab(tabCount);
        sb.AddItem((object)(objectIdentificator + v + ".AddRange(new " + type + "[] { " + SH.JoinIEnumerable(',', whereIsUsed2) + "});"));
    }

    /// <summary>
    /// Pokud nechceš použít identifikátor objektu(například u statické třídy), vlož do A2 null
    /// </summary>
    /// <param name="tabCount"></param>
    /// <param name="timeObjectName"></param>
    /// <param name="v"></param>
    /// <param name="type"></param>
    /// <param name="whereIsUsed2"></param>
    /// <param name="wrapToHyphens"></param>
    public void AddValuesViaAddRange(int tabCount, string timeObjectName, string v, Type type, List<string> whereIsUsed2, bool wrapToHyphens)
    {
        AddValuesViaAddRange(tabCount, timeObjectName, v, type.FullName, whereIsUsed2, wrapToHyphens);
        sb.AppendLine();
    }
}
