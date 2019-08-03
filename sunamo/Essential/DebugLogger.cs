using sunamo.Constants;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Diagnostics;


//namespace sunamo.Essential
//{

/// <summary>
/// Tento //DebugLogger.Instance je ve sunamo, obsahuje jedinou metodu, kterou používej ve //DebugLogger.Instance např. apps
/// Pokud chceš rychleji zapisovat a nepotřebuješ explicitně nějaké metody, vytvoř si vlastní třídu //DebugLogger.Instance v projektu aplikace. Ono by s_tejně kompilátor měl poznat že jen volá něco jiného a tak by to mělo být stejně efektivní
/// </summary>
public class DebugLogger : LoggerBase
{
    public static DebugLogger Instance = new DebugLogger(DebugWriteLine);

    public DebugLogger(VoidStringParamsObjects writeLineHandler) : base(writeLineHandler)
    {
    }

    public static void DebugWriteLine(TypeOfMessage tz, string text, params object[] args)
    {
        Debug.WriteLine(tz.ToString() + AllStrings.cs2 + SH.Format2(text, args));
    }

    public static void DebugWriteLine(string text, params object[] args)
    {
        Debug.WriteLine(SH.Format2(text, args));
    }

    public static void Break()
    {
        Debugger.Break();
    }
}