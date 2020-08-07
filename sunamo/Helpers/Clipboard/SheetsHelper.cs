using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using sunamo.Collections;
using sunamo.Essential;

public class SheetsHelper
{
    public static string SwitchRowsAndColumn(string s)
    {
        List<List<string>> exists = new List<List<string>>();

        var l = SH.GetLines(s);
        foreach (var item in l)
        {
            exists.Add(GetRowCells(item));
        }

        ValuesTableGrid<string> t = new ValuesTableGrid<string>(exists);
        DataTable dt = t.SwitchRowsAndColumn();

        return DataTableToString(dt);
    }

    public static string DataTableToString(DataTable s)
    {
        StringBuilder sb = new StringBuilder();

        foreach (DataRow item in s.Rows)
        {
            sb.AppendLine(JoinForGoogleSheetRow(item.ItemArray));
        }

        return sb.ToString();
    }

    public static List<string> GetRowCells(string ClipboardS)
    {
        return SplitFromGoogleSheets(ClipboardS);
    }

    /// <summary>
    /// If null, will be  load from clipboard
    /// </summary>
    /// <param name="input"></param>
    public static List<string> Rows(string input = null)
    {
        if (input == null)
        {
            input = ClipboardHelper.GetText();
        }

        return SH.Split(input, "\n");
    }

    /// <summary>
    /// A1 can be null
    /// </summary>
    /// <param name="input"></param>
    /// <param name=""></param>
    /// <returns></returns>
    public static List<string> SplitFromGoogleSheetsRow(string input, bool removeEmptyElements )
    {
        var r =SplitFromGoogleSheets(input);
        if (removeEmptyElements)
        {
            CA.RemoveStringsEmpty2(r);
        }
        return r;
    }

    /// <summary>
    /// If A1 null, take from clipboard
    /// </summary>
    /// <param name="input"></param>
    public static List<string> SplitFromGoogleSheets(string input = null)
    {
        if (input == null)
        {
            input = ClipboardHelper.GetText();
        }

        var bm = SH.TabOrSpaceNextTo(input);
        List<string> vr = new List<string>();

        if (bm.Count > 0)
        {
            vr.AddRange( SH.SplitByIndexes(input, bm));

            vr.Reverse();
        }
        else
        {
            ThisApp.SetStatus(TypeOfMessage.Warning, "Bad data in clipboard");
        }
        //var vr = SH.Split(input, AllStrings.tab);
        return vr;
    }

    public static void JoinForGoogleSheetRow(StringBuilder sb, IEnumerable en)
    {
        CA.JoinForGoogleSheetRow(sb, en);
    }

    public static string JoinForGoogleSheetRow(IEnumerable en)
    {
        return CA.JoinForGoogleSheetRow(en);
    }

    /// <summary>
    /// Take data from clipboard
    /// </summary>
    private static List<string> GetRowCells()
    {
        return GetRowCells(ClipboardHelper.GetText());
    }
}