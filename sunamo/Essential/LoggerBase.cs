using System;
using System.Collections;
using System.Collections.Generic;

namespace sunamo.Essential
{
    public class LoggerBase
    {
        // TODO: Make logger class as base and replace all occurences With Instance 

        VoidStringParamsObjects writeLineDelegate;
        public bool IsActive = true;

        public LoggerBase(VoidStringParamsObjects writeLineDelegate)
        {
            this.writeLineDelegate = writeLineDelegate;
        }
        
        public void WriteCount(string collectionName, IEnumerable list)
        {
            WriteLine(collectionName + " count: " + list.Count());
            
        }

        public void WriteList(string collectionName, IEnumerable list)
        {
            WriteLine(collectionName + " elements:");
            foreach (var item in list)
            {
                WriteLine(item.ToString());
            }
        }

        public  void TwoState(bool ret, params object[] toAppend)
        {
            WriteLine(ret.ToString() + "," + SH.Join(',', toAppend));
        }

        public void WriteArgs(params object[] args)
        {
            writeLineDelegate.Invoke(SH.JoinPairs(args));
        }

        public  void WriteLine(string text, params object[] args)
        {
            if (IsActive)
            {
                writeLineDelegate.Invoke(text, args);
            }
            
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
                WriteLine(append + text.ToString());
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
                    WriteLine(list[i]);
                }
            }
        }

        

        public void WriteList(List<string> list)
        {
            list.ForEach(d => WriteLine(d));
        }

        
    }
}
