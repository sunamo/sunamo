﻿
using System.Collections.Generic;
using System.Text;
using System;
using System.Data.SqlClient;
using System.Data;
using sunamo;

//using System.Activities;
public partial class MSColumnsDB : List<MSSloupecDB>
{
    bool signed = false;
    string derived = null;
    string replaceMSinMSStoredProceduresI = null;
    static string _tableNameField = "_tableName";

    /// <summary>
    /// A1 od jakých rozhraní a tříd by měla být odvozena třída TableRow
    /// </summary>
    /// <param name="derived"></param>
    /// <param name="signed"></param>
    /// <param name="p"></param>
    public MSColumnsDB(string derived, bool signed, params MSSloupecDB[] p)
    {
        this.derived = derived;
        this.signed = signed;
        this.AddRange(p);
    }

    public MSColumnsDB(bool signed, string replaceMSinMSStoredProceduresI, params MSSloupecDB[] p)
    {
        this.signed = signed;
        this.replaceMSinMSStoredProceduresI = replaceMSinMSStoredProceduresI;
        this.AddRange(p);
    }

    /// <summary>
    /// A2 is name of site - part before Layer class
    /// A3 is Lyr_ etc.
    /// A4 is parameter which is inserted into GetSqlCreateTable.
    /// dynamicTables - whether is not desirable to create references to other tables. Good while test tables and apps, when I will it delete later.
    /// </summary>
    public static string GetSqlForClearAndCreateTables(Dictionary<string, MSColumnsDB> sl, string Mja, string tablePrefix, bool dynamicTables)
    {
        StringBuilder dropes = new StringBuilder();
        StringBuilder createTable = new StringBuilder();

        foreach (KeyValuePair<string, MSColumnsDB> item in sl)
        {
            if (!IsDynamic(item.Key, tablePrefix))
            {
                dropes.AppendLine("MSStoredProceduresI.ci.DropTableIfExists(\"" + item.Key + "\");");
            }
        }
        foreach (KeyValuePair<string, MSColumnsDB> item in sl)
        {
            if (!IsDynamic(item.Key, tablePrefix))
            {
                createTable.AppendLine(Mja + "Layer.s[\"" + item.Key + "\"].GetSqlCreateTable(\"" + item.Key + "\", " + dynamicTables.ToString().ToLower() + ").ExecuteNonQuery();");
            }
        }

        return dropes.ToString() + Environment.NewLine + createTable.ToString();
    }

    /// <summary>
    /// True if name of table after prefix contains another _
    /// </summary>
    /// <param name="p"></param>
    /// <param name="tablePrefix"></param>
    /// <returns></returns>
    private static bool IsDynamic(string p, string tablePrefix)
    {
        //List<int> nt = SH.ReturnOccurencesOfString(p, "_");
        string d = null;
        if (p.StartsWith(tablePrefix) && tablePrefix != "")
        {
            d = p.Substring(tablePrefix.Length);
        }
        else
        {
            d = p;
        }

        return d.Contains("_");
    }



    public MSColumnsDB(bool signed, params MSSloupecDB[] p)
    {
        this.signed = signed;
        this.AddRange(p);
    }

    public MSColumnsDB(params MSSloupecDB[] p)
    {
        this.AddRange(p);
    }

    public string GetTROfColumns()
    {
        StringBuilder sb = new StringBuilder();
        if (this.Count == 0)
        {
            throw new Exception("Nebyly nalezeny žádné sloupce");
        }
        sb.Append("(");
        foreach (MSSloupecDB item in this)
        {
            sb.Append(item.Name + ",");

        }
        return sb.ToString().TrimEnd(',') + ")";
    }

    /// <summary>
    /// A2 pokud nechci aby se mi vytvářeli reference na ostatní tabulky. Vhodné při testování tabulek a programů, kdy je pak ještě budu mazat a znovu plnit.
    /// </summary>
    public SqlCommand GetSqlCreateTable(string table, bool dynamicTables, SqlConnection conn)
    {
        string sql = GeneratorMsSql.CreateTable(table, this, dynamicTables, conn);
        SqlCommand comm = new SqlCommand(sql, conn);
        return comm;
    }

    /// <summary>
    /// A2 pokud nechci aby se mi vytvářeli reference na ostatní tabulky. Vhodné při testování tabulek a programů, kdy je pak ještě budu mazat a znovu plnit.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="dynamicTables"></param>
    /// <returns></returns>
    public SqlCommand GetSqlCreateTable(string table, bool dynamicTables)
    {
        return GetSqlCreateTable(table, dynamicTables, MSDatabaseLayer.conn);
    }

