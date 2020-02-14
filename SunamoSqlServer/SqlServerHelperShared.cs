using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSQL;

public partial class SqlServerHelper
{
    static Type type = typeof(SqlServerHelper);

    

    /// <summary>
    /// Update - return always first element
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <param name="serie"></param>
    /// <returns></returns>
    public static bool GetTableAndColumn(string sql, ref string table, ref string column, int serie)
    {
        List<int> indexesOfVarCharOrChar = new List<int>();
        List<string> str = new List<string>();
        var res = GetTableAndColumns(sql, ref table, ref str, serie, indexesOfVarCharOrChar);
        if (str.Count() > 0)
        {
            column = str[0];
        }
        return res;
    }

    public static bool GetTableAndColumns(string sql, ref string table, ref List< string> columns, int serie, List<int> indexesOfVarCharOrChar)
    {
        columns.Clear();
         table = null;

        bool result = true;
        var p = SH.GetFirstWord(sql).ToLower();

        if (p == "update")
        {
            Update(sql, ref table, ref columns, serie);
        }
        else if (p == "insert")
        {
            Insert(sql, ref table, ref columns, serie);
        }
        else if(p == "select")
        {
            if (table != null)
            {
                var mscDb = isNVarChar[table];
                var dict = mscDb.dict;
                if (columns.Count == 1 && columns[1] == AllStrings.asterisk)
                {

                    columns.Clear();
                    columns.AddRange(dict.Select(d => d.Value.Name));
                    for (int i = 0; i < columns.Count; i++)
                    {
                        var column = mscDb[i];
                        if (column.Type == SqlDbType2.VarChar || column.Type == SqlDbType2.Char)
                        {
                            indexesOfVarCharOrChar.Add(i);
                        }
                    }
                }
                else
                {
                    //var dxs = new List<int>();
                    //foreach (var item in columns)
                    //{
                    //    dict[item].id
                    //}
                }
            }

            return false;
        }
        else if(p == "delete")
        {
            return false;
        }
        else
        {
            ThrowExceptions.NotImplementedCase(type, RH.CallingMethod(), p);
        }

#if DEBUG
        ////DebugLogger.DebugWriteLine("SQL: " + sql);
        ////DebugLogger.DebugWriteLine("Table: {0}, Column: {1}", table, column);
        ////DebugLogger.DebugWriteLine("---");
#endif

        //column = column.Trim();
        table = table.Trim();

        return result;
    }

    private static void Insert(string sql, ref string table, ref List< string> columns, int serie)
    {
        string column = null;
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

        columns.Add(column);
    }

    /// <summary>
    /// In key is name of table
    /// </summary>
    public static Dictionary<string, MSColumnsDB> isNVarChar = new Dictionary<string, MSColumnsDB>();

    private static void Update(string sql, ref string table, ref List<string> columns, int serie)
    {
        string column = null;

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

                        if (serie != 0)
                        {
                            /* U UPDATE Lyr_YoutubeVideos SET CodeYT=@p2  WHERE  CodeYT = @p0  AND  IDSong = @p1  je první @2. 
                             * Tím pádem mi vrátí @2 protože je druhý v pořadí. Myslím že zde u update to mohu ignorovat
                            */
                            column = columnNames[0];
                            break;
                        }
                    }
                }

                if (serie < columnNames.Count && table != null)
                {
                    column = columnNames[serie];
                    break;
                }

                i++;
            }
        }

        columns.Add(column);
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