﻿using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
public static partial class CSharpHelper
{
    public static string CreateConstsForSearchUris(List<string> uris)
    {
        CSharpGenerator csg = new CSharpGenerator();
        List<string> all = new List<string>();

        foreach (var item in uris)
        {
            Uri u = new Uri(item);
            string name = ConvertPascalConvention.ToConvention(u.Host);
            csg.Field(2, AccessModifiers.Public, true, VariableModifiers.Mapped, "string", name, true, item);
            all.Add(name);
        }

        csg.List(2, "string", "All", all, false);

        return csg.ToString();
    }

    public static string DefaultValueForType(string type)
    {
        if (type.Contains("."))
        {
            type = ConvertTypeShortcutFullName.ToShortcut(type);
        }
        switch (type)
        {
            case "string":
                return "\"\"";
            case "bool":
                return "false";
            case "float":
            case "double":
            case "int":
            case "long":
            case "short":
            case "decimal":
            case "sbyte":
                return "-1";
            case "byte":
            case "ushort":
            case "uint":
            case "ulong":
                return "0";
            case "DateTime":
                // Původně tu bylo MinValue kvůli SQLite ale dohodl jsem se že SQLite už nebudu používat a proto si ušetřím v kódu práci s MSSQL 
                return "SqlServerHelper.DateTimeMinVal";
            case "byte" + "[]":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return "null";
            case "Guid":
                return "Guid.Empty";
            case "char":
                throw new Exception("Nepodporovaný typ");
        }
        throw new Exception("Nepodporovaný typ");
    }

    public static string DefaultValueForTypeSqLite(string type)
    {
        if (type.Contains(AllStrings.dot))
        {
            type = ConvertTypeShortcutFullName.ToShortcut(type);
        }

        switch (type)
        {
            case "TEXT":
                return AllStrings.qm;
            case "INTEGER":
                return int.MaxValue.ToString();
            case "REAL":
                return "0.0";
            case "DATETIME":
                // Původně tu bylo MinValue kvůli SQLite ale dohodl jsem se že SQLite už nebudu používat a proto si ušetřím v kódu práci s MSSQL 
                return "DateTime.MinValue";
            case "BLOB":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return "null";
            default:
                throw new Exception("Nepodporovaný typ v metodě DefaultValueForTypeSqLite");
        }

        throw new Exception("Nepodporovaný typ");
    }

    public static string GetDictionaryStringObject<Value>(List<string> keys, List<Value> values, string nameDictionary, bool checkForNull)
    {
        int pocetTabu = 0;
        CSharpGenerator gen = new CSharpGenerator();
        gen.Region(pocetTabu, "Airplane companies");
        if (checkForNull)
        {
            gen.If(pocetTabu, nameDictionary + " " + "== null");
        }

        gen.DictionaryStringObject(pocetTabu, nameDictionary, DictionaryHelper.GetDictionary<string, Value>(keys, values));
        if (checkForNull)
        {
            gen.EndBrace(pocetTabu);
        }

        gen.EndRegion(pocetTabu);
        string result = gen.ToString();
        return result;
    }

    public static string GetCtorInner(int tabCount, IEnumerable values)
    {
        const string assignVariable = "this.{0} = {0};";
        CSharpGenerator csg = new CSharpGenerator();
        foreach (var item in values)
        {
            csg.AppendLine(tabCount, SH.Format2(assignVariable, item));
        }

        return csg.ToString().Trim();
    }

    /// <summary>
    /// Return also List because Array isn't use
    /// </summary>
    /// <param name = "input"></param>
    /// <param name = "arrayName"></param>
    /// <returns></returns>
    public static string GetArray(List<string> input, string arrayName)
    {
        CSharpGenerator generator = new CSharpGenerator();
        generator.List(0, "string", arrayName, input);
        return generator.ToString();
    }

    public static string GetList(List<string> input, string listName)
    {
        CSharpGenerator generator = new CSharpGenerator();
        generator.List(0, "string", listName, input);
        return generator.ToString();
    }

    public static List<string> RemoveRegions(List<string> lines)
    {
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            string line = lines[i].Trim();
            if (line.StartsWith("#" + "region" + " ") || line.StartsWith("#endregion"))
            {
                lines.RemoveAt(i);
            }
        }

        return lines;
    }

    public static string GenerateConstants(int tabCount, Func<string, string> changeInput, List<string> input)
    {
        CSharpGenerator csg = new CSharpGenerator();
        foreach (var item in input)
        {
            var name = changeInput(item);

            csg.Field(tabCount, AccessModifiers.Public, true, VariableModifiers.Mapped, "string", name, true, string.Empty);
        }

        return csg.ToString();
    }
}