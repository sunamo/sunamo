using sunamo.Constants;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// Cant be DEBUG, in dependent assembly often dont see this classes even if all projects is Debug
//#if DEBUG
//namespace sunamo.Essential
//{

/// <summary>
/// Tento //////DebugLogger.Instance je ve sunamo, obsahuje jedinou metodu, kterou používej ve //////DebugLogger.Instance např. apps
/// Pokud chceš rychleji zapisovat a nepotřebuješ explicitně nějaké metody, vytvoř si vlastní třídu //////DebugLogger.Instance v projektu aplikace. Ono by s_tejně kompilátor měl poznat že jen volá něco jiného a tak by to mělo být stejně efektivní
/// </summary>
public class DebugLogger : LoggerBase
{
    public static DebugLogger Instance =
#if DEBUG2
        new DebugLogger(DebugWriteLine);
#elif !DEBUG2
        //new DebugLogger(DebugWriteLine);
        null;
#endif

    public DebugLogger(VoidStringParamsObjects writeLineHandler) : base(writeLineHandler)
    {
    }
#if DEBUG //2
    public static void DebugWriteLine(TypeOfMessage tz, string text, params object[] args)
    {
        //DebugLogger.DebugWriteLine(tz.ToString() + AllStrings.cs2 + SH.Format2(text, args));
    }

    public static void DebugWriteLine(string text, params object[] args)
    {
        Debug.WriteLine(SH.Format2(text, args));
    }
#endif

    public static void Break()
    {
        Debugger.Break();
    }
}