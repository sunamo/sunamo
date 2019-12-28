using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSQL;

public partial class SqlServerHelper
{
    static Type type = typeof(SqlServerHelper);

    public static bool GetTableAndColumn(string sql, ref string table, ref string column, int serie)
    {
        bool result = true;
        var p = SH.GetFirstWord(sql).ToLower();

        if (p == "update")
        {
            Update(sql, ref table, ref column, serie);
        }
        else if (p == "insert")
        {
            Insert(sql, ref table, ref column, serie);
        }
        else if(p == "select")
        {
            return false;
        }
        else if(p == "delete")
        {
            return false;
        }
        else
        {
            ThrowExceptions.NotImplementedCase(type, RH.CallingMethod());
        }

#if DEBUG
        DebugLogger.DebugWriteLine("SQL: " + sql);
        DebugLogger.DebugWriteLine("Table: {0}, Column: {1}", table, column);
        DebugLogger.DebugWriteLine("---");
#endif

        if (column == null)
        {

        }
        if (table == null)
        {

        }

        return result;
    }

    private static void Insert(string sql, ref string table, ref string column, int serie)
    {
        var p = TSQLStatementReader.ParseStatements(sql);

        // yet in first parameter is two in from property, there is two elements with text property: 1) from 2) table
        foreach (var item in p)
        {
            var tokens = item.Tokens;
            int i = 0;
            InsertPosition updatePosition = InsertPosition.Begin;
            var duo = 0;
            List<string> columnNames = new List<string>();

            foreach (var item2 in tokens)
            {

                var t1 = item2.GetType().ToString();
                var t2 = typeof(TSQL.Tokens.TSQLIdentifier).ToString();
                if (t1 == t2)
                {
                    if (table == null)
                    {
                        table = item2.Text;
                    }
                    else
                    {
                        columnNames.Add(item2.Text);
                    }
                }

                if (serie < columnNames.Count && table != null)
                {
                    column = columnNames[serie];
                }
                i++;
            }
        }

        if (column == null)
        {
            if (isNVarChar.Count != 0)
            {
                column = isNVarChar[table][serie].Name;
            }

            
        }



    }

    public static Dictionary<string, MSColumnsDB> isNVarChar = new Dictionary<string, MSColumnsDB>();

    private static void Update(string sql, ref string table, ref string column, int serie)
    {
        var p = TSQLStatementReader.ParseStatements(sql);

        // yet in first parameter is two in from property, there is two elements with text property: 1) from 2) table
        foreach (var item in p)
        {
            var tokens = item.Tokens;
            int i = 0;
            UpdatePosition updatePosition = UpdatePosition.Begin;
            var duo = 0;
            List<string> columnNames = new List<string>();

            foreach (var item2 in tokens)
            {
                if (table == null)
                {
                    var t1 = item2.GetType().ToString();
                    var t2 = typeof(TSQL.Tokens.TSQLIdentifier).ToString();
                    if (t1 == t2)
                    {
                        table = item2.Text;
                    }
                }

                var l = item2.Text.ToLower();

                if (l == "set")
                {
                    updatePosition = UpdatePosition.Set;

                    continue;
                }

                if (updatePosition == UpdatePosition.Set)
                {
                    var c = duo % 2 == 0;
                    if (c)
                    {
                        columnNames.Add(item2.Text);

                    }
                }

                //if (l != "from")
                //{
                //    table = item2.Text;
                //}

                if (serie < columnNames.Count && table != null)
                {
                    column = columnNames[serie];
                }

                i++;
            }
        }
    }
    public static string ConvertToVarChar(string maybeUnicode, ConvertToVarcharArgs e = null)
    {
        bool args = e != null;
        StringBuilder sb = new StringBuilder();
        foreach (var item in maybeUnicode)
        {
            var b1 = s_availableCharsInVarCharWithoutDiacriticLetters.Contains(item);
            var b2 = SH.diacritic.IndexOf(item) != -1;

            if (b1 || b2)
            {
                sb.Append(item);
            }
            else
            {
                string before = item.ToString();
                // Is use Diacritics package which allow pass only string, not char
                var after = SH.TextWithoutDiacritic(before);
                // if wont be here !char.IsWhiteSpace(item), it will strip newlines
                var b3 = before != after;
                var b4 = char.IsWhiteSpace(item);
                if (b3 || b4)
                {
                    if (b3)
                    {
                        if (args)
                        {
                            DictionaryHelper.AddOrCreateIfDontExists<string, string>(e.changed, before, after);
                        }
                    }
                    sb.Append(after);
                }
                else
                {
                    if (args)
                    {
                        if (e.notSupportedChars != null)
                        {
                            e.notSupportedChars.Add(item);
                        }
                    }
                }
                
            }
        }

        var vr = SH.ReplaceAll(sb.ToString(), AllStrings.space, AllStrings.doubleSpace);
        return vr;
    }
}