    /// <summary>
    /// Vyplň A2 na SE pokud chceš všechny sloupce
    /// </summary>
    /// <param name="tableNameWithPrefix"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    public string GetCsSunamoGridView(string tableNameWithPrefix, string columns)
    {
        CSharpGenerator csg = new CSharpGenerator();

        csg.AppendLine(2, "DataTable dt = MSStoredProceduresI.ci.SelectDataTableSelective(\"" + tableNameWithPrefix + "\");");
        csg.AppendLine(2, "int radku = dt.Rows.Count;");
        List<string> s = new List<string>();
        List<string> ss = new List<string>();
        foreach (MSSloupecDB item in this)
        {
            string a = item.Name + "s";
            s.Add(item.Name);
            ss.Add(a);
            csg.AppendLine(2, "string[] {0} = new string[radku];", a);
        }

        csg.AppendLine(2, "");
        csg.AppendLine(2, "");
        csg.AppendLine(2, "int i = 0;");
        csg.AppendLine(2, "foreach (DataRow item in dt.Rows)");
        csg.StartBrace(2);
        csg.AppendLine(3, "object[] o = item.ItemArray;");
        for (int i = 0; i < this.Count; i++)
        {
            csg.AppendLine(3, ss[i] + "[i] = o[" + i.ToString() + "].ToString();");
        }
        csg.AppendLine(3, "i++;");
        csg.EndBrace(2);

        csg.AppendLine(2, "SunamoGridView sgv = new SunamoGridView({0});", SH.Join(',', ss.ToArray()));

        csg.AppendLine(2, "");
        csg.AppendLine(2, "");


        for (int i = 0; i < ss.Count; i++)
        {
            csg.AppendLine(2, "sgv.AddSpanColumn(\"" + s[i] + "\", " + i + ");");
        }

        csg.AppendLine(2, "");
        csg.AppendLine(2, "");
        csg.AppendLine(2, "");

        return csg.ToString();
    }

    /// <summary>
    /// A1
    /// A2 = mss_, může být vloženo i Nope_, on si jej automaticky nahradí za SE
    /// A3 = folder to write in scz
    /// A4 = folder to write in sczAdmin
    /// </summary>
    /// <param name="nazevTabulky"></param>
    /// <param name="dbPrefix"></param>
    /// <param name="folderSaveToDirectoryName"></param>
    /// <param name="generate4"></param>
    /// <returns></returns>
    public string SaveCsTableRow(string nazevTabulky, string dbPrefix, string folderSaveToDirectoryName, string folderSunamoCzAdminSaveToDirectoryName, bool generate4)
    {
        if (nazevTabulky.Contains("DayView") || nazevTabulky.EndsWith("2"))
        {
            return "";
        }

        string nameCs = null;
        string cs = GetCsTableRow(signed, nazevTabulky, dbPrefix, out nameCs);
        string Path = FS.Combine(folderSaveToDirectoryName, "DontCopy2", nameCs + ".cs");
        string PathSczAdmin = null;

        if (folderSunamoCzAdminSaveToDirectoryName != null)
        {
            PathSczAdmin = FS.Combine(folderSunamoCzAdminSaveToDirectoryName, "DontCopy2", nameCs + ".cs");
        }

        // create base
        string csBase = GetCsTableRowBase(nazevTabulky, dbPrefix);
        string PathBase = FS.Combine(folderSaveToDirectoryName, "DontCopyBase", nameCs + "Base.cs");

        string PathSczBaseAdmin = null;
        if (folderSunamoCzAdminSaveToDirectoryName != null)
        {
            PathSczBaseAdmin = FS.Combine(folderSunamoCzAdminSaveToDirectoryName, "DontCopyBase", nameCs + "Base.cs");
        }

        FS.CreateUpfoldersPsysicallyUnlessThere(Path);
        //FS.CreateUpfoldersPsysicallyUnlessThere(Path4);
        FS.CreateUpfoldersPsysicallyUnlessThere(PathBase);

        if (PathSczAdmin != null)
        {
            FS.CreateUpfoldersPsysicallyUnlessThere(PathSczAdmin);
            FS.CreateUpfoldersPsysicallyUnlessThere(PathSczBaseAdmin);
        }

        //string cs4 = GetCs
        if (replaceMSinMSStoredProceduresI != null)
        {
            string cs2 = cs.Replace("MSStoredProceduresI", replaceMSinMSStoredProceduresI + "StoredProceduresI");
            TF.SaveFile(cs2, Path);
            if (PathSczAdmin != null)
            {
                TF.SaveFile(cs2, PathSczAdmin);
            }
            
        }
        else
        {
            TF.SaveFile(cs, Path);
            if (PathSczAdmin != null)
            {
                TF.SaveFile(cs, PathSczAdmin);
            }
        }

        if (PathSczBaseAdmin != null)
        {
            TF.SaveFile(csBase, PathSczBaseAdmin);
        }
        TF.SaveFile(csBase, PathBase);
        return "Uloženo do souboru " + Path;
    }

