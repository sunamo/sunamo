using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Diagnostics;


//namespace sunamo.Essential
//{
    
    /// <summary>
    /// Tento DebugLogger.Instance je ve sunamo, obsahuje jedinou metodu, kterou používej ve DebugLogger.Instance např. apps
    /// Pokud chceš rychleji zapisovat a nepotřebuješ explicitně nějaké metody, vytvoř si vlastní třídu DebugLogger.Instance v projektu aplikace. Ono by s_tejně kompilátor měl poznat že jen volá něco jiného a tak by to mělo být stejně efektivní
    /// </summary>
    public  class DebugLogger : LoggerBase
    {
        public static DebugLogger Instance = new DebugLogger(DebugWriteLine);

        public DebugLogger(VoidString writeLineHandler) : base(writeLineHandler)
        {

        }

        public static void DebugWriteMessage(TypeOfMessage typeOfMessage, string text, params object[] args)
        {
            DebugWriteLine(typeOfMessage.ToString() + ": " + string.Format( text, args));
        }

        private static void DebugWriteLine(string text)
        {
            Debug.WriteLine(text);
        }

        public static void Break()
        {
            Debugger.Break();
        }

    
}
//}
#if DEBUG
#endif
