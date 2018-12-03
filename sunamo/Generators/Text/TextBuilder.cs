using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators.Text
{
    /// <summary>
    /// Similar: InstantSB(can specify own delimiter, check whether dont exists)
    /// TextBuilder(implements Undo, save to Sb or List)
    /// HtmlSB(Same as InstantSB, use br)
    /// 
    /// </summary>
    public class TextBuilder 
    {
        static Type type = typeof(TextBuilder);

        bool canUndo = false;
        int lastIndex = -1;
        string lastText = "";
        public StringBuilder sb = null;
        public string prependEveryNoWhite = string.Empty;
        /// <summary>
        /// For PowershellRunner
        /// </summary>
        public List<string> list = null;
        bool useList = false;

        public void Clear()
        {
            if (useList)
            {
                list.Clear();
            }
            else
            {
                sb.Clear();
            }
        }

        public TextBuilder(bool useList = false)
        {
            this.useList = useList;
            if (useList)
            {
                list = new List<string>();
            }
            else
            {
                sb = new StringBuilder();
            }
        }

        public bool CanUndo
        {
            get
            {
                if (useList)
                {
                    return false;
                }
                return canUndo;
            }
            set
            {
                if (true)
                {
                    UndoIsNotAllowed("CanUndo");
                }
                canUndo = value;
                if (!value)
                {
                    lastIndex = -1;
                    lastText = "";
                }
            }
        }

        void UndoIsNotAllowed(string method)
        {
            ThrowExceptions.IsNotAllowed(type, method, "Undo");
        }

        public void Undo()
        {
            if (useList)
            {
                UndoIsNotAllowed("Undo");
            }
            if (lastIndex != -1)
            {
                sb.Remove(lastIndex, lastText.Length);
            }
        }

        public void Append(string s)
        {
            if (useList)
            {
                CA.AppendToLastElement(list, s);
            }
            else
            {
                SetUndo(s);
                sb.Append(prependEveryNoWhite);
                sb.Append(s);
            }
        }

        private void SetUndo(string text)
        {
            if (useList)
            {
                UndoIsNotAllowed("SetUndo");
            }
            if (CanUndo)
            {
                lastIndex = sb.Length;
                lastText = text;
            }
        }

        public void Append(object s)
        {
            string text = s.ToString();
            SetUndo(text);
            Append(text);
        }

        public void AppendLine()
        {
            Append(Environment.NewLine);
        }

        public void AppendLine(string s)
        {
            if (useList)
            {
                list.Add( prependEveryNoWhite+ s);
            }
            else
            {
                SetUndo(s);
                sb.Append(prependEveryNoWhite + s + Environment.NewLine);
            }
        }

        /// <summary>
        /// Is is in use List, join it.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (useList)
            {
                return SH.JoinNL(list);
            }
            else
            {
                return sb.ToString();
            }
        }
    }
}
