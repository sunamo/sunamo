
using System;
using System.Collections;
using System.Collections.Generic;

namespace sunamo.Essential
{
    public class LoggerBase
    {
        // TODO: Make logger public class as base and replace all occurences With Instance 

        VoidStringParamsObjects writeLineDelegate;
        public bool IsActive = true;
        static Type type = typeof(LoggerBase);

        public void DumpObject(string name, object o, DumpProvider d, params string[] onlyNames)
        {
            var dump = RH.DumpAsString(name, o, d, onlyNames);

            WriteLine(dump);
            WriteLine(AllStrings.space);
        }

        public void DumpObjects(string name, IEnumerable o, DumpProvider d, params string[] onlyNames)
        {
            int i = 0;
            foreach (var item in o)
            {
                DumpObject(name + " #" + i, item, d, onlyNames);
                i++;
            }
        }

        /// <summary>
        /// Only due to Old sfw apps
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="name"></param>
        /// <param name="v2"></param>
        public void WriteLineFormat(string v1, params string[] name)
        {
            WriteLine(v1, name);
        }

        public LoggerBase(VoidStringParamsObjects writeLineDelegate)
        {
            this.writeLineDelegate = writeLineDelegate;
        }
        
        public void WriteCount(string collectionName, IEnumerable list)
        {
            WriteLine(collectionName + " " + "count" + ": " + list.Count());
            
        }


        public void WriteList(string collectionName, List<string> list)
        {
            WriteLine(collectionName + " " + "elements" + ":");
            WriteList(list);
        }

        public  void TwoState(bool ret, params object[] toAppend)
        {
            WriteLine(ret.ToString() + AllStrings.comma + SH.Join(AllChars.comma, toAppend));
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
        /// <summary>
        /// for compatibility with Console.WriteLine 
        /// </summary>
        /// <param name="what"></param>
        public void WriteLine(object what)
        {
            if (what != null)
            {
                WriteLine(SH.ListToString( what));
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
            writeLineDelegate.Invoke(what + AllStrings.colon);
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