    /// <summary>
    /// Do A2 může být vloženo i Nope_, on si jej automaticky nahradí za SE
    /// </summary>
    /// <param name="nazevTabulky"></param>
    /// <param name="dbPrefix"></param>
    /// <returns></returns>
    public string GetCsTableRow4(string nazevTabulky, string dbPrefix)
    {
        string nazevCs;
        return GetCsTableRow4(nazevTabulky, dbPrefix, out nazevCs);
    }

    /// <summary>
    /// Generate base (TableRowAlbumBase)
    /// Do A2 může být vloženo i Nope_, on si jej automaticky nahradí za SE
    /// </summary>
    /// <param name="nazevTabulky"></param>
    /// <param name="dbPrefix"></param>
    /// <returns></returns>
    public string GetCsTableRowBase(string nazevTabulky, string dbPrefix)
    {
        string nazevCs;
        return GetCsTableRowBase(nazevTabulky, dbPrefix, out nazevCs);
    }

    /// <summary>
    /// Do A2 může být vloženo i Nope_, on si jej automaticky nahradí za SE
    /// </summary>
    /// <param name="nazevTabulky"></param>
    /// <param name="dbPrefix"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public string GetCsTableRowBase(string nazevTabulky, string dbPrefix, out string tableName)
    {
        string dbPrefix2 = dbPrefix;
        if (dbPrefix2 == "Nope_")
        {
            dbPrefix2 = "";
        }

        string nazevTabulkyCopy = SH.Copy(nazevTabulky);
        CSharpGenerator csg = new CSharpGenerator();

        if (nazevTabulky.StartsWith(dbPrefix))
        {
            nazevTabulky = nazevTabulky.Substring(dbPrefix.Length);
        }

        bool isDynamicTable = false;
        if (nazevTabulky.Contains("_"))
        {
            isDynamicTable = true;
            nazevTabulky = ConvertPascalConvention.ToConvention(nazevTabulky);
        }

        tableName = "TableRow" + nazevTabulky + "Base";
        csg.Using(usings);

        csg.StartClass(0, AccessModifiers.Public, false, tableName);

        if (isDynamicTable)
        {
            csg.Field(1, AccessModifiers.Private, false, VariableModifiers.None, "string", _tableNameField, true);
        }

        List<string> paramsForCtor = new List<string>(this.Count * 2);

        foreach (MSSloupecDB item in this)
        {
            
            string typ = MSDatabaseLayer.ConvertSqlDbTypeToDotNetType(item.Type2);

            if (typ == "string" && item.Delka.ToUpper() != "(MAX)")
            {
                string name = item.Name;
                if (item.Delka != string.Empty)
                {
                    name = name.Replace(item.Delka, string.Empty);
                }

                string nameLower = SH.FirstCharLower(name);

                // Public kvůli používání ve SunamoCzAdmin.Cmd a SunamoCzAdmin.Wpf
                csg.Field(1, AccessModifiers.Private, false, VariableModifiers.None, typ, nameLower, true);

                csg.Property(1, AccessModifiers.Public, false, typ, name, "return SH.Substring(" + nameLower + ",0," +  item.LengthWithoutBraces + ", true);", true, nameLower);

                paramsForCtor.Add(typ);
                paramsForCtor.Add(name);
            }
            else
            {
                string name = item.Name;

                // Public kvůli používání ve SunamoCzAdmin.Cmd a SunamoCzAdmin.Wpf
                csg.Field(1, AccessModifiers.Public, false, VariableModifiers.None, typ, name, true);
                paramsForCtor.Add(typ);
                paramsForCtor.Add(name);
            }
        }

        csg.Append(1, GenerateCtors(tableName, isDynamicTable, paramsForCtor, true));

        if (isDynamicTable)
        {
            csg.Property(1, AccessModifiers.Protected, false, "string", "TableName", true, false, _tableNameField);
        }
        else
        {
            csg.Property(1, AccessModifiers.Protected, false, "string", "TableName", true, false, "Tables." + dbPrefix2 + nazevTabulky);
        }

        CSharpGenerator innerParseRow = new CSharpGenerator();

        //innerParseRow.AppendLine(2, "base.o = o;");
        innerParseRow.AppendLine(2, "if (o != null)");

        innerParseRow.AppendLine(2, "{");
        for (int i = 0; i < this.Count; i++)
        {
            MSSloupecDB item = this[i];
            innerParseRow.AppendLine(3, Copy(item.Name) + " = MSTableRowParse." + ConvertSqlDbTypeToGetMethod(item.Type2) + "(o," + i.ToString() + ");");

        }
        // Na závěr každé metody nesmí být AppendLine
        innerParseRow.Append(2, "}");
        // Musí být public, protože když získám DataTable, jak ji mám pak co nejrychleji napasovat na nějaký objekt?
        csg.Method(2, "protected void ParseRow(object[] o)", innerParseRow.ToString());

        csg.EndBrace(0);

        return csg.ToString();
    }

