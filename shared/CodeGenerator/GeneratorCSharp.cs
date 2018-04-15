using System.Text;
using System.Collections.Generic;
using System;
using System.Collections;
using sunamo.CodeGenerator;
using sunamo.Constants;

public class GeneratorCSharp : GeneratorCodeAbstract
{
    public GeneratorCSharp()
    {
    }

    public void StartClass(int pocetTab, AccessModifiers _public, bool _static, string className, params string[] derive)
    {
        AddTab(pocetTab);
        PublicStatic(_public, _static);
        sb.AddItem((object)("class " + className));
        if (derive.Length != 0)
        {
            sb.AddItem((object)":");
            for (int i = 0; i < derive.Length - 1; i++)
            {
                sb.AddItem((object)(derive[i] + ","));
            }
            sb.AddItem((object)derive[derive.Length - 1]);
        }
        StartBrace(pocetTab);
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
            sb.AddItem((object)"internal");
        }
        else
        {
            throw new Exception("Neimplementovaná výjimka v metodě WriteAccessModifiers.");
        }
    }

    public void Attribute(int pocetTab, string name, string attrs)
    {
        AddTab(pocetTab);
        sb.AppendLine("[" + name + "(" + attrs + ")]");
    }

    public void Field(int pocetTab, AccessModifiers _public, bool _static, VariableModifiers variableModifiers, string type, string name, bool addHyphensToValue, string value)
    {
        ObjectInitializationOptions oio = ObjectInitializationOptions.Original;
        if (addHyphensToValue)
        {
            oio = ObjectInitializationOptions.Hyphens;
        }
        Field(pocetTab, _public, _static, variableModifiers, type, name, oio, value);
    }

    /// <summary>
    /// Pokud do A2 zadáš Private tak se jednoduše žádný modifikátor nepřidá - to proto že se může jednat o vnitřek metody atd.
    /// A1 se bude ignorovat pokud v A7 bude NewAssign
    /// Do A8 se nesmí vkládal null, program by havaroval
    /// </summary>
    /// <param name="pocetTab"></param>
    /// <param name="_public"></param>
    /// <param name="_static"></param>
    /// <param name="variableModifiers"></param>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="oio"></param>
    /// <param name="value"></param>
    public void Field(int pocetTab, AccessModifiers _public, bool _static, VariableModifiers variableModifiers, string type, string name, ObjectInitializationOptions oio, string value)
    {
        AddTab(pocetTab);
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

    public void Field(int pocetTab, AccessModifiers _public, bool _static, VariableModifiers variableModifiers, string type, string name, bool defaultValue)
    {
        AddTab(pocetTab);
        ModificatorsField(_public, _static, variableModifiers);
        ReturnTypeName(type, name);
        DefaultValue(type, defaultValue);
        sb.AddItem((object)";");
        sb.AppendLine();
        //this.sb.AddItem(sb.ToString());
    }

    private void DefaultValue(string type, bool defaultValue)
    {
        if (defaultValue)
        {
            sb.AddItem((object)"=");
            sb.AddItem((object)CSharpHelper.DefaultValueForType(type));
        }
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
    public void Ctor(int pocetTab, ModifiersConstructor mc, string ctorName, string inner, params string[] args)
    {
        AddTab(pocetTab);
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

        StartBrace(pocetTab);
        //sb.AppendLine();
        Append(pocetTab + 1, inner);
        
        EndBrace(pocetTab -2);
        sb.AppendLine();
    }

    /// <summary>
    /// Do A1 byly uloženy v pořadí typ, název, typ, název
    /// Statický konstruktor zde nevytvoříte
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="autoAssing"></param>
    /// <param name="args"></param>
    public void Ctor(int pocetTab, ModifiersConstructor mc, string ctorName, bool autoAssing, bool isBase, params string[] args)
    {
        AddTab(pocetTab);
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
        
        StartBrace(pocetTab);
        if (autoAssing && isBase)
        {
            foreach (string item in nazevParams)
            {
                
                This(pocetTab, item);
                sb.AddItem((object)"=");
                sb.AddItem((object)(item + ";"));
                sb.AppendLine();
            }
        }
        EndBrace(pocetTab);
        sb.AppendLine();
    }

    public void Property(int pocetTab, AccessModifiers _public, bool _static, string returnType, string name, bool _get, bool _set, string field)
    {     
        #region MyRegion
        AddTab(pocetTab);
        PublicStatic(_public, _static);
	    #endregion
        ReturnTypeName(returnType, name);
        AddTab(pocetTab);
        StartBrace(pocetTab);
        if (_get)
        {
            AddTab(pocetTab + 1);
            sb.AddItem((object)"get");
            StartBrace(pocetTab + 1);
            AddTab(pocetTab + 2);
            sb.AddItem((object)("return " + field + ";"));
            sb.AppendLine();
            EndBrace(pocetTab + 1);
        }
        if (_set)
        {
            AddTab(pocetTab + 1);
            sb.AddItem((object)"set");
            
            StartBrace(pocetTab + 1);
            AddTab(pocetTab + 2);
            sb.AddItem((object)(field + " = value;"));
            sb.AppendLine();
            EndBrace(pocetTab + 1);
        }
        
        EndBrace(pocetTab);
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
    public void Method(int pocetTab, AccessModifiers _public, bool _static, string returnType, string name, string inner, string args)
    {
        AddTab(pocetTab);
        PublicStatic(_public, _static);
        ReturnTypeName(returnType, name);
        StartParenthesis();
        sb.AddItem((object)args);
        EndParenthesis();
        
        StartBrace(pocetTab);
        //AddTab(pocetTab + 1);
        sb.AddItem((object)inner);
        sb.AppendLine();
        EndBrace(pocetTab);
        sb.AppendLine();
    }

    private void ReturnTypeName(string returnType, string name)
    {
        sb.AddItem((object)returnType);
        sb.AddItem((object)name);
    }

    public void Method(int pocetTab, string header, string inner)
    {
        AddTab(pocetTab);
        sb.AddItem((object)header);
        
        StartBrace(pocetTab);
        //AddTab(pocetTab + 1);
        sb.AddItem((object)inner);
        sb.AppendLine("");
        EndBrace(pocetTab);
        sb.AppendLine();
    }

    public void Usings(string usings)
    {
        sb.AddItem((object)usings);
        sb.AppendLine();
        sb.AppendLine();
    }
    /// <summary>
    /// Pokud chceš nový řádek bez jakéhokoliv textu, zadej například 2, ""
    /// Nepoužívej na to metodu jen s pocetTab, protože ji pak IntelliSense nevidělo.
    /// </summary>
    /// <param name="pocetTab"></param>
    /// <param name="p"></param>
    /// <param name="p2"></param>
    
    /// <summary>
    /// Automaticky doplní počáteční závorku
    /// </summary>
    /// <param name="podminka"></param>
    public void If(int pocetTab, string podminka)
    {
        AddTab(pocetTab);
        sb.AppendLine( "if(" + podminka + ")");
        StartBrace(pocetTab);
    }

    /// <summary>
    /// Automaticky doplní počáteční závorku
    /// </summary>
    public void Else(int pocetTab)
    {
        AddTab(pocetTab);
        sb.AppendLine("else");
        StartBrace(pocetTab);
    }

    public void EnumWithComments(AccessModifiers _public, string nameEnum, Dictionary<string, string> nameCommentEnums)
    {
        WriteAccessModifiers(_public);
        int pocetTabu = 1;
        AddTab(pocetTabu);
        sb.AddItem((object)("enum " + nameEnum));
        StartBrace(pocetTabu);
        foreach (var item in nameCommentEnums)
        {
            XmlSummary(pocetTabu + 1, item.Value);
            this.AppendLine(pocetTabu + 1, item.Key + ",");
        }
        EndBrace(pocetTabu);
    }

    private void XmlSummary(int pocetTabu, string summary)
    {
        this.AppendLine(pocetTabu, "/// <summary>");
        this.AppendLine(pocetTabu, "/// " + summary);
        this.AppendLine(pocetTabu, "/// </summary>");
    }

    private void AppendAttribute(int pocetTabu, string name, string inParentheses)
    {
        string zav = "";
        if (inParentheses != null)
        {
            zav = "(" + inParentheses + ")";
        }
        this.AppendLine(pocetTabu, "[" + name + zav + "]");
    }

    public void List(int pocetTabu, string genericType, string v, List<string> list)
    {
        string cn = "List<"+genericType+">";
        NewVariable(pocetTabu, AccessModifiers.Private, cn, v, false);
        list = CA.WrapWith(list, AllStrings.qm);
        AppendLine(pocetTabu, v + " = new List<" + genericType + ">(" + SH.Join(list, AllChars.comma));
        
    }

    

    public void This(int pocetTab, string item)
    {
        
        Append(pocetTab, "this." + item);
    }

    #region Dictionary
    public void DictionaryNumberNumber<T, U>(int pocetTabu, string nameDictionary, Dictionary<T, U> nameCommentEnums)
    {
        string cn = "Dictionary<" + typeof(T).FullName + ", " + typeof(U).FullName + ">";
        NewVariable(pocetTabu, AccessModifiers.Private, cn, nameDictionary, true);
        foreach (var item in nameCommentEnums)
        {
            this.AppendLine(pocetTabu, nameDictionary + ".Add(" + item.Key.ToString().Replace(',', '.') + ", " + item.Value.ToString().Replace(',', '.') + ");");
        }
    }

    public void DictionaryStringString(int pocetTabu, string nameDictionary, Dictionary<string, string> nameCommentEnums)
    {
        string cn = "Dictionary<string, string>";
        NewVariable(pocetTabu, AccessModifiers.Private, cn, nameDictionary, true);
        foreach (var item in nameCommentEnums)
        {
            this.AppendLine(pocetTabu, nameDictionary + ".Add(\"" + item.Key + "\", \"" + item.Value + "\");");
        }
    }

    public void DictionaryStringObject<Value>(int pocetTabu, string nameDictionary, Dictionary<string, Value> dict)
    {
        string valueType = null;
        if (dict.Count > 0)
        {
            valueType = ConvertTypeShortcutFullName.ToShortcut(DictionaryHelper.GetFirstItem(dict));
        }
        string cn = "Dictionary<string, "+valueType+">";
        NewVariable(pocetTabu, AccessModifiers.Private, cn, nameDictionary, true);
        foreach (var item in dict)
        {
            this.AppendLine(pocetTabu, nameDictionary + ".Add(\"" + item.Key + "\", " + item.Value + ");");
        }
    }
    #endregion

    private void NewVariable(int pocetTabu, AccessModifiers _public, string cn, string name, bool createInstance)
    {
        AddTab2(pocetTabu, "");
        WriteAccessModifiers(_public);
        sb.AddItem((object)cn);
        sb.AddItem((object)name);
        if (createInstance)
        {
            sb.AddItem((object)(" = new " + cn + "();"));
        }
        else
        {
            sb.AddItem((object)" = null;");
        }
    }

    public void Enum(int pocetTabu, AccessModifiers _public, string nameEnum, List<EnumItem> enumItems)
    {  
        WriteAccessModifiers(_public);
        AddTab(pocetTabu);
        sb.AddItem((object)("enum " + nameEnum));
        StartBrace(pocetTabu);
        foreach (var item in enumItems)
        {
            if (item.Attributes != null)
            {
                foreach (var item2 in item.Attributes)
                {
                    AppendAttribute(pocetTabu + 1, item2.Key, item2.Value);
                }
            }
            string hex = "";
            if (item.Hex != "")
            {
                hex = "=" + item.Hex;
            }

            this.AppendLine(pocetTabu + 1, item.Name + hex + ",");
        }
        EndBrace(pocetTabu);
    }

    /// <summary>
    /// A4 nepřidává do uvozovek
    /// </summary>
    /// <param name="pocetTab"></param>
    /// <param name="objectName"></param>
    /// <param name="variable"></param>
    /// <param name="value"></param>
    public void AssignValue(int pocetTab, string objectName, string variable, string value, bool addToHyphens)
    {
        AddTab(pocetTab);
        sb.AddItem((object)(objectName + "." + variable));
        sb.AddItem((object)"=");
        if (addToHyphens)
        {
            value = SH.WrapWith(value, '"');
        }

        sb.AddItem((object)(value + ";"));
        sb.AppendLine();
    }

    public void AddValuesViaAddRange(int pocetTab, string timeObjectName, string v, string type, IList<string> whereIsUsed2, bool wrapToHyphens)
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
        AddTab(pocetTab);
        sb.AddItem((object)(objectIdentificator + v + ".AddRange(new " + type + "[] { " + SH.JoinIEnumerable(',', whereIsUsed2) + "});"));
    }

    /// <summary>
    /// Pokud nechceš použít identifikátor objektu(například u statické třídy), vlož do A2 null
    /// </summary>
    /// <param name="pocetTab"></param>
    /// <param name="timeObjectName"></param>
    /// <param name="v"></param>
    /// <param name="type"></param>
    /// <param name="whereIsUsed2"></param>
    /// <param name="wrapToHyphens"></param>
    public void AddValuesViaAddRange(int pocetTab, string timeObjectName, string v, Type type, IList<string> whereIsUsed2, bool wrapToHyphens)
    {
        AddValuesViaAddRange(pocetTab, timeObjectName, v, type.FullName, whereIsUsed2, wrapToHyphens);
        sb.AppendLine();
    }
}
