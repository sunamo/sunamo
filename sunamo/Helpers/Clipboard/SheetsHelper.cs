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

    public static string CalculateMedianAverage(string input, bool mustBeAllNumbers = true)
    {
        var defDouble = -1;
        var list = CA.ToNumber<double>(BTS.ParseDouble, SheetsHelper.SplitFromGoogleSheets(input), defDouble, false);

        return NH.CalculateMedianAverage(list);
    }

    public static string CalculateMedianFromTwoRows(string s)
    {
        var r = SheetsHelper.Rows(s);
        for (int i = 0; i < r.Count; i++)
        {
            r[i] = CalculateMedianAverage(r[i]);
        }
        return SH.JoinNL(r);
    }

    public static List<List<string> > AllLines(string d)
    {
        List<List<string>> result = new List<List<string>>();
        var l = SH.GetLines(d);
        foreach (var item in l)
        {
            result.Add(GetRowCells(item));
        }
        return result;
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
    /// Not to parse whole content 
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