    public static string Copy(string p)
    {
        return p;
    }

    public string GetCsTableRow(bool signed, string nazevTabulky, string dbPrefix)
    {
        string nazevCs;
        return GetCsTableRow(signed, nazevTabulky, dbPrefix, out nazevCs);
    }



    public bool IsOtherColumnID
    {
        get
        {
            return this[0].Name != "ID";
        }
    }

    /// <summary>
    /// Do A1 se dává prostě klíč ze slovníku, do A2 mss.ToString() + "_"
    /// </summary>
    /// <param name="nazevTabulky"></param>
    /// <param name="dbPrefix"></param>
    /// <returns></returns>
    public string GetNameTableRow(string nazevTabulky, string dbPrefix, out bool isDynamicTable, out string nazevTabulkyJC, out string dbPrefix2)
    {
        isDynamicTable = false;
        dbPrefix2 = dbPrefix;
        if (dbPrefix2 == "Nope_")
        {
            dbPrefix2 = "";
        }
        // OBSAHUJE I PREFIX, TAKŽE TŘEBA Koc_
        string nazevTabulkyCopy = SH.Copy(nazevTabulky);
        string niMethod = CSharpGenerator.AddTab(2, "throw new NotImplementedException();");
        
        // ZBAVÍM TABULKU nazevTabulky PREFIXU, ČILI NEOBSAHUJE NAPŘ. Koc_
        if (nazevTabulky.StartsWith(dbPrefix))
        {
            nazevTabulky = nazevTabulky.Substring(dbPrefix.Length);
        }
        if (nazevTabulky.Contains("_"))
        {
            isDynamicTable = true;
            nazevTabulky = ConvertPascalConvention.ToConvention(nazevTabulky);
        }
        nazevTabulkyJC = SH.ConvertPluralToSingleEn(nazevTabulky);
        string tableName = "TableRow" + nazevTabulky;
        return tableName; 
    }

    public string GetNameTableRow(string nazevTabulky, string dbPrefix)
    {
        bool isDynamicTable = false;
        string dbPrefix2 = "";
        string nazevTabulkyJC = "";
        return GetNameTableRow(nazevTabulky, dbPrefix, out isDynamicTable, out nazevTabulkyJC, out dbPrefix2);
    }

