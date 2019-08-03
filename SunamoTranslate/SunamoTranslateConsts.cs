using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sunamo.Values;

public class SunamoTranslateConsts
{
    

    public static void InitializeNotTranslateAble()
    {

    }

    static SunamoTranslateConsts()
    {
        allBasicTypes = Consts.allBasicTypes.Select(d => d.Name).ToList();
        allBasicTypesFull = Consts.allBasicTypes.Select(d => d.FullName).ToList();
    }

    public static PpkOnDrive stringsNotToTranslate = new PpkOnDrive(AppData.ci.GetFile(AppFolders.Settings, "notToTranslateStrings.txt"));

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
}

