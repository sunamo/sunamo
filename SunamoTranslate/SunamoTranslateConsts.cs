using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sunamo.Values;

public class SunamoTranslateConsts
{
    public static List<string> notTranslateAbleLines = null;

    public static void InitializeNotTranslateAble()
    {
        if (SunamoTranslateConsts.notTranslateAbleLines == null)
        {
            var lines2 = SH.GetLines(SunamoTranslateConsts.notTranslateAbleStrings);
            CA.RemoveDuplicitiesList<string>(lines2);
            SunamoTranslateConsts.notTranslateAbleLines = new List<string>(lines2.Count);
            SunamoTranslateConsts.notTranslateAbleLines.AddRange(lines2);
        }
    }

    static SunamoTranslateConsts()
    {
        allBasicTypes = Consts.allBasicTypes.Select(d => d.Name).ToList();
        allBasicTypesFull = Consts.allBasicTypes.Select(d => d.FullName).ToList();
    }

    public static List<string> sqlKeywords
    {
        get
        {
            return new List<string>(_sqlKeywords);
        }
    }

    public static List<string> allBasicTypes = null;
    public static List<string> allBasicTypesFull = null;



    /// <summary>
    /// Every here have to be delimited in code by space
    /// none string can contains space (event inner join and so)
    /// NO (,) and other non letter chars
    /// </summary>
    static List<string> _sqlKeywords = CA.ToListString("sum", "id", "object", "objectproperty", "select", "in", "update", "delete", "insert", "inner", "join", "from", "group", "by", "top", "sysobjects", "*", "where", "and", "exec", "set", "newid");

    /// <summary>
    /// Must be automatically 
    /// </summary>
    public const string notTranslateAbleStrings = @"long
Word
string
Word
string
long
string
string
, lbl
runat
Text
Name(
return
Guid
public
Serie
args

(null)
";
}

