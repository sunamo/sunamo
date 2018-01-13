using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Essential
{
    public class LoggerBase
    {
         VoidString writeLineDelegate;

        public LoggerBase(VoidString writeLineDelegate)
        {
            this.writeLineDelegate = writeLineDelegate;
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
            Debug.WriteLine(what + ":");
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

        public void WriteLine(string text)
        {
            writeLineDelegate.Invoke(text);
        }
    }
}
