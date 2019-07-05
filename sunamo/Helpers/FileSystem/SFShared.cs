﻿using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using sunamo.Data;
using System.Collections;
using System.Linq;
using sunamo.Constants;

public static partial class SF
{
    

/// <summary>
    /// If need to combine string and IEnumerable, lets use CA.Join
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="o"></param>
    /// <returns></returns>
    public static string PrepareToSerializationExplicit(char p1, IEnumerable o)
    {
        string vr = SH.GetString(o, p1.ToString());
        return vr.Substring(0, vr.Length - 1);
    }

public static List<List<string>> GetAllElementsFileAdvanced(string file, out List<string> hlavicka, char oddelovaciZnak = AllChars.pipe)
    {
        string oz = oddelovaciZnak.ToString();
        List<List<string>> vr = new List<List<string>>();
        string[] lines = File.ReadAllLines(file);
        lines = CA.Trim(lines);
        hlavicka = SF.GetAllElementsLine(lines[0], AllChars.bs);
        int musiByt = SH.OccurencesOfStringIn(lines[0], oz);
        int nalezeno = 0;
        StringBuilder jedenRadek = new StringBuilder();
        for (int i = 1; i < lines.Length; i++)
        {
            nalezeno += SH.OccurencesOfStringIn(lines[i], oz);
            jedenRadek.AppendLine(lines[i]);
            if (nalezeno == musiByt)
            {
                nalezeno = 0;
                var columns = SF.GetAllElementsLine(jedenRadek.ToString(), AllChars.bs);
                jedenRadek = new StringBuilder();
                vr.Add(columns);
            }
        }

        return vr;
    }

/// <summary>
    /// Get all elements from A1
    /// </summary>
    /// <param name = "var"></param>
    /// <returns></returns>
    public static List<string> GetAllElementsLine(string var, object oddelovaciZnak = null)
    {
        if (oddelovaciZnak == null)
        {
            oddelovaciZnak = AllChars.pipe;
        }
        // Musí tu být none, protože pak když někde nic nebylo, tak mi to je nevrátilo a progran vyhodil IndexOutOfRangeException
        return SH.SplitNone(var, oddelovaciZnak);
    }
}