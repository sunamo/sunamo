using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSQL;

public partial class SqlServerHelper
{
    public static List<char> s_availableCharsInVarCharWithoutDiacriticLetters = null;
    public static List<char> allowedInPassword = CA.ToList<char>('!', '@', '#', '$', '%', '^', '&', '*', '?', '_', '~');
    static SqlServerHelper()
    {
        s_availableCharsInVarCharWithoutDiacriticLetters = new List<char>(new char[] { AllChars.colon, AllChars.space, AllChars.dash, AllChars.dot, AllChars.comma, AllChars.sc, AllChars.excl, AllChars.bs, AllChars.lb, AllChars.rb, AllChars.rsqb, AllChars.lsqb, AllChars.lcub, AllChars.rcub, AllChars.plus, AllChars.percnt, AllChars.lowbar, AllChars.slash, AllChars.bs, AllChars.lt, AllChars.gt, AllChars.apostrophe });
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(AllChars.lowerChars);
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(AllChars.upperChars);
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(AllChars.numericChars);
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(new char[] { '\u2013', '\u2014', '\u2026', '\u201E', '\u201C', '\u201A', '\u2018', '\u00BB', '\u00AB', '\u2019', '\u201D', '\u00B0', '\u02C7', '\u00A8', '\u00A4', '\u00F7', '\u00D7', '\u02DD' });
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(new char[] { '~', '@', '#', '$', '^', '&', '=', '|' });


        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(allowedInPassword);
        CA.RemoveDuplicitiesList<char>(s_availableCharsInVarCharWithoutDiacriticLetters);
    }

    public const String diacritic = "\u00E1\u010D\u010F\u00E9\u011B\u00ED\u0148\u00F3\u0161\u0165\u00FA\u016F\u00FD\u0159\u017E\u00C1\u010C\u010E\u00C9\u011A\u00CD\u0147\u00D3\u0160\u0164\u00DA\u016E\u00DD\u0158\u017D";

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

    private static void Insert(string sql, ref string table, ref List<string> columns, int serie)
    {
        string column = null;


        var p = TSQLStatementReader.ParseStatements(sql);

        // yet in first parameter is two in from property, there is two elements with text property: 1) from 2) table
        foreach (var item in p)
        {
            var tokens = item.Tokens;
            int i = 0;
            //InsertPosition updatePosition = InsertPosition.Begin;
            //var duo = 0;
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
                // Is saved to them in AddLayer
                // Must call Copy To be actualized
                column = isNVarChar[table][serie].Name;
            }
        }

        columns.Add(column);
    }

    /// <summary>
    /// In key is name of table
    /// </summary>
    public static Dictionary<string, MSColumnsDB> isNVarChar = new Dictionary<string, MSColumnsDB>();

    public static bool GetTableAndColumns(string sql, ref string table, ref List<string> columns, int serie, List<int> indexesOfVarCharOrChar)
    {
        columns.Clear();
        table = null;

        bool result = true;
        var p = SH.GetFirstWord(sql).ToLower();

        if (p == SqlConsts.update)
        {
            Update(sql, ref table, ref columns, serie);
        }
        else if (p == SqlConsts.insert)
        {
            Insert(sql, ref table, ref columns, serie);
        }
        else if (p == SqlConsts.select)
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
        else if (p == SqlConsts.delete)
        {
            return false;
        }
        else
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), p);
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

    static Type type = typeof(SqlServerHelper);

    /// <summary>
    /// Update - return always first element
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <param name="serie"></param>
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

    public static string SqlCommandToTSQLText(SqlCommand cmd)
    {
        StringBuilder query = new StringBuilder(cmd.CommandText);

        foreach (SqlParameter p in cmd.Parameters)
        {
            query = query.Replace(p.ParameterName, p.Value.ToString());
        }

        return query.ToString();
    }

    public static bool IsNull(object o)
    {
        return o == null || o == DBNull.Value;
    }

    public static bool IsNullOrEmpty(object o)
    {
        if (IsNull(o))
        {
            return true;
        }
        return o.ToString().Trim() == string.Empty;
    }

    public static Tuple<int, int> UnnormalizeNumber(int serie)
    {
        const int increaseAbout = 1000; 

        int l = int.MinValue;
        int h = l + increaseAbout;

        for (int i = 0; i < serie; i++)
        {
            l += increaseAbout;
            h += increaseAbout;
        }

        Tuple<int, int> d = new Tuple<int, int>(l, h);
        return d;
    }
}