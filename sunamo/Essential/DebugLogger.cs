using sunamo.Essential;

#if DEBUG
namespace sunamo
{

    /// <summary>
    /// Tento DebugLogger je ve sunamo, obsahuje jedinou metodu, kterou používej ve DebugLogger např. apps
    /// Pokud chceš rychleji zapisovat a nepotřebuješ explicitně nějaké metody, vytvoř si vlastní třídu DebugLogger v projektu aplikace. Ono by s_tejně kompilátor měl poznat že jen volá něco jiného a tak by to mělo být stejně efektivní
    /// </summary>
    public static class DebugLogger
    {
        static LogHelper logHelper = new LogHelper(DebugWriteLine);

        

        #region Merge with all
        private static void DebugWriteLine(string text)
        {
            Debug.WriteLine(text);
        }
        #endregion

        #region Write
        public static void WriteLine(string text)
        {
            logHelper.WriteLine(text);
        }

        public static void WriteLine(string what, object text)
        {
            logHelper.WriteLine(what, text);
        }

        public static void WriteNumberedList(string what, List<string> list, bool numbered)
        {
            logHelper.WriteNumberedList(what, list, numbered);
        }

        public static void WriteList(List<string> list)
        {
            list.ForEach(d => logHelper.WriteLine(d));
        }

        public static void TwoState(bool ret, params object[] toAppend)
        #endregion

        #region Debug specific
        public static void Break()
        {
            Debugger.Break();
        } 
        #endregion
    }
}
#endif
