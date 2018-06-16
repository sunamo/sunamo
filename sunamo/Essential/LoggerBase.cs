using System.Collections.Generic;

namespace sunamo.Essential
{
    public class LoggerBase
    {
        // TODO: Make logger class as base and replace all occurences With Instance 

        VoidString writeLineDelegate;

        public LoggerBase(VoidString writeLineDelegate)
        {
            this.writeLineDelegate = writeLineDelegate;
        }

        public  void TwoState(bool ret, params object[] toAppend)
        {
            writeLineDelegate.Invoke(ret.ToString() + "," + SH.Join(',', toAppend));
        }

        public void WriteLine(string text)
        {
            writeLineDelegate.Invoke(text);
        }

        public  void WriteLine(string what, object text)
        {
            if (text != null)
            {
                string append = string.Empty;

                if (!string.IsNullOrEmpty(what))
                {
                    append = what + ": ";
                }
                writeLineDelegate.Invoke(append + text.ToString());
            }
        }

        public  void WriteNumberedList(string what, List<string> list, bool numbered)
        {
            writeLineDelegate.Invoke(what + ":");
            for (int i = 0; i < list.Count; i++)
            {
                if (numbered)
                {
                    WriteLine((i + 1).ToString(), list[i]);
                }
                else
                {
                    writeLineDelegate.Invoke(list[i]);
                }
            }
        }

        public void WriteList(List<string> list)
        {
            list.ForEach(d => writeLineDelegate.Invoke(d));
        }

        
    }
}
