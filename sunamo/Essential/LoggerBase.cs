using sunamo.Constants;
using sunamo.Values;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace sunamo.Essential
{
    public class LoggerBase
    {
        // TODO: Make logger class as base and replace all occurences With Instance 

        
        private bool isActive = false;

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                if (value)
                {
                    sunamo.Essential.ThisApp.StatusSetted += ThisApp_StatusSetted;
                }
                else
                {
                    // Don't raise exception when StatusSetted is null
                    sunamo.Essential.ThisApp.StatusSetted -= ThisApp_StatusSetted;
                }
            }
        }

        private  void ThisApp_StatusSetted(TypeOfMessage t, string message)
        {
            WriteLine(t.ToString() + Consts.cs + message);
        }

        VoidString writeLineDelegate;

        public LoggerBase(VoidString writeLineDelegate)
        {
            this.writeLineDelegate = writeLineDelegate;
        }

        #region General
        public void TwoState(bool ret, params object[] toAppend)
        {
            writeLineDelegate.Invoke(ret.ToString() + "," + SH.Join(',', toAppend));
        }

        public void WriteLine(string text)
        {
            writeLineDelegate.Invoke(text);
        }

        public void WriteCount(string what, System.Collections.IEnumerable text)
        {
            WriteLine(what + " count:", text.Count());
        }

        public void GetFormattedHeader(ref string what)
        {
            string append = string.Empty;

            if (!string.IsNullOrEmpty(what))
            {
                append = what + ": ";
            }
            what = append;
        }

        public void WriteLine(string what, object text)
        {
            if (text != null)
            {
                GetFormattedHeader(ref what);
                writeLineDelegate.Invoke(what + text.ToString());
            }
        }

        public void WriteNumberedList(string what, List<string> list, bool numbered)
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

        public void WriteList(string name, IEnumerable list)
        {
            WriteLine(name + " elements", string.Empty);
            WriteList(list);
        }

        public void WriteList(IEnumerable list)
        {
            list.ForEach(d => writeLineDelegate.Invoke(d));
        } 
        #endregion


    }
}