    /// <summary>
    /// Generate only derived(TableRowAlbum)
    /// Do A2 může být vloženo i Nope_, on si jej automaticky nahradí za SE
    /// </summary>
    /// <param name="nazevTabulky"></param>
    /// <param name="dbPrefix"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public string GetCsTableRow(bool signed, string nazevTabulky, string dbPrefix, out string tableName)
    {
        CSharpGenerator csg = new CSharpGenerator();
        
        // Zda první sloupec má jiný název než null
        bool isOtherColumnID = IsOtherColumnID;

        bool isDynamicTable = false;
        string dbPrefix2 = "";
        string nazevTabulkyJC = "";
        tableName = GetNameTableRow(nazevTabulky, dbPrefix, out isDynamicTable, out nazevTabulkyJC, out dbPrefix2);

        csg.Using(usings);
        //, "ITableRow<" + MSDatabaseLayer.ConvertSqlDbTypeToDotNetType(this[0].Type) + ">"
        string implements = tableName + "Base";
        string am = "public ";
        if (derived != null)
        {
            
            implements += "," + derived;
        }
        csg.StartClass(0, AccessModifiers.Public, false, tableName, implements);

        string seznamNameValue = "";
        List<string> nameFieldsFirstCharLower = new List<string>();
        List<string> allColumnsWithFirst = new List<string>();
        bool first = true;

        string sloupecID = null;
        string sloupecIDTyp = null;

        // Bude null, pokud sloupec nebude číselný typ
        Type typSloupecID = null;
        // Name of columns
        List<string> nameFields = new List<string>();

        // this is List<MSSloupecDB>
        foreach (MSSloupecDB item in this)
        {
            string typ = MSDatabaseLayer.ConvertSqlDbTypeToDotNetType(item.Type2);
            string name = item.Name;
            if (first)
            {
                first = false;
                if (name.StartsWith("ID") || name.StartsWith("Serie"))
                {
                    sloupecID = Copy(name);
                    sloupecIDTyp = typ;
                    typSloupecID = ConvertTypeNameTypeNumbers.ToType(typ);
                    
                }
                else
                {
                    // Je to například IDMisters
                    throw new Exception("V prvním sloupci není řádek ID nebo ID*");
                }
            }
            else
            {
                // Používá se při insert, 
                nameFields.Add(name);
                //nameFieldsFirstCharLower.Add(Copy(name));
            }
            allColumnsWithFirst.Add(name);
        }
        seznamNameValue = SH.Join(',', allColumnsWithFirst.ToArray());
        string seznamNameValueBezPrvniho = SH.Join(',', nameFields.ToArray());
        string nazvySloupcuBezPrvnihoVZavorkach = "(" + SH.Join(',', nameFields.ToArray()) + ")";

        /*
         * TableName je již s TableRow, který slouží k vytváření K
         * 
         * private static string NewMethod(string tableName, 
         * bool isDynamicTable, string nazevTabulkyCopy, string, List<string> paramsForCtor)
         */
        List<string> paramsForCtor = new List<string>(this.Count * 2);
        foreach (MSSloupecDB item in this)
        {
            string typ = MSDatabaseLayer.ConvertSqlDbTypeToDotNetType(item.Type2);
            string name = item.Name;

            paramsForCtor.Add(typ);
            paramsForCtor.Add(name);
        }

        csg.Append(2, GenerateCtors(tableName, isDynamicTable, paramsForCtor, false));

        

        #region Bez transakce
        
        CSharpGenerator innerSelectInTable = new CSharpGenerator();

        innerSelectInTable.AppendLine(2, "object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, \"" + sloupecID + "\", " + Copy(sloupecID) + ");" + @"
ParseRow(o);");
        csg.Method(1, "public void SelectInTable()", innerSelectInTable.ToString());

        string pridatDoNazvuMetody = "";
        if (sloupecIDTyp == "Guid")
        {
            pridatDoNazvuMetody = "Guid";
        }
        bool signed2 = false;
        if (signed)
        {
            if (typSloupecID != null)
            {
                signed2 = true;
            }
        }
        if (nameFields.Count == 0)
        {
            throw new Exception("Tabulka nemůže mít jen 1 sloupec.");
        }
        else
        {
            string pretypovaniInsert = "";
            if (typSloupecID != typeof(long))
            {
                pretypovaniInsert = "(" + sloupecIDTyp + ")";
            }

             CreateMethodInsert1(csg, am, sloupecID, typSloupecID, seznamNameValueBezPrvniho, signed2);

            string innerInsertToTable2 = CSharpGenerator.AddTab(2, sloupecID + "=(" + sloupecIDTyp + ")MSStoredProceduresI.ci.Insert2" + pridatDoNazvuMetody + "(TableName,\"" + sloupecID + "\",typeof(" + sloupecIDTyp + ")," + seznamNameValueBezPrvniho + ");");
            innerInsertToTable2 += CSharpGenerator.AddTab(2, "return " + sloupecID + ";");
            csg.Method(1, am + sloupecIDTyp + " InsertToTable2()", innerInsertToTable2);

            string innerInsertToTable3 = CSharpGenerator.AddTab(2, "MSStoredProceduresI.ci.Insert" + pridatDoNazvuMetody + "4(TableName, " + seznamNameValue + ");");
            csg.Method(1, am + "void InsertToTable3(" + sloupecIDTyp + " " + sloupecID + ")", innerInsertToTable3);


        }

        #region Metody které jsem odstranil aby dll nebyla tak velká
        if (derived == "ITableRowWordLong")
        {
            string nameVariable = "ID";
            csg.Property(1, AccessModifiers.Public, false, "long", SH.FirstCharLower(nameVariable), true, true, nameVariable);

            nameVariable = "Word";
            csg.Property(1, AccessModifiers.Public, false, "string", SH.FirstCharLower(nameVariable), true, true, nameVariable);
        }
        else if (derived == "ITableRowWordInt")
        {
            string nameVariable = "ID";
            csg.Property(1, AccessModifiers.Public, false, "int", SH.FirstCharLower(nameVariable), true, true, nameVariable);

            nameVariable = "Word";
            csg.Property(1, AccessModifiers.Public, false, "string", SH.FirstCharLower(nameVariable), true, true, nameVariable);
        }
        else if (derived == "ITableRowSearchIndexLong")
        {
            string nameVariable = "IDWord";
            csg.Property(1, AccessModifiers.Public, false, "long", SH.FirstCharLower(nameVariable), true, true, nameVariable);

            nameVariable = "TableChar";
            csg.Property(1, AccessModifiers.Public, false, "string", SH.FirstCharLower(nameVariable), true, true, nameVariable);

            nameVariable = "EntityID";
            csg.Property(1, AccessModifiers.Public, false, "int", SH.FirstCharLower(nameVariable), true, true, nameVariable);
        }
        else if (derived == "ITableRowSearchIndexInt")
        {
            string nameVariable = "IDWord";
            csg.Property(1, AccessModifiers.Public, false, "int", SH.FirstCharLower(nameVariable), true, true, nameVariable);

            nameVariable = "TableChar";
            csg.Property(1, AccessModifiers.Public, false, "string", SH.FirstCharLower(nameVariable), true, true, nameVariable);

            nameVariable = "EntityID";
            csg.Property(1, AccessModifiers.Public, false, "int", SH.FirstCharLower(nameVariable), true, true, nameVariable);
        }
        #endregion

        
        #endregion

        if (this.Count > 1)
        {
            MSSloupecDB sloupec = this[1];
            //&& !nazevTabulky.Contains("_")
            if (sloupec.Name == "Name"  && !isDynamicTable)
            {
                if (this[0].Name == "ID")
                {
                    csg.Method(1, "public static string Get" + nazevTabulkyJC + "Name(" + sloupecIDTyp + " id)", CSharpGenerator.AddTab(2, "return MSStoredProceduresI.ci.SelectNameOfID(Tables." +  nazevTabulky + ", id);"));
                }
                else
                {
                    //csg.Method("public static string Get" + nazevTabulkyJC + "Name(int id)", "return MSStoredProceduresI.ci.SelectValueOfIDOrSE(\"" + nazevTabulkyCopy + "\", id, \"" + this[0].Name + "\");");
                    csg.Method(1, "public static string Get" + nazevTabulkyJC + "Name(" + sloupecIDTyp + " id)", CSharpGenerator.AddTab(2, "return MSStoredProceduresI.ci.SelectNameOfID(Tables." +  nazevTabulky + ", id,\"" + this[0].Name + "\");"));
                }
            }
        }
        csg.EndBrace(0);

        return csg.ToString();
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="isDynamicTable"></param>
    /// <param name="_tableNameField"></param>
    /// <param name="paramsForCtor"></param>
    private static string GenerateCtors(string tableName, bool isDynamicTable, List<string> paramsForCtor2, bool isBase)
    {
        List<string> paramsForCtor = new List<string>(paramsForCtor2);
        CSharpGenerator csg2 = new CSharpGenerator();
        CSharpGenerator ctor1Inner = new CSharpGenerator();
        ctor1Inner.AppendLine(3, "ParseRow(o);");

        if (isBase && isDynamicTable)
        {
            csg2.Ctor(1, ModifiersConstructor.Public, tableName, "");
        }
        csg2.Ctor(1, ModifiersConstructor.Public, tableName, ctor1Inner.ToString(), "object[]", "o");

        List<string> paramsForCtorWithoutID = new List<string>();
        if (paramsForCtor.Count != 0)
        {
            paramsForCtorWithoutID = new List<string>(paramsForCtor);
            paramsForCtorWithoutID.RemoveAt(0);
            paramsForCtorWithoutID.RemoveAt(0);
            //}
        }

        for (int i = 0; i < paramsForCtorWithoutID.Count; i++)
        {
            if (i % 2 == 1)
            {
                paramsForCtorWithoutID[i] = Copy(paramsForCtorWithoutID[i]);
            }
        }
        if (isDynamicTable)
        {
            csg2.Ctor(1, ModifiersConstructor.Public, tableName, true, isBase, "string", _tableNameField);
        }
        else
        {
            csg2.Ctor(1, ModifiersConstructor.Public, tableName, false, isBase);
        }
        if (paramsForCtorWithoutID.Count != 0)
        {
            if (isDynamicTable)
            {
                paramsForCtorWithoutID.Insert(0, _tableNameField);
                paramsForCtorWithoutID.Insert(0, "string");
                csg2.Ctor(1, ModifiersConstructor.Public, tableName, true, isBase, paramsForCtorWithoutID.ToArray());
            }
            else
            {
                csg2.Ctor(1, ModifiersConstructor.Public, tableName, true, isBase, paramsForCtorWithoutID.ToArray());
            }
        }
        return csg2.ToString();
    }

    public static string ConvertSqlDbTypeToGetMethod(SqlDbType p)
    {
        switch (p)
        {
            case SqlDbType.Text:
            case SqlDbType.Char:
            case SqlDbType.NText:
            case SqlDbType.NChar:
            case SqlDbType.NVarChar:
                return "GetString";
                
            case SqlDbType.Int:
                return "GetInt";
                
            case SqlDbType.Real:
                return "GetFloat";
                
            case SqlDbType.BigInt:
                return "GetLong";
                
            case SqlDbType.Bit:
                return "GetBool";
                
            case SqlDbType.Date:
            case SqlDbType.DateTime:
            case SqlDbType.DateTime2:
            case SqlDbType.Time:
            case SqlDbType.DateTimeOffset:
            case SqlDbType.SmallDateTime:
                return "GetDateTime";
            // Bude to až po všech běžně používaných datových typech, protože bych se měl vyvarovat ukládat do malé DB takové množství dat
            case SqlDbType.Timestamp:
            case SqlDbType.Binary:
            case SqlDbType.VarBinary:
            case SqlDbType.Image:
                return "GetImage";
                
            case SqlDbType.SmallMoney:
            case SqlDbType.Money:
            case SqlDbType.Decimal:
                return "GetDecimal";
                
            case SqlDbType.Float:
                return "GetDouble";
                
            case SqlDbType.SmallInt:
                return "GetShort";
                
            case SqlDbType.TinyInt:
                return "GetByte";
                
            case SqlDbType.Structured:
            case SqlDbType.Udt:
            case SqlDbType.Xml:
                throw new Exception("Snažíte se převést na int strukturovaný(složitý) datový typ");
                
            case SqlDbType.UniqueIdentifier:
                return "GetGuid";
                

            case SqlDbType.VarChar:
                return "GetString";
                
            case SqlDbType.Variant:
                return "GetObject";
                
            default:
                throw new Exception("Snažíte se převést datový typ, pro který není implementována větev");
                
        }
    }



    public string GetCsEntityView(string table, string dbPrefix, string nameOfVariable)
    {
        CSharpGenerator csg = new CSharpGenerator();
        CSharpGenerator csgDisplayInfo = new CSharpGenerator();
        string nvc = SH.Copy(nameOfVariable);
        string nultyParametr = "";
        bool numero = false;

        foreach (MSSloupecDB item in this)
        {
            numero = false;
            nultyParametr = "";
            nameOfVariable = SH.Copy(nvc);
            switch (item.Type2)
            {
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.UniqueIdentifier:
                case SqlDbType.VarChar:
                    // Nechat tak, už by měl být string, akorát u UniqueIdentifier si toho nejsem úplně jistý ale to by šlo vylepšit.
                    nameOfVariable += "." + item.Name;
                    break;
                case SqlDbType.SmallInt:
                case SqlDbType.SmallMoney:
                case SqlDbType.Int:
                case SqlDbType.Money:
                case SqlDbType.Decimal:
                case SqlDbType.TinyInt:
                case SqlDbType.BigInt:
                    numero = true;
                    nameOfVariable = nameOfVariable + "." + item.Name;// + ".ToString()";
                    break;
                case SqlDbType.Char:
                    // Nechat tak, když se bude jednat o ID(ve většině případů) tak se to bude muset ještě projet nějakou metodou, protože Kompilátor to podtrhne a vyhodí chybu
                    nameOfVariable = nameOfVariable + "." + item.Name;// + ".ToString()";
                    break;

                case SqlDbType.Bit:
                    nameOfVariable += "." + item.Name;
                    // Nechat tak, kompilátor vyhodí chybu, uživatel musí nastavit vlastní metodu
                    break;
                case SqlDbType.Date:
                    nultyParametr = nameOfVariable + "." + item.Name + ",";
                    nameOfVariable = nameOfVariable + "." + item.Name + ".ToShortDateString()";
                    break;
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.SmallDateTime:
                    nultyParametr = nameOfVariable + "." + item.Name + ",";
                    nameOfVariable = nameOfVariable + "." + item.Name + ".ToString()";
                    break;
                case SqlDbType.Time:
                    nultyParametr = nameOfVariable + "." + item.Name + ",";
                    nameOfVariable = nameOfVariable + "." + item.Name + ".ToShortTimeString()";
                    break;
                case SqlDbType.DateTimeOffset:
                case SqlDbType.Timestamp:
                    throw new NotSupportedException("Datový typ DateTimeOffset a Timestamp není podporován.");

                case SqlDbType.Real:
                case SqlDbType.Float:
                    nameOfVariable = nameOfVariable + "." + item.Name + ".ToString()";
                    break;

                case SqlDbType.Image:
                case SqlDbType.Binary:
                case SqlDbType.VarBinary:
                    throw new Exception("Not supported convert binary data to string");
                    


                case SqlDbType.Structured:
                    throw new NotSupportedException("Strukturované datové typy nejsou podporovány.");
                    

                case SqlDbType.Udt:
                    throw new NotSupportedException("Univerzální datové typy nejsou podporovány.");
                    
                case SqlDbType.Variant:
                    throw new NotSupportedException("Variantní datové typy nejsou podporovány.");
                    
                case SqlDbType.Xml:
                    throw new NotSupportedException("Xml datový typ není podporován");
                    
                default:
                    break;
            }
            if (nultyParametr == "")
            {
                if (numero)
                {
                    csgDisplayInfo.If(3, nameOfVariable + " != -1");
                }
                csgDisplayInfo.AppendLine(0, CSharpGenerator.AddTab(4, "SetP(" + nameOfVariable + ", lbl" + item.Name + ", p" + item.Name + ");"));
                if (numero)
                {
                    csgDisplayInfo.EndBrace(3);
                    csgDisplayInfo.Else(3);
                    csgDisplayInfo.AppendLine(0, CSharpGenerator.AddTab(4, "p" + item.Name + ".Visible = false;"));
                    csgDisplayInfo.EndBrace(3);
                }
            }
            else
            {
                csgDisplayInfo.AppendLine(3, "SetPDateTime(" + nultyParametr + nameOfVariable + ", lbl" + item.Name + ", p" + item.Name + ");");
            }
        }

        csg.Method(2, "public void DisplayInfo()", csgDisplayInfo.ToString());

        csg.Method(2, "private void SetP(string p, HtmlGenericControl lblName, HtmlGenericControl pName)",
CSharpGenerator.AddTab(3, @"string t = p.Trim();
        if (t != " + "\"\"" + @")
        {
            lblName.InnerHtml = t;
            pName.Visible = true;
        }
        else
        {
            pName.Visible = false;
        }"));

        csg.Method(2, "protected void SetPDateTime(DateTime dt, string p, HtmlGenericControl lblName, HtmlGenericControl pName)",
CSharpGenerator.AddTab(3, @"if ((dt.Day == 31 && dt.Month == 12 && dt.Year == 9999) || (dt.Hour == 23 || dt.Minute == 59))
{
            pName.Visible = false;
            return;
        }
        string t = p.Trim();
        if (t != " + "\"\"" + @")
        {
            lblName.InnerHtml = t;
            pName.Visible = true;
        }
        else
        {
            pName.Visible = false;
        }"));

        csg.Method(2, "private void SetVisible(bool b)", CSharpGenerator.AddTab(3, @"divButtons.Visible = b;
        h1.Visible = b;
        entityInfo.Visible = b;"));

        return csg.ToString();
    }

    public string GetHtmlEntityView(string table, string dbPrefix)
    {
        HtmlGenerator hg = new HtmlGenerator();

        hg.WriteTagWithAttrs("h1", "id", "h1", "runat", "server");
        hg.TerminateTag("h1");

        hg.WriteTagWithAttrs("div", "class", "tl");

        hg.WriteTagWithAttrs("div", "runat", "server", "id", "divButtons");
        hg.StartComment();
        hg.WriteNonPairTagWithAttrs("asp:Button", "runat", "server", "Text", "", "CssClass", "button", "ID", "btnChs");
        hg.EndComment();
        hg.TerminateTag("div");

        hg.WriteTagWithAttrs("div", "runat", "server", "id", "entityInfo");

        foreach (MSSloupecDB item in this)
        {
            string n = item.Name;
            hg.WriteTagWithAttrs("p", "id", "p" + n, "runat", "server");
            hg.WriteElement("b", "abc");
            hg.WriteBr();
            hg.WriteTagWithAttrs("span", "runat", "server", "id", "lbl" + n);
            hg.TerminateTag("span");
            hg.TerminateTag("p");
        }

        hg.TerminateTag("div");

        hg.TerminateTag("div");

        return hg.ToString();
    }

    public SqlCommand GetSqlCreateTable(string nazevTabulky)
    {
        return GetSqlCreateTable(nazevTabulky, false);
    }

    public static MSColumnsDB IDName(int p)
    {
        return new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(" + p.ToString() + ")", false, true)
            );
    }

    public static MSColumnsDB IDNameShort(int p)
    {
        return new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.SmallInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(" + p.ToString() + ")", false, true)
            );
    }

    public static MSColumnsDB IDNameTinyInt(int p)
    {
        return new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.TinyInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(" + p.ToString() + ")", false, true)
            );
    }

    public static MSColumnsDB IntInt(string c1, string c2)
    {
        return new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, c1),
            MSSloupecDB.CI(SqlDbType2.Int, c2)
        );
    }
}