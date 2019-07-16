using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using sunamo.Collections;

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
        return SplitFromGoogleSheets();
    }

    /// <summary>
    /// If null, will be  load from clipboard
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static List<string> Rows(string input = null)
    {
        if (input == null)
        {
            input = ClipboardHelper.GetText();
        }

        return SH.Split(input, "\n");
    }

    /// <summary>
    /// If A1 null, take from clipboard
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static List<string> SplitFromGoogleSheets(string input = null)
    {
        if (input == null)
        {
            input = ClipboardHelper.GetText();
        }

        var vr = SH.Split(input, AllStrings.tab);
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
    /// <returns></returns>
    private static List<string> GetRowCells()
    {
        return GetRowCells(ClipboardHelper.GetText());
    }
